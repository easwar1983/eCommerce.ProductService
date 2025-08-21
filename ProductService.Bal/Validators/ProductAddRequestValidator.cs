using FluentValidation;
using ProductService.Bal.DTO;

namespace ProductService.Bal.Validators;
public class ProductAddRequestValidator : AbstractValidator<ProductAddRequest>
{
    public ProductAddRequestValidator()
    {
        // Product Name
        RuleFor(x => x.ProductName)
            .NotEmpty().WithMessage("Product name is required.");

        // Category
        RuleFor(x => x.Category)
            .IsInEnum().WithMessage("Invalid category option, Allowed: Electronics, HomeAppliances, Furniture, Accessories, Sports, Toys, Beauty, Automotive, Grocery, Health");

        // Unit Price
        RuleFor(x => x.UnitPrice)
            .GreaterThan(0).WithMessage("Price must be greater than zero.")
            .LessThanOrEqualTo(100000).WithMessage("Price must be less than or equal to 100,000.");

        // Quantity in stock
        RuleFor(x => x.QuantityInStock)
            .GreaterThanOrEqualTo(0).WithMessage("Quantity in stock must be greater than or equal to zero.")
            .LessThanOrEqualTo(10000).WithMessage("Quantity in stock must be less than or equal to 10,000.");
    }
}
