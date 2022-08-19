using DAL.Models.Update;
using Domain.Entities;

namespace DAL.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> ListAsync();
        IQueryable<Category> List();
        Task<Category> GetAsync(int id);
        Task AddAsync(Category item);
        Task DeleteAsync(int id);
        Task UpdateAsync(CategoryUpdateDataModel item);
    }
}
