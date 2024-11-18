using Domain;
using FluentValidation;

namespace Application.Bookmakers
{
    public class BookmakerValidator : AbstractValidator<Bookmaker>
    {
        public BookmakerValidator()
        {
            RuleFor(x => x.BookmakerName).NotEmpty();
        }
    }
}