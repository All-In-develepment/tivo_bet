using Application.Core;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Products
{
    public class CreateProduct
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Domain.Product Product { get; set; }
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
                    RuleFor(x => x.Product).SetValidator(new ProductValidator());
                }
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                _context.Products.Add(request.Product);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to create product");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}