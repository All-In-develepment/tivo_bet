using Application.Core;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Bookmakers
{
    public class EditBookmaker
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Bookmaker Bookmaker { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Bookmaker).SetValidator(new BookmakerValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }


            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var bookmaker = await _context.Bookmakers.FindAsync(request.Bookmaker.BookmakerId);

                if (bookmaker == null) return null;

                _mapper.Map(request.Bookmaker, bookmaker);
                bookmaker.BookmakerName = request.Bookmaker.BookmakerName;

                var result = await _context.SaveChangesAsync() > 0;

                // if (!result) return Result<Unit>.Failure(Unit.Value);
                // verifica se houve alteração no banco de dados e s enão houve retorna o motivo do erro
                if (!result) return Result<Unit>.Failure("Failed to update bookmaker");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}