using Domain.Entities;
using System.Linq.Expressions;

namespace DAL.Filter
{
    public interface IItemFilterBuilder
    {
        Expression<Func<Item, bool>> Filter { get; }

        IItemFilterBuilder WhereCategoryId(int? categoryId);
    }
}
