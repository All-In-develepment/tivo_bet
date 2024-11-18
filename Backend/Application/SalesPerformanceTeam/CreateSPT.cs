using Application.Core;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.SalesPerformanceTeam
{
    public class CreateSPT
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Domain.SalesPerformanceTeam SPT { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public class CommandValidator : AbstractValidator<Command>
            {
                public CommandValidator()
                {
                    RuleFor(x => x.SPT).SetValidator(new SPTValidator());
                }
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request.SPT.SPTTotalLeads != 0)
                {
                    if (request.SPT.SPTTotalSales != 0)
                    {
                        request.SPT.SPTAVGSales = request.SPT.SPTTotalLeads / request.SPT.SPTTotalSales;
                    }

                    if (request.SPT.SPTTotalRegister != 0)
                    {
                        request.SPT.SPTAVGRegister = request.SPT.SPTTotalLeads / request.SPT.SPTTotalRegister;
                    }
                    
                    if (request.SPT.SPTTotalRedeposit != 0)
                    {
                        request.SPT.SPTAVGRedeposit = request.SPT.SPTTotalLeads / request.SPT.SPTTotalRedeposit;
                    }
                    
                    var somaTotal = request.SPT.SPTTotalSales + request.SPT.SPTTotalRegister + request.SPT.SPTTotalRedeposit;
                    request.SPT.SPTAVGConvertion = ((float)somaTotal / request.SPT.SPTTotalLeads)*100;
                } else {
                    request.SPT.SPTAVGSales = 0;
                    request.SPT.SPTAVGRegister = 0;
                    request.SPT.SPTAVGRedeposit = 0;
                    request.SPT.SPTAVGConvertion = 0;
                }

                if (request.SPT.SPTTotalSales != 0)
                {
                    request.SPT.SPTAVGSalesAmont = request.SPT.SPTTotalSalesAmont / request.SPT.SPTTotalSales;
                } else {
                    request.SPT.SPTAVGSalesAmont = 0;
                }

                if (request.SPT.SPTTotalRegister != 0)
                {
                    request.SPT.SPTAVGRegisterAmont = request.SPT.SPTTotalRegisterAmont / request.SPT.SPTTotalRegister;
                } else {
                    request.SPT.SPTAVGRegisterAmont = 0;
                }

                if (request.SPT.SPTTotalRedeposit != 0)
                {
                    request.SPT.SPTAVGRedepositAmont = request.SPT.SPTTotalRedepositAmont / request.SPT.SPTTotalRedeposit;
                } else {
                    request.SPT.SPTAVGRedepositAmont = 0;
                }
                request.SPT.SPTCreatedAt = DateTime.Now;
                request.SPT.SPTUpdatedAt = DateTime.Now;

                _context.SalesPerformanceTeams.Add(request.SPT);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to create SPT");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}