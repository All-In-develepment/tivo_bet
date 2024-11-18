using Application.Core;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Seller
{
    public class CreateSeller
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Domain.Seller Seller { get; set; }
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
                    RuleFor(x => x.Seller).SetValidator(new SellerValidator());
                }
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                _context.Sellers.Add(request.Seller);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to create seller");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}