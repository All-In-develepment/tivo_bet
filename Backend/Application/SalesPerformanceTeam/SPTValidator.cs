using FluentValidation;

namespace Application.SalesPerformanceTeam
{
    public class SPTValidator : AbstractValidator<Domain.SalesPerformanceTeam>
    {
        public SPTValidator()
        {
            RuleFor(x => x.SPTDate).NotEmpty();
            RuleFor(x => x.SPTSellerId).NotEmpty();
            RuleFor(x => x.SPTProjectId).NotEmpty();
            RuleFor(x => x.SPTEventId).NotEmpty();
        }
    }
}