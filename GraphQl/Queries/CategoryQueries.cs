using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.View;

namespace GraphQl.Queries
{
    [ExtendObjectType(typeof(Queries))]
    public class CategoryQueries
    {
        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 2)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<CategoryViewModel> Categories([Service] ICategoryService categoryService) => categoryService.List();
    }
}
