using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Persistence;

namespace Application.TivoGame
{
    public class ListTivoGames
    {
        public class Query : IRequest<Result<PagedList<TivoGameDto>>>
        {
            public PagingParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<TivoGameDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<PagedList<TivoGameDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = _context.TivoGames
                    .ProjectTo<TivoGameDto>(_mapper.ConfigurationProvider)
                    .AsQueryable();

                return Result<PagedList<TivoGameDto>>
                    .Success(await PagedList<TivoGameDto>.CreateAsync(query,
                        request.Params.PageNumber, request.Params.PageSize));
            }
        }
    }
}