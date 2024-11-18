using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Persistence;

namespace Application.Project;
public class ListActiveProjects
{
    public class Query : IRequest<Result<PagedList<ProjectDto>>>
    {
        public PagingParams Params { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<PagedList<ProjectDto>>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public Handler(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<PagedList<ProjectDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _context.Projects
                .Where(x => x.ProjectIsActive == true)
                .OrderBy(d => d.ProjectName)
                .ProjectTo<ProjectDto>(_mapper.ConfigurationProvider)
                .AsQueryable();

            return Result<PagedList<ProjectDto>>
                .Success(await PagedList<ProjectDto>.CreateAsync(query,
                    request.Params.PageNumber, request.Params.PageSize));
        }
    }
}