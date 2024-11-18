using Application.Core;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.SalesPerformanceTeam
{
    public class ReakByConversion
    {
        public class Query : IRequest<Result<PagedList<SalesPerformaceTeamDto>>>
        {
            public SPTParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<SalesPerformaceTeamDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }
            public async Task<Result<PagedList<SalesPerformaceTeamDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = _context.SalesPerformanceTeams
                    .Include(s => s.SPTSeller)
                    .Include(b => b.SPTProject)
                    .Include(p => p.SPTEvent)
                    .Select(spt => new SalesPerformaceTeamDto
                    {
                        SPTId = spt.SPTId,
                        SPTDate = spt.SPTDate,
                        SPTTotalLeads = spt.SPTTotalLeads,
                        SPTTotalSales = spt.SPTTotalSales,
                        SPTCreatedAt = spt.SPTCreatedAt,
                        SPTUpdatedAt = spt.SPTUpdatedAt,
                        SPTSellerId = spt.SPTSellerId,
                        SPTSellerName = spt.SPTSeller.SellerName,
                        SPTProjectId = spt.SPTProjectId,
                        SPTProjectName = spt.SPTProject.ProjectName,
                        SPTEventId = spt.SPTEventId,
                        SPTEventName = spt.SPTEvent.EventName,
                        SPTBookmakerId = spt.SPTBookmakerId,
                        SPTBookmakerName = spt.SPTBookmaker.BookmakerName,
                        SPTTotalSalesAmont = spt.SPTTotalSalesAmont,
                        SPTAVGSales = spt.SPTAVGSales,
                        SPTAVGSalesAmont = spt.SPTAVGSalesAmont,
                        SPTTotalRegister = spt.SPTTotalRegister,
                        SPTTotalRegisterAmont = spt.SPTTotalRegisterAmont,
                        SPTAVGRegister = spt.SPTAVGRegister,
                        SPTAVGRegisterAmont = spt.SPTAVGRegisterAmont,
                        SPTTotalRedeposit = spt.SPTTotalRedeposit,
                        SPTTotalRedepositAmont = spt.SPTTotalRedepositAmont,
                        SPTAVGRedeposit = spt.SPTAVGRedeposit,
                        SPTAVGRedepositAmont = spt.SPTAVGRedepositAmont,
                        SPTAVGConvertion = spt.SPTAVGConvertion
                    })
                    .Where(x => x.SPTDate >= request.Params.StartDate && x.SPTDate <= request.Params.EndDate)
                    .OrderBy(d => d.SPTDate)
                    .GroupBy(x => x.SPTSellerId)
                    .Select(g => new SalesPerformaceTeamDto
                    {
                        SPTSellerId = g.Key,
                        SPTTotalSales = g.Sum(x => x.SPTTotalSales),
                        SPTSellerName = g.Max(x => x.SPTSellerName),
                        SPTProjectId = g.Max(x => x.SPTProjectId),
                        SPTDate = g.Max(x => x.SPTDate),
                        SPTId = g.Max(x => x.SPTId)
                    })
                    .AsQueryable();

                return Result<PagedList<SalesPerformaceTeamDto>>
                    .Success(await PagedList<SalesPerformaceTeamDto>.CreateAsync(query,
                        request.Params.PageNumber, request.Params.PageSize));
            }
        }
    }
}