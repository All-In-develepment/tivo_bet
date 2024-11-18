using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Sales
{
    public class SaleListGroupBySeller
    {
        public class Query : IRequest<Result<PagedList<SaleDto>>>
        {
            public SaleParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<SaleDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<PagedList<SaleDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = _context.Sales
                    .Include(s => s.Seller)
                    .Select(s => new SaleDto
                    {
                        SaleId = s.SaleId,
                        SalePrice = s.SalePrice,
                        SaleDate = s.SaleDate,
                        SellerId = s.SellerId,
                        SellerName = s.Seller.SellerName,
                        ProjectId = s.Seller.ProjectId,
                    })
                    .Where(x => x.SaleDate >= request.Params.StartDate)
                    .OrderBy(d => d.SaleDate)
                    .GroupBy(x => x.SellerId)
                    .Select(g => new SaleDto
                    {
                        SellerId = g.Key,
                        SalePrice = g.Sum(x => x.SalePrice),
                        SellerName = g.Max(x => x.SellerName),
                        ProjectId = g.Max(x => x.ProjectId),
                        SaleDate = g.Max(x => x.SaleDate),
                        SaleId = g.Max(x => x.SaleId)
                    })
                    .AsQueryable();

                return Result<PagedList<SaleDto>>
                    .Success(await PagedList<SaleDto>.CreateAsync(query,
                        request.Params.PageNumber, request.Params.PageSize));
            }
        }
    }
}