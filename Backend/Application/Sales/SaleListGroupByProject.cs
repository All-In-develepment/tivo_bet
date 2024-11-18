using Application.Core;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Sales
{
    public class SaleListGroupByProject
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
                    .Where(x => x.SaleDate >= request.Params.StartDate)
                    .OrderBy(d => d.SaleDate)
                    .GroupBy(x => x.ProjectId)
                    .Select(g => new SaleDto
                    {
                        ProjectId = g.Key,
                        SalePrice = g.Sum(x => x.SalePrice),
                        SellerName = g.Max(x => x.Seller.SellerName),
                        SellerId = g.Max(x => x.SellerId),
                        SaleDate = g.Max(x => x.SaleDate),
                        SaleId = g.Max(x => x.SaleId),
                        ProjectName = g.Max(x => x.Project.ProjectName)
                    })
                    .AsQueryable();

                return Result<PagedList<SaleDto>>
                    .Success(await PagedList<SaleDto>.CreateAsync(query,
                        request.Params.PageNumber, request.Params.PageSize));
            }
        }
    }
}