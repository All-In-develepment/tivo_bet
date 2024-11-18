using Application.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Events
{
    public class DetailsEvent
    {
        public class Query : IRequest<Result<EventDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<EventDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }
            public async Task<Result<EventDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var eventDetail = await _context.Events
                    .ProjectTo<EventDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(x => x.EventsId == request.Id);

                return Result<EventDto>.Success(eventDetail);
            }
        }
    }
}