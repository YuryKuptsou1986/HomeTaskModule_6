using BasketService.Domain.Entities;
using BasketService.DAL.LiteDb.DbContext;
using BasketService.DAL.Interfaces;
using BasketService.DAL.Exceptions;

namespace BasketService.DAL.LiteDb.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ILiteDBContext _dbContext;

        public CartRepository(ILiteDBContext dbContext)
        {
            if (dbContext == null) {
                throw new ArgumentNullException("Can not be null", nameof(dbContext));
            }
            _dbContext = dbContext;
        }

        public Cart GetCartById(string cartId)
        {
            var cart = _dbContext.Database.GetCollection<Cart>(nameof(Cart)).Include(x => x.Items).FindById(cartId);
            if (cart == null) {
                throw new NotFoundException(nameof(Cart), cartId);
            }
            return cart;
        }

        public void AddCart(Cart cart)
        {
            _dbContext.Database.GetCollection<Cart>(nameof(Cart)).Insert(cart);
        }

        public bool CheckCartExists(string cartId)
        {
            return _dbContext.Database.GetCollection<Cart>(nameof(Cart)).FindById(cartId) != null;
        }

        public void UpdateCart(Cart cart)
        {
            if (!CheckCartExists(cart.Id)) {
                if (cart == null) {
                    throw new NotFoundException(nameof(Cart), cart.Id);
                }
            }
            _dbContext.Database.GetCollection<Cart>(nameof(Cart)).Update(cart);
        }
    }
}
