using DAL.Models.Update;
using Domain.Entities;
using ViewModel.Page;
using ViewModel.Query;

namespace DAL.Interfaces
{
    public interface IItemRepository
    {
        Task<IPagedCollection<Item>> ListAsync(ItemQuery query);
        IQueryable<Item> List();
        Task<Item> GetAsync(int id);
        Task AddAsync(Item item);
        Task DeleteAsync(int id);
        Task UpdateAsync(ItemUpdateDataModel item);
    }
}
