using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Project
{
    public class DetailsProject
    {
        public class Query : IRequest<Result<ProjectDto>>
        {
            public Guid Id { get; set; }
        }
        
        public class Handler : IRequestHandler<Query, Result<ProjectDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }
            public async Task<Result<ProjectDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var project = await _context.Projects
                    .ProjectTo<ProjectDto>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync(x => x.ProjectId == request.Id);

                if (project == null) return null;

                return Result<ProjectDto>.Success(project);
            }
        }
    }
}