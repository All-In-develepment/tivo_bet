using FluentValidation;

namespace Application.ProjectWeight
{
    public class ProjectWeightValidator : AbstractValidator<Domain.ProjectWeight>
    {
        public ProjectWeightValidator()
        {
            RuleFor(x => x.ProjectId).NotEmpty();
            RuleFor(x => x.Month).NotEmpty();
            RuleFor(x => x.SalesValueWeight).NotEmpty();
            RuleFor(x => x.ConversionWeight).NotEmpty();
            RuleFor(x => x.RegistrationWeight).NotEmpty();
            RuleFor(x => x.DepositWeight).NotEmpty();
        }
    }
}