using ViewModel.Insert;
using ViewModel.Update;
using ViewModel.View;

namespace BLL.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewModel>> ListAsync();
        IQueryable<CategoryViewModel> List();
        Task<CategoryViewModel> GetAsync(int id);
        Task AddAsync(CategoryInsertModel category);
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, CategoryUpdateModel category);
    }
}
