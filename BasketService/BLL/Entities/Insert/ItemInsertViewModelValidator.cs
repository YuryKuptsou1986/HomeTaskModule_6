using FluentValidation;

namespace BasketService.BLL.Entities.Insert
{
    public class ItemInsertViewModelValidator : AbstractValidator<ItemInsertViewModel>
    {
        public ItemInsertViewModelValidator()
        {
            RuleFor(x => x.ItemId).GreaterThanOrEqualTo(1).WithMessage("Should be greater or equals 0");
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage("Should be greater or equals 0");
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Can not be null or empty");
            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0).WithMessage("Should be greater or equals 0");
            RuleFor(x => x.ImageInfo).SetValidator(new ImageInfoInsertViewModelValidator()).When(x => x.ImageInfo != null);
        }
    }
}
