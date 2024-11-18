using Application.Core;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Products
{
    public class EditProduct
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Product Product { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Product).SetValidator(new ProductValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var product = await _context.Products.FindAsync(request.Product.ProductId);

                if (product == null) return null;

                _mapper.Map(request.Product, product);
                product.ProductName = request.Product.ProductName;
                product.ProdutcDescription = request.Product.ProdutcDescription;
                product.ProductIsActive = request.Product.ProductIsActive;
                product.ProductPrice = request.Product.ProductPrice;

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to update product");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}