using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Persistence;

namespace Application.ProjectWeight
{
    public class ListProjectWeight
    {
        public class Query : IRequest<Result<PagedList<ProjectWeightDto>>>
        {
            public PagingParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<ProjectWeightDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }
            public async Task<Result<PagedList<ProjectWeightDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = _context.ProjectWeights
                    .OrderBy(d => d.Month)
                    .ProjectTo<ProjectWeightDto>(_mapper.ConfigurationProvider)
                    .AsQueryable();

                return Result<PagedList<ProjectWeightDto>>
                    .Success(await PagedList<ProjectWeightDto>.CreateAsync(query,
                        request.Params.PageNumber, request.Params.PageSize));
            }
        }
    }
}