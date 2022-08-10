using ViewModel.Insert;
using ViewModel.Page;
using ViewModel.Query;
using ViewModel.Update;
using ViewModel.View;

namespace BLL.Services
{
    public interface IItemService
    {
        Task<IPagedCollection<ItemViewModel>> ListAsync(ItemQuery query);
        Task<ItemViewModel> GetAsync(int id);
        Task AddAsync(ItemInsertModel item);
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, ItemUpdateModel item);
    }
}
