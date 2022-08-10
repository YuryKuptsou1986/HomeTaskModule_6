using DAL.Context;
using DAL.Filter;
using DAL.Interfaces;
using DAL.Models.Update;
using Domain.Entities;
using Extension.Exceptions;
using Microsoft.EntityFrameworkCore;
using ViewModel.Page;
using ViewModel.Query;

namespace DAL.Repositories
{
    internal class ItemRepository : IItemRepository
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IItemFilterBuilder _itemFilterBuilder;

        public ItemRepository(IApplicationDbContext context, IItemFilterBuilder itemFilterBuilder)
        {
            _dbContext = context;
            _itemFilterBuilder = itemFilterBuilder;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbContext.Items.FindAsync(new object[] { id });

            if (entity == null) {
                throw new NotFoundException(nameof(Item), id);
            }

            _dbContext.Items.Remove(entity);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Item> GetAsync(int id)
        {
            var entity = await _dbContext.Items.FindAsync(new object[] { id });

            if (entity == null) {
                throw new NotFoundException(nameof(Item), id);
            }

            return entity;
        }

        public async Task AddAsync(Item item)
        {
            await _dbContext.Items.AddAsync(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IPagedCollection<Item>> ListAsync(ItemQuery query)
        {
            var filter = _itemFilterBuilder.WhereCategoryId(query.CategoryId).Filter;

            var itemCount = await _dbContext.Items.CountAsync();

            var items  = await _dbContext
                .Items
                .Where(filter)
                .Skip(query.PageSize * (query.Page - 1))
                .Take(query.PageSize)
                .Include(x=>x.Category).ToListAsync();

            return new PagedCollection<Item>(
               items,
               itemCount,
               query.Page,
               query.PageSize
               );
        }

        public async Task UpdateAsync(ItemUpdateDataModel item)
        {
            var entity = await _dbContext.Items.FindAsync(new object[] { item.Id });

            if (entity == null) {
                throw new NotFoundException(nameof(Item), item.Id);
            }

            entity.Update(item.Name, item.Description, item.Image, item.CategoryId, item.Price, item.Amount);

            await _dbContext.SaveChangesAsync();
        }
    }
}
