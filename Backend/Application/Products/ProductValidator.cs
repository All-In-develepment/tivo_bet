using Domain;
using FluentValidation;

namespace Application.Products
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.ProductName).NotEmpty();
        }
    }
}