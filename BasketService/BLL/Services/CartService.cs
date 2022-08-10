using BasketService.Domain.Entities;
using BasketService.DAL.Interfaces;
using BasketService.BLL.Entities.Insert;
using BasketService.BLL.Entities.View;
using AutoMapper;
using BasketService.BLL.Entities.Update;

namespace BasketService.BLL.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public CartService(ICartRepository cartRepository, IItemRepository itemRepository, IMapper mapper)
        {
            if(cartRepository == null) {
                throw new ArgumentNullException("Can not be null", nameof(cartRepository));
            }

            _cartRepository = cartRepository;
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        public void AddItem(CartInsertViewModel cart)
        {
            var cartMapped = _mapper.Map<Cart>(cart);
            var itemMapped = _mapper.Map<Item>(cart.Item);
            _itemRepository.AddItem(itemMapped);

            if (!_cartRepository.CheckCartExists(cartMapped.Id)) {
                cartMapped.Items.Add(itemMapped);
                _cartRepository.AddCart(cartMapped);
            } else {
                var existCart = _cartRepository.GetCartById(cartMapped.Id);
                existCart.Items.Add(itemMapped);
                _cartRepository.UpdateCart(existCart);
            }
        }

        public void DeleteItem(string cartId, int itemId)
        {
            var existCart = _cartRepository.GetCartById(cartId);
            var item = existCart.Items.Where(x => x.Id == itemId).FirstOrDefault();
            _itemRepository.DeleteItem(itemId);
            existCart.Items.Remove(item);
            _cartRepository.UpdateCart(existCart);
        }

        public CartViewModel GetCartById(string cartId)
        {
            return _mapper.Map<CartViewModel>(_cartRepository.GetCartById(cartId));
        }

        public void UpdateItem(ItemUpdateViewModel item)
        {
            var updatedItem = _mapper.Map<Item>(item);

            var existingItems = _itemRepository.FindByItemId(item.ItemId).ToList();

            existingItems.ToList().ForEach(x => {
                x.Update(updatedItem.Name, updatedItem.Price, updatedItem.Quantity, updatedItem.ImageInfo);
            });

            _itemRepository.UpdateItems(existingItems);
        }
    }
}
