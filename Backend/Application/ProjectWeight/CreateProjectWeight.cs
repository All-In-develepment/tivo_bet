using Application.Core;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.ProjectWeight
{
    public class CreateProjectWeight
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Domain.ProjectWeight ProjectWeight { get; set; }
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
                    RuleFor(x => x.ProjectWeight).SetValidator(new ProjectWeightValidator());
                }
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                request.ProjectWeight.CreatedAt = request.ProjectWeight.UpdatedAt = DateTime.Now;
                _context.ProjectWeights.Add(request.ProjectWeight);

                var result = false;

                try {
                    result = await _context.SaveChangesAsync() > 0;
                } catch (Exception ex) {
                    var innerException = ex.InnerException;
                    Console.WriteLine(innerException);
                }

                if (!result) return Result<Unit>.Failure("Failed to create project weight");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}