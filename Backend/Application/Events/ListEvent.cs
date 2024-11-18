using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Persistence;

namespace Application.Events
{
    public class ListEvent
    {
        public class Query : IRequest<Result<PagedList<EventDto>>>
        {
            public PagingParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<EventDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<PagedList<EventDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = _context.Events
                    .OrderBy(d => d.EventName)
                    .ProjectTo<EventDto>(_mapper.ConfigurationProvider)
                    .AsQueryable();

                return Result<PagedList<EventDto>>
                    .Success(await PagedList<EventDto>.CreateAsync(query,
                        request.Params.PageNumber, request.Params.PageSize));
            }
        }
    }
}