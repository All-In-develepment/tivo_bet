using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.TivoGame
{
    public class DetailTivoGame
    {
        public class Query : IRequest<Result<TivoGameDto>>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<TivoGameDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext tivoGameRepository, IMapper mapper)
            {
                _context = tivoGameRepository;
                _mapper = mapper;
            }
            public async Task<Result<TivoGameDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var tivoGame = await _context.TivoGames
                    .ProjectTo<TivoGameDto>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync(x => x.GameId == request.Id);

                if (tivoGame == null) return null;

                return Result<TivoGameDto>.Success(tivoGame);
            }
        }
    }
}