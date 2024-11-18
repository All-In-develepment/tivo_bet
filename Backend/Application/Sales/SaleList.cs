using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Sales
{
    public class SaleList
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
                    .Include(p => p.Product)
                    .Include(c => c.Project)
                    .Select(s => new SaleDto
                    {
                        SaleId = s.SaleId,
                        SalePrice = s.SalePrice,
                        SaleDate = s.SaleDate,
                        SellerId = s.SellerId,
                        SellerName = s.Seller.SellerName,
                        ProjectId = s.Project.ProjectId,
                        ProjectName = s.Project.ProjectName,
                        ProductId = s.Product.ProductId,
                        ProductName = s.Product.ProductName
                    })
                    .Where(x => x.SaleDate >= request.Params.StartDate)
                    .OrderBy(d => d.SaleDate)
                    .ProjectTo<SaleDto>(_mapper.ConfigurationProvider)
                    .AsQueryable();

                return Result<PagedList<SaleDto>>
                    .Success(await PagedList<SaleDto>.CreateAsync(query,
                        request.Params.PageNumber, request.Params.PageSize));
            }
        }
    }
}