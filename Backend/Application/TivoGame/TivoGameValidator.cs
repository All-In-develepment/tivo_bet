using FluentValidation;

namespace Application.TivoGame
{
    public class TivoGameValidator : AbstractValidator<Domain.TivoGame>
    {
        public TivoGameValidator()
        {
            RuleFor(x => x.GameId).NotEmpty();
            RuleFor(x => x.HomeTeam).NotEmpty();
            RuleFor(x => x.AwayTeam).NotEmpty();
            RuleFor(x => x.HomeScore).NotNull();
            RuleFor(x => x.AwayScore).NotNull();
            RuleFor(x => x.GameDate).NotEmpty();
            RuleFor(x => x.GameTime).NotEmpty();
            RuleFor(x => x.FullTimeScore).NotEmpty();
            RuleFor(x => x.HalfTimeScore).NotEmpty();
            RuleFor(x => x.League).NotEmpty();
        }
    }
}