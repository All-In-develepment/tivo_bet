using Application.Core;
using AutoMapper;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Seller
{
    public class EditSeller
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Domain.Seller Seller { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Seller).SetValidator(new SellerValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {

            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var seller = await _context.Sellers.FindAsync(request.Seller.SellerId);

                if (seller == null) return null;

                _mapper.Map(request.Seller, seller);
                seller.SellerName = request.Seller.SellerName;
                seller.SellerIsActive = request.Seller.SellerIsActive;
                seller.ProjectId = request.Seller.ProjectId;


                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to update seller");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}