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
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public Task<IEnumerable<CategoryViewModel>> Categories([Service] ICategoryService categoryService) => categoryService.ListAsync();
    }
}
