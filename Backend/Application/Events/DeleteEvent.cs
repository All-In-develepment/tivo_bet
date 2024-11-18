using Application.Core;
using MediatR;
using Persistence;

namespace Application.Events
{
    public class DeleteEvent
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
                var eventToDelete = await _context.Events.FindAsync(request.Id);

                if (eventToDelete == null) return null;

                _context.Remove(eventToDelete);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to delete the event");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}