using Domain.Entities;
using Extension.Expressions;
using System.Linq.Expressions;

namespace DAL.Filter
{
    public class ItemFilterBuilder : IItemFilterBuilder
    {
        public Expression<Func<Item, bool>> Filter { get; private set; } = ExpressionBuilder.True<Item>();

        public IItemFilterBuilder WhereCategoryId(int? categoryId)
        {
            if (categoryId.HasValue) {
                Filter = Filter.And(item => item.CategoryId == categoryId);
            }

            return this;
        }
    }
}
