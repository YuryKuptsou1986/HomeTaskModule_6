using FluentValidation;

namespace ViewModel.Update
{
    public class ItemUpdateModelValidator : AbstractValidator<ItemUpdateModel>
    {
        public ItemUpdateModelValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Name can not be null")
                .NotEmpty().WithMessage("Name can not be empty")
                .MaximumLength(50).WithMessage("Maximum characters for Name are 50");
            RuleFor(x => x.CategoryId).NotNull().WithMessage("CategoryId is required");
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage("Price should be greater or equals 0");
            RuleFor(x => x.Amount).GreaterThanOrEqualTo(0).WithMessage("Amount should be greater or equals 0");
        }
    }
}
