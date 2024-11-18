using Domain;
using FluentValidation;

namespace Application.Config
{
    //Validador de entidade Config
    public class ConfigValidator : AbstractValidator<Configuration>
    {
        public ConfigValidator()
        {
            RuleFor(x => x.ConfigurationName).NotEmpty();
            RuleFor(x => x.ConfigurationValue).NotEmpty();
        }
    }
}