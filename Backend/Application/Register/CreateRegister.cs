using Application.Core;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Register
{
    public class CreateRegister
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Domain.Register Register { get; set; }
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
                    RuleFor(x => x.Register).SetValidator(new RegisterValidator());
                }
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                request.Register.RegisterAVG = request.Register.RegisterAmount / request.Register.RegisterTotal;
                // request.Register.RegisterAVGConversion = request.Register.RegisterTotal / request.Register.RegisterLeads;
                request.Register.RegisterAVGConversion = ((float)request.Register.RegisterTotal / (float)request.Register.RegisterLeads) * 100;
                _context.Registers.Add(request.Register);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to create register");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}