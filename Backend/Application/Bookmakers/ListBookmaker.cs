using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Persistence;

namespace Application.Bookmakers
{
    public class ListBookmaker
    {
        public class Query : IRequest<Result<PagedList<BookmakerDto>>>
        {
            public PagingParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<BookmakerDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }
            public async Task<Result<PagedList<BookmakerDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = _context.Bookmakers
                    .OrderBy(d => d.BookmakerName)
                    .ProjectTo<BookmakerDto>(_mapper.ConfigurationProvider)
                    .AsQueryable();

                return Result<PagedList<BookmakerDto>>
                    .Success(await PagedList<BookmakerDto>.CreateAsync(query,
                        request.Params.PageNumber, request.Params.PageSize));
            }
        }
    }
}