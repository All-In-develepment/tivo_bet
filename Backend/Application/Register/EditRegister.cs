using Application.Core;
using AutoMapper;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Register
{
    public class EditRegister
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Domain.Register Register { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Register).SetValidator(new RegisterValidator());
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
                var register = await _context.Registers.FindAsync(request.Register.RegisterId);

                if (register == null) return null;

                _mapper.Map(request.Register, register);
                register.RegisterDate = request.Register.RegisterDate;
                register.RegisterTotal = request.Register.RegisterTotal;
                register.RegisterAmount = request.Register.RegisterAmount;
                register.BookmakerId = request.Register.BookmakerId;
                register.EventsId = request.Register.EventsId;
                register.SellerId = request.Register.SellerId;
                register.EventsId = request.Register.EventsId;

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to update register");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}