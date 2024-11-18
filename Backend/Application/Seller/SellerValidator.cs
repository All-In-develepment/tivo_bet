using FluentValidation;

namespace Application.Seller
{
    public class SellerValidator : AbstractValidator<Domain.Seller>
    {
        public SellerValidator() 
        {
            RuleFor(x => x.SellerName).NotEmpty();
            RuleFor(x => x.ProjectId).NotEmpty();
            RuleFor(x => x.SellerIsActive).NotNull();
        }
    }
}