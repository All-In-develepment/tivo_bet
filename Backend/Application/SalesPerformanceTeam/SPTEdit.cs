using Application.Core;
using AutoMapper;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.SalesPerformanceTeam
{
    public class SPTEdit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Domain.SalesPerformanceTeam SalesPerformanceTeam { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.SalesPerformanceTeam).SetValidator(new SPTValidator());
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
                var salesPerformanceTeam = await _context.SalesPerformanceTeams.FindAsync(request.SalesPerformanceTeam.SPTId);

                if (salesPerformanceTeam == null) return null;

                _mapper.Map(request.SalesPerformanceTeam, salesPerformanceTeam);
                salesPerformanceTeam.SPTDate = request.SalesPerformanceTeam.SPTDate;
                salesPerformanceTeam.SPTTotalLeads = request.SalesPerformanceTeam.SPTTotalLeads;
                salesPerformanceTeam.SPTTotalSales = request.SalesPerformanceTeam.SPTTotalSales;
                salesPerformanceTeam.SPTTotalSalesAmont = request.SalesPerformanceTeam.SPTTotalSalesAmont;
                salesPerformanceTeam.SPTTotalRegister = request.SalesPerformanceTeam.SPTTotalRegister;
                salesPerformanceTeam.SPTTotalRegisterAmont = request.SalesPerformanceTeam.SPTTotalRegisterAmont;
                salesPerformanceTeam.SPTTotalRedeposit = request.SalesPerformanceTeam.SPTTotalRedeposit;
                salesPerformanceTeam.SPTTotalRedepositAmont = request.SalesPerformanceTeam.SPTTotalRedepositAmont;
                salesPerformanceTeam.SPTBookmakerId = request.SalesPerformanceTeam.SPTBookmakerId;
                salesPerformanceTeam.SPTSellerId = request.SalesPerformanceTeam.SPTSellerId;
                salesPerformanceTeam.SPTProjectId = request.SalesPerformanceTeam.SPTProjectId;
                salesPerformanceTeam.SPTEventId = request.SalesPerformanceTeam.SPTEventId;
                salesPerformanceTeam.SPTUpdatedAt = request.SalesPerformanceTeam.SPTUpdatedAt;
                // 
                if (request.SalesPerformanceTeam.SPTTotalLeads != 0)
                {
                    if (request.SalesPerformanceTeam.SPTTotalSales != 0)
                    {
                        request.SalesPerformanceTeam.SPTAVGSales = request.SalesPerformanceTeam.SPTTotalLeads / request.SalesPerformanceTeam.SPTTotalSales;
                    }

                    if (request.SalesPerformanceTeam.SPTTotalRegister != 0)
                    {
                        request.SalesPerformanceTeam.SPTAVGRegister = request.SalesPerformanceTeam.SPTTotalLeads / request.SalesPerformanceTeam.SPTTotalRegister;
                    }
                    
                    if (request.SalesPerformanceTeam.SPTTotalRedeposit != 0)
                    {
                        request.SalesPerformanceTeam.SPTAVGRedeposit = request.SalesPerformanceTeam.SPTTotalLeads / request.SalesPerformanceTeam.SPTTotalRedeposit;
                    }
                    
                    var somaTotal = request.SalesPerformanceTeam.SPTTotalSales + request.SalesPerformanceTeam.SPTTotalRegister + request.SalesPerformanceTeam.SPTTotalRedeposit;
                    request.SalesPerformanceTeam.SPTAVGConvertion = ((float)somaTotal / request.SalesPerformanceTeam.SPTTotalLeads)*100;
                } else {
                    request.SalesPerformanceTeam.SPTAVGSales = 0;
                    request.SalesPerformanceTeam.SPTAVGRegister = 0;
                    request.SalesPerformanceTeam.SPTAVGRedeposit = 0;
                    request.SalesPerformanceTeam.SPTAVGConvertion = 0;
                }

                if (request.SalesPerformanceTeam.SPTTotalSales != 0)
                {
                    request.SalesPerformanceTeam.SPTAVGSalesAmont = request.SalesPerformanceTeam.SPTTotalSalesAmont / request.SalesPerformanceTeam.SPTTotalSales;
                } else {
                    request.SalesPerformanceTeam.SPTAVGSalesAmont = 0;
                }

                if (request.SalesPerformanceTeam.SPTTotalRegister != 0)
                {
                    request.SalesPerformanceTeam.SPTAVGRegisterAmont = request.SalesPerformanceTeam.SPTTotalRegisterAmont / request.SalesPerformanceTeam.SPTTotalRegister;
                } else {
                    request.SalesPerformanceTeam.SPTAVGRegisterAmont = 0;
                }

                if (request.SalesPerformanceTeam.SPTTotalRedeposit != 0)
                {
                    request.SalesPerformanceTeam.SPTAVGRedepositAmont = request.SalesPerformanceTeam.SPTTotalRedepositAmont / request.SalesPerformanceTeam.SPTTotalRedeposit;
                } else {
                    request.SalesPerformanceTeam.SPTAVGRedepositAmont = 0;
                }
                request.SalesPerformanceTeam.SPTUpdatedAt = DateTime.Now;

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to update sales performance team");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}