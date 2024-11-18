using Application.Core;
using MediatR;
using Persistence;

namespace Application.ProjectWeight
{
    public class DeleteProjectWeight
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
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
                var projectWeight = await _context.ProjectWeights.FindAsync(request.Id);

                if (projectWeight == null) return null;

                _context.Remove(projectWeight);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to delete the project weight");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}