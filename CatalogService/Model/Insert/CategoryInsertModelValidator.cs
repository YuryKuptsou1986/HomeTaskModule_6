using FluentValidation;

namespace ViewModel.Insert
{
    public class CategoryInsertModelValidator : AbstractValidator<CategoryInsertModel>
    {
        public CategoryInsertModelValidator()
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
