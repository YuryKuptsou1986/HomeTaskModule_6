using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context
{
    internal interface IApplicationDbContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Item> Items { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
