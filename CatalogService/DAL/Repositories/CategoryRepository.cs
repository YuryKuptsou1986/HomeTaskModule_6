using DAL.Context;
using DAL.Interfaces;
using DAL.Models.Update;
using Domain.Entities;
using Extension.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    internal class CategoryRepository : ICategoryRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public CategoryRepository(IApplicationDbContext context)
        {
            _dbContext = context;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbContext.Categories.FindAsync(new object[] { id });
            
            if (entity == null) {
                throw new NotFoundException(nameof(Category), id);
            }

            var parentCategories = _dbContext.Categories.Where(x => x.ParentCategoryId == entity.Id);
            await parentCategories.ForEachAsync(x => x.Update(x.Name, x.Image, null));

            _dbContext.Categories.UpdateRange(parentCategories);
            _dbContext.Categories.Remove(entity);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Category> GetAsync(int id)
        {
            var entity = await _dbContext.Categories.FindAsync(new object[] { id });

            if (entity == null) {
                throw new NotFoundException(nameof(Category), id);
            }

            return entity;
        }

        public async Task AddAsync(Category item)
        {
            await _dbContext.Categories.AddAsync(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Category>> ListAsync()
        {
            var items = await _dbContext.Categories.ToListAsync();
            return items;
        }

        public async Task UpdateAsync(CategoryUpdateDataModel item)
        {
            var entity = await _dbContext.Categories.FindAsync(new object[] { item.Id });

            if (entity == null) {
                throw new NotFoundException(nameof(Category), item.Id);
            }
            entity.Update(item.Name, item.Image, item.ParentCategoryId);

            await _dbContext.SaveChangesAsync();
        }
    }
}
