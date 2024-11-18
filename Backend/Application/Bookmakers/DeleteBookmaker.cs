using Application.Core;
using MediatR;
using Persistence;

namespace Application.Bookmakers
{
    public class DeleteBookmaker
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
                var bookmaker = await _context.Bookmakers.FindAsync(request.Id);

                if (bookmaker == null) return null;

                _context.Remove(bookmaker);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to delete the bookmaker");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}