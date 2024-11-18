using Application.Core;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Config
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Configuration Config { get; set; }
        }

        //Validador de comandos
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Config).SetValidator(new ConfigValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;

            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var config = await _context.Configurations.FindAsync(request.Config.ConfigurationId);

                if (config == null) return null;

                config.ConfigurationName = request.Config.ConfigurationName ?? config.ConfigurationName;
                config.ConfigurationValue = request.Config.ConfigurationValue ?? config.ConfigurationValue;

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to update config");

                return Result<Unit>.Success(Unit.Value);
            }
        }

    }
}