using FluentValidation;

namespace Application.Project
{
    public class ProjectValidator : AbstractValidator<Domain.Project>
    {
        public ProjectValidator()
        {
            RuleFor(x => x.ProjectName).NotEmpty();
            RuleFor(x => x.ProjectIsActive).NotNull();
        }
    }
}