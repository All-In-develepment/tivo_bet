using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Register
{
    public class DetailsRegister
    {
        public class Query : IRequest<Result<RegisterDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<RegisterDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<RegisterDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var register = await _context.Registers
                    .ProjectTo<RegisterDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(x => x.RegisterId == request.Id);

                return Result<RegisterDto>.Success(register);
            }
        }
    }
}