using BasketService.DAL.Interfaces;
using BasketService.DAL.LiteDb.Repositories;
using BasketService.Filters;
using BasketService.BLL.Entities.Insert;
using BasketService.BLL.Mappings;
using BasketService.BLL.Services;
using BasketService.DAL.LiteDb.DbContext;
using BasketService.DAL.LiteDb.Providers;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using ServiceMessaging.RabbitMQService;
using ServiceMessaging.RabbitMQService.Connection;
using ServiceMessaging.MessageQueue;
using ServiceMessaging.Items;
using ServiceMessaging.MessageQueue.Subscription;
using BasketService.Services;
using ServiceMessaging.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<MessageQueueHostedService>();

builder.Services.AddScoped<ILiteDbSettingsProvider, LiteDbSettingsProvider>();
builder.Services.AddScoped<ILiteDBContext, LiteDBContext>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<ICartService, CartService>();

builder.Services.AddSingleton<IMessageQueueSettingProvider, MessageQueueSettingProvider>();
builder.Services.AddSingleton<IRabbitMqConnectionFactory, RabbitMqConnectionFactory>();
builder.Services.AddSingleton<IRabbitMqConnection, RabbitMqConnection>();
builder.Services.AddTransient<IServiceScope>(sp => { return sp.CreateScope(); });
builder.Services.AddSingleton<IMessageQueueSubscriptionsManager, MessageQueueSubscriptionsManager>();
builder.Services.AddTransient<IMessageQueueListener, RabbitMessageQueueListener>();
builder.Services.AddTransient<ItemChangeMessageQueueEvent>();

builder.Services.AddAutoMapper(typeof(AppMappingProfile));

// Add services to the container.
builder.Services.AddControllers(options => options.Filters.Add(new ApiExceptionFilterAttribute()))
    .AddFluentValidation(options => {
        options.RegisterValidatorsFromAssemblyContaining<CartInsertViewModelValidator>(includeInternalTypes: true);
        options.RegisterValidatorsFromAssemblyContaining<ImageInfoInsertViewModelValidator>(includeInternalTypes: true);
        options.RegisterValidatorsFromAssemblyContaining<ItemInsertViewModelValidator>(includeInternalTypes: true);
    });
builder.Services.AddApiVersioning(config => {
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer(options => { 
    options.GroupNameFormat = "'v'VVV";  
    options.SubstituteApiVersionInUrl = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "Cart Service" });
    c.SwaggerDoc("v2", new OpenApiInfo { Version = "v2", Title = "Cart Service" });
});

var app = builder.Build();

var queueListenerService = app.Services.GetRequiredService<IMessageQueueListener>();
queueListenerService.Subscribe<ItemMessage, ItemChangeMessageQueueEvent>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "version v1");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "version v2");
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
