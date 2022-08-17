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
        public Task<IPagedCollection<ItemViewModel>> Categories([Service] IItemService itemService, ItemQuery query) => itemService.ListAsync(query);
    }
}
