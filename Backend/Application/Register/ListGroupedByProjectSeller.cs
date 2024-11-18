using Application.Core;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Register
{
  public class ListGroupedByProjectSeller
  {
    public class Query : IRequest<Result<PagedList<RegisterDto>>>
    {
      public RegisterParams Params { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<PagedList<RegisterDto>>>
    {
      private readonly DataContext _context;
      private readonly IMapper _mapper;

      public Handler(DataContext context, IMapper mapper)
      {
        _mapper = mapper;
        _context = context;
      }

      public async Task<Result<PagedList<RegisterDto>>> Handle(Query request, CancellationToken cancellationToken)
      {
        var query = _context.Registers
          .Include(r => r.Project)
          .Where(x => x.RegisterDate >= request.Params.StartDate)
          .OrderBy(d => d.RegisterDate)
          .GroupBy(x => x.SellerId)
          .Select(g => new RegisterDto
          {
            SellerId = g.Key,
            RegisterTotal = g.Sum(x => x.RegisterTotal),
            RegisterId = g.Max(x => x.RegisterId),
            RegisterLeads = g.Sum(x => x.RegisterLeads),
            RegisterAVGConversion = g.Average(x => x.RegisterAVGConversion),
            RegisterAVG = g.Average(x => x.RegisterAVG),
            RegisterAmount = g.Sum(x => x.RegisterAmount),
            RegisterDate = g.Max(x => x.RegisterDate),
            SellerName = g.Max(x => x.Seller.SellerName)
          })
          .AsQueryable();

        return Result<PagedList<RegisterDto>>
          .Success(await PagedList<RegisterDto>.CreateAsync(query,
            request.Params.PageNumber, request.Params.PageSize));
      }
    }
  }
}