using Domain;
using FluentValidation;

namespace Application.Register
{
    public class RegisterValidator : AbstractValidator<Domain.Register>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.RegisterDate).NotEmpty();
            RuleFor(x => x.RegisterTotal).NotEmpty();
            RuleFor(x => x.RegisterAmount).NotEmpty();
            RuleFor(x => x.RegisterLeads).NotEmpty();
            RuleFor(x => x.EventsId).NotEmpty();
            RuleFor(x => x.SellerId).NotEmpty();
            RuleFor(x => x.BookmakerId).NotEmpty();
            RuleFor(x => x.ProjectId).NotEmpty();
        }
    }
}