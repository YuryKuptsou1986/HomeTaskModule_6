using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context
{
    internal class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { 
            Database.EnsureCreated();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureCategory(modelBuilder);
            ConfigureItem(modelBuilder);
        }

        private void ConfigureCategory(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasKey(c => c.Id);
            modelBuilder.Entity<Category>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Category>().Property(x => x.Name).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Category>().Property(x => x.Image).IsRequired(false);
            modelBuilder.Entity<Category>().Property(x => x.ParentCategoryId).IsRequired(false);            
            modelBuilder.Entity<Category>().HasOne(x => x.ParentCategory);
        }

        private void ConfigureItem(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().HasKey(x => x.Id);
            modelBuilder.Entity<Item>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Item>().Property(x => x.Name).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Item>().Property(x => x.Description).IsRequired(false);
            modelBuilder.Entity<Item>().Property(x => x.Image).IsRequired(false);
            modelBuilder.Entity<Item>().Property(x => x.CategoryId).IsRequired();
            modelBuilder.Entity<Item>().HasOne(x => x.Category);
            modelBuilder.Entity<Item>().Property(x => x.Price).IsRequired();
            modelBuilder.Entity<Item>().Property(x => x.Amount).IsRequired();
        }
    }
}
