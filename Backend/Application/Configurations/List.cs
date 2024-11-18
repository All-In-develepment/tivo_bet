using Application.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Config
{
    public class List
    {
        public class Query : IRequest<Result<List<ConfigurationDto>>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<List<ConfigurationDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IMapper mapper, IUserAccessor userAccessor)
            {
                _mapper = mapper;
                _context = context;
                _userAccessor = userAccessor;
            }

            public async Task<Result<List<ConfigurationDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                // var config = await _context.RunConfigs
                //     .ProjectTo<ConfigurationDto>(_mapper.ConfigurationProvider)
                //     .ToListAsync();

                return Result<List<ConfigurationDto>>.Success(await _context.Configurations
                    .ProjectTo<ConfigurationDto>(_mapper.ConfigurationProvider)
                    .ToListAsync());
            }
        }
    }
}