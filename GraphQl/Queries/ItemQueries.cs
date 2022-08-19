using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Page;
using ViewModel.Query;
using ViewModel.View;

namespace GraphQl.Queries
{
    [ExtendObjectType(typeof(Queries))]
    public class ItemQueries
    {
        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 2)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<ItemViewModel> Items([Service] IItemService itemService) => itemService.List();
    }
}
