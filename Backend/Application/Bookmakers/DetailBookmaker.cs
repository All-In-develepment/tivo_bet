using Application.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Bookmakers
{
    public class DetailBookmaker
    {
        public class Query : IRequest<Result<BookmakerDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<BookmakerDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }
            public async Task<Result<BookmakerDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var bookmaker = await _context.Bookmakers
                    .ProjectTo<BookmakerDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(x => x.BookmakerId == request.Id);

                return Result<BookmakerDto>.Success(bookmaker);
            }
        }
    }
}