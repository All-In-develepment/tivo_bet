using FluentValidation;

namespace Application.Events
{
    public class EventValidator : AbstractValidator<Domain.Events>
    {
        public EventValidator()
        {
            RuleFor(x => x.EventName).NotEmpty();
        }
    }
}