using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Seller
{
    public class DetailsSeller
    {
        public class Query : IRequest<Result<SellerDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<SellerDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }
            public async Task<Result<SellerDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var seller = await _context.Sellers
                    .ProjectTo<SellerDto>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync(x => x.SellerId == request.Id);

                if (seller == null) return null;

                return Result<SellerDto>.Success(seller);
            }
        }
    }
}