using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.ProjectWeight
{
    public class DetailsProjectWeight
    {
        public class Query : IRequest<Result<ProjectWeightDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler :  IRequestHandler<Query, Result<ProjectWeightDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }
            public async Task<Result<ProjectWeightDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var projectWeight = await _context.ProjectWeights
                    .ProjectTo<ProjectWeightDto>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync(x => x.ProjectWeightId == request.Id);

                if (projectWeight == null) return null;

                return Result<ProjectWeightDto>.Success(projectWeight);
            }
        }
    }
}