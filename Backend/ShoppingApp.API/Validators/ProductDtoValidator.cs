using FluentValidation;
using ShoppingApp.API.Dtos;

namespace ShoppingApp.API.Validators
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title cannot be null or empty.")
            .Length(3, 30).WithMessage("Title must be between 3 and 30 characters long.");

            RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Category cannot be null or empty.")
            .Length(3, 30).WithMessage("Category must be between 3 and 30 characters long.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description cannot be null or empty.")
                .MaximumLength(200).WithMessage("Description must not exceed 200 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");
        }
    }
}
