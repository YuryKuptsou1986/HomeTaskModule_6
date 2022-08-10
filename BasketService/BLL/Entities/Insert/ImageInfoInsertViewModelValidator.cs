using FluentValidation;

namespace BasketService.BLL.Entities.Insert
{
    public class ImageInfoInsertViewModelValidator : AbstractValidator<ImageInfoInsertViewModel>
    {
        public ImageInfoInsertViewModelValidator()
        {
            RuleFor(x => x.Url).NotNull().NotEmpty().WithMessage("Can not be null or empty");
            RuleFor(x => x.Url)
                .Must(uri => Uri.TryCreate(uri.ToString(), UriKind.Absolute, out _))
                .When(x => !string.IsNullOrEmpty(x.Url.ToString()))
                .WithMessage("URI is not valid.");
        }
    }
}
