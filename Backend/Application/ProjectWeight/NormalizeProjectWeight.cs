using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.ProjectWeight
{
	public class NormalizeProjectWeight
	{
		public class Query : IRequest<Result<PagedList<ProjectWeightNormalizeDto>>>
		{
			public PagingParams Params { get; set; }
			public DateTime InitialDate { get; set; }
			public DateTime FinalDate { get; set; }
		}

		public class Handler : IRequestHandler<Query, Result<PagedList<ProjectWeightNormalizeDto>>>
		{
			private readonly DataContext _context;
			private readonly IMapper _mapper;
			public Handler(DataContext context, IMapper mapper)
			{
				_mapper = mapper;
				_context = context;
			}
			public async Task<Result<PagedList<ProjectWeightNormalizeDto>>> Handle(Query request, CancellationToken cancellationToken)
			{
				if (request.InitialDate == DateTime.MinValue)
				{
					Console.WriteLine("Data Inicial é nula ou igual a DateTime.MinValue");
					request.InitialDate = DateTime.Now.AddMonths(-1);
				}

				if (request.FinalDate == DateTime.MinValue)
				{
					request.FinalDate = DateTime.Now;
				}
				var query = _context.ProjectWeights
					.Where(d => d.Month >= request.InitialDate && d.Month <= request.FinalDate)
					.Include(p => p.Project)
					.Select(p => new ProjectWeightNormalizeDto
					{
						ProjectWeightId = p.ProjectWeightId,
						ProjectId = p.ProjectId,
						ProjectName = p.Project.ProjectName,
						Month = p.Month,
						SalesValueWeight = p.SalesValueWeight,
						ConversionWeight = p.ConversionWeight,
						RegistrationWeight = p.RegistrationWeight,
						DepositWeight = p.DepositWeight
					})
					.AsQueryable();

				var projectWeights = Result<PagedList<ProjectWeightNormalizeDto>>
									.Success(await PagedList<ProjectWeightNormalizeDto>
									.CreateAsync(query, request.Params.PageNumber, request.Params.PageSize));

				if (projectWeights == null) return null;

				// percorre a lista de pesos de projetos e adiciona o peso normalizado
				foreach (var item in projectWeights.Value)
				{
					var maxWeight = new List<decimal> { item.SalesValueWeight, item.ConversionWeight, item.RegistrationWeight, item.DepositWeight }.Max();
					item.SalesValueWeightNormalized = item.SalesValueWeight / maxWeight;
					item.ConversionWeightNormalized = item.ConversionWeight / maxWeight;
					item.RegistrationWeightNormalized = item.RegistrationWeight / maxWeight;
					item.DepositWeightNormalized = item.DepositWeight / maxWeight;
				}

				// foreach (var item in projectWeights.Value)
				// {
				// 	var maxWeight = item.Max(d => d.SalesValueWeight);
				// 	item. = item.Weight / maxWeight;
				// }

				return projectWeights;
			}
		}
	}
}