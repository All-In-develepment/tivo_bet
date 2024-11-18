using Application.Core;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.TivoGame
{
    public class CreateTivoGame
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Domain.TivoGame TivoGame { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public class CommandValidator : AbstractValidator<Command>
            {
                public CommandValidator()
                {
                    RuleFor(x => x.TivoGame).SetValidator(new TivoGameValidator());
                }
            } 

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                _context.TivoGames.Add(request.TivoGame);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to create TivoGame");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}