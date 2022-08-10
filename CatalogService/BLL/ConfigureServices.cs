using BLL.Mappings;
using BLL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BLL
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddBLLServices(this IServiceCollection services)
        {
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddAutoMapper(typeof(AppMappingProfile));

            return services;
        }
    }
}
