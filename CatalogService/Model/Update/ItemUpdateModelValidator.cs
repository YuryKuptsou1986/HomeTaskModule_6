using FluentValidation;

namespace ViewModel.Update
{
    public class ItemUpdateModelValidator : AbstractValidator<ItemUpdateModel>
    {
        public ItemUpdateModelValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Can not be null")
                .NotEmpty().WithMessage("Can not be empty")
                .MaximumLength(50).WithMessage("Maximum characters are 50");
            RuleFor(x => x.CategoryId).NotNull().WithMessage("Is required");
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage("Should be greater or equals 0");
            RuleFor(x => x.Amount).GreaterThanOrEqualTo(0).WithMessage("Should be greater or equals 0");
        }
    }
}
