using FluentValidation;

namespace ViewModel.Update
{
    public class CategoryUpdateModelValidator : AbstractValidator<CategoryUpdateModel>
    {
        public CategoryUpdateModelValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Name can not be null")
                .NotEmpty().WithMessage("Name can not be empty")
                .MaximumLength(50).WithMessage("Maximum characters for Name are 50");
            RuleFor(x => x.Image)
                .Must(uri => Uri.TryCreate(uri.ToString(), UriKind.Absolute, out _))
                .When(x => !string.IsNullOrEmpty(x.Image.ToString()))
                .WithMessage("URI is not valid.");
        }
    }
}
