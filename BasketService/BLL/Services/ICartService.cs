using BasketService.BLL.Entities.Insert;
using BasketService.BLL.Entities.Update;
using BasketService.BLL.Entities.View;

namespace BasketService.BLL.Services
{
    public interface ICartService
    {
        public CartViewModel GetCartById(string id);
        public void AddItem(CartInsertViewModel item);
        public void DeleteItem(string cartId, int itemId);
        public void UpdateItem(ItemUpdateViewModel item);
    }
}
