using FluentValidation;

namespace ViewModel.Insert
{
    public class CategoryInsertModelValidator : AbstractValidator<CategoryInsertModel>
    {
        public CategoryInsertModelValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Can not be null")
                .NotEmpty().WithMessage("Can not be empty")
                .MaximumLength(50).WithMessage("Maximum characters are 50");
        }
    }
}
