using BasketService.DAL.Interfaces;
using BasketService.DAL.Exceptions;
using BasketService.DAL.LiteDb.DbContext;
using BasketService.Domain.Entities;

namespace BasketService.DAL.LiteDb.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ILiteDBContext _dbContext;

        public ItemRepository(ILiteDBContext dbContext)
        {
            if (dbContext == null) {
                throw new ArgumentNullException("Can not be null", nameof(dbContext));
            }
            _dbContext = dbContext;
        }

        public void AddItem(Item item)
        {
            _dbContext.Database.GetCollection<Item>(nameof(Item)).Insert(item);
        }

        public void DeleteItem(int Id)
        {
            var item = _dbContext.Database.GetCollection<Item>(nameof(Item)).FindById(Id);
            if (item == null) {
                throw new NotFoundException(nameof(Item), Id);
            }

            _dbContext.Database.GetCollection<Item>(nameof(Item)).Delete(Id);
        }

        public void UpdateItem(Item item)
        {
            var updatedItem = _dbContext.Database.GetCollection<Item>(nameof(Item)).FindById(item.ItemId);
            if (item != null) {
                _dbContext.Database.GetCollection<Item>().Update(item);
            }
        }

        public void UpdateItems(IEnumerable<Item> items)
        {
            if (items != null) {
                _dbContext.Database.GetCollection<Item>().Update(items);
            }
        }

        public IEnumerable<Item> FindByItemId(int itemId)
        {
            return _dbContext.Database.GetCollection<Item>().Find(x => x.ItemId == itemId);
        }
    }
}
