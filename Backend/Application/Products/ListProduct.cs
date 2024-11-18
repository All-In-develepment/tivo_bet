using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Persistence;

namespace Application.Products
{
    public class ListProduct
    {
        public class Query : IRequest<Result<PagedList<ProductDto>>>
        {
            public PagingParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<ProductDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<PagedList<ProductDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = _context.Products
                    .OrderBy(d => d.ProductName)
                    .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                    .AsQueryable();

                return Result<PagedList<ProductDto>>
                    .Success(await PagedList<ProductDto>.CreateAsync(query,
                        request.Params.PageNumber, request.Params.PageSize));
            }
        }
    }
}