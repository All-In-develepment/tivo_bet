using Application.Core;
using MediatR;
using Persistence;

namespace Application.Register
{
    public class DeleteRegister
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
                var register = await _context.Registers.FindAsync(request.Id);

                if (register == null) return null;

                _context.Remove(register);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to delete the register");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}