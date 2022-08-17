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
    public class CategoryMutations
    {
        public async Task<bool> UpdateCategory(
            [Service] ICategoryService categoryService,
            int id, 
            [UseFluentValidation, UseValidator<CategoryUpdateModelValidator>] CategoryUpdateModel category
            )
        {
            await categoryService.UpdateAsync(id, category);
            return true;
        }

        public async Task<bool> AddCategory(
            [Service] ICategoryService categoryService,
            [UseFluentValidation, UseValidator<CategoryInsertModelValidator>] CategoryInsertModel category
            )
        {
            await categoryService.AddAsync(category);
            return true;
        }

        public async Task<bool> DeleteCategory(
            [Service] ICategoryService categoryService,
            int id
            )
        {
            await categoryService.DeleteAsync(id);
            return true;
        }
    }
}
