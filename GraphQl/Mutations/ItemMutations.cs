using AppAny.HotChocolate.FluentValidation;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Insert;
using ViewModel.Update;

namespace GraphQl.Mutations
{
    [ExtendObjectType(typeof(Mutations))]
    public class ItemMutations
    {
        public async Task<bool> UpdateItem(
            [Service] IItemService itemService,
            int id,
            [UseFluentValidation, UseValidator<ItemUpdateModelValidator>] ItemUpdateModel item
            )
        {
            await itemService.UpdateAsync(id, item);
            return true;
        }

        public async Task<bool> AddItem(
            [Service] IItemService itemService,
            [UseFluentValidation, UseValidator<ItemInsertModelValidator>] ItemInsertModel item
            )
        {
            await itemService.AddAsync(item);
            return true;
        }

        public async Task<bool> DeleteItem(
            [Service] IItemService itemService,
            int id
            )
        {
            await itemService.DeleteAsync(id);
            return true;
        }
    }
}
