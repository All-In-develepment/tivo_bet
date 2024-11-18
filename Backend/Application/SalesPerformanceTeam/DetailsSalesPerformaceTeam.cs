using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.SalesPerformanceTeam
{
    public class DetailsSalesPerformaceTeam
    {
        public class Query : IRequest<Result<SalesPerformaceTeamDto>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<SalesPerformaceTeamDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }
            public async Task<Result<SalesPerformaceTeamDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var spt = await _context.SalesPerformanceTeams
                    .ProjectTo<SalesPerformaceTeamDto>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync(x => x.SPTId == request.Id);

                if (spt == null) return null;

                return Result<SalesPerformaceTeamDto>.Success(spt);
            }
        }
    }
}