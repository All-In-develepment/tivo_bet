using Application.Core;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Events
{
    public class EditEvent
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Domain.Events Event { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Event).SetValidator(new EventValidator());
            }
        }
        
        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var eventEdit = await _context.Events.FindAsync(request.Event.EventsId);

                if (eventEdit == null) return null;

                _mapper.Map(request.Event, eventEdit);
                eventEdit.EventName = request.Event.EventName;
                eventEdit.EventDescription = request.Event.EventDescription;

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to update event");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}