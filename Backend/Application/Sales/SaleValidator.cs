using Domain;
using FluentValidation;

namespace Application.Sales
{
    public class SaleValidator : AbstractValidator<Sale>
    {
        public SaleValidator()
        {
            RuleFor(x => x.SalePrice).NotEmpty();
            RuleFor(x => x.SaleDate).NotEmpty();
        }
    }
}