using Application.Core;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Bookmakers
{
    public class CreateBookmaker
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Bookmaker Bookmaker { get; set; }
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
                    RuleFor(x => x.Bookmaker).SetValidator(new BookmakerValidator());
                }
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                _context.Bookmakers.Add(request.Bookmaker);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to create bookmakers");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}