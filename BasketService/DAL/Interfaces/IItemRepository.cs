using BasketService.Domain.Entities;

namespace BasketService.DAL.Interfaces
{
    public interface IItemRepository
    {
        public void AddItem(Item item);
        public void DeleteItem(int itemId);

        public void UpdateItem(Item item);

        void UpdateItems(IEnumerable<Item> items);

        IEnumerable<Item> FindByItemId(int itemId);
    }
}
