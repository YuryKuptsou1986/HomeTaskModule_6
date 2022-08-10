using BasketService.Domain.Entities;
using System.Collections.Generic;

namespace BasketService.DAL.Interfaces
{
    public interface ICartRepository
    {
        public Cart GetCartById(string cartId);
        public bool CheckCartExists(string cartId);
        public void UpdateCart(Cart cart);
        public void AddCart(Cart cart);
    }
}
