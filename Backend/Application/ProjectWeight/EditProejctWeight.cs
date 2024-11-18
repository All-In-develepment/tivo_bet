using Application.Core;
using AutoMapper;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.ProjectWeight
{
    public class EditProejctWeight
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Domain.ProjectWeight ProjectWeight { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.ProjectWeight).SetValidator(new ProjectWeightValidator());
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
                var projectWeight = await _context.ProjectWeights.FindAsync(request.ProjectWeight.ProjectWeightId);

                if (projectWeight == null) return null;

                _mapper.Map(request.ProjectWeight, projectWeight);
                projectWeight.ProjectId = request.ProjectWeight.ProjectId;
                projectWeight.Month = request.ProjectWeight.Month;
                projectWeight.SalesValueWeight = request.ProjectWeight.SalesValueWeight;
                projectWeight.ConversionWeight = request.ProjectWeight.ConversionWeight;
                projectWeight.RegistrationWeight = request.ProjectWeight.RegistrationWeight;
                projectWeight.DepositWeight = request.ProjectWeight.DepositWeight;
                projectWeight.UpdatedAt = DateTime.Now;

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to update project weight");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}