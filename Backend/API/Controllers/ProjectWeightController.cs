using Application.Core;
using Application.ProjectWeight;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class ProjectWeightController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetProjectWeights([FromQuery] PagingParams param)
        {
            return HandlePagedResult(await Mediator.Send(new ListProjectWeight.Query { Params = param }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProjectWeight(ProjectWeight projectWeight)
        {
            return HandleResult(await Mediator.Send(new CreateProjectWeight.Command { ProjectWeight = projectWeight }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditProjectWeight(Guid id, ProjectWeight projectWeight)
        {
            projectWeight.ProjectWeightId = id;
            return HandleResult(await Mediator.Send(new EditProejctWeight.Command { ProjectWeight = projectWeight }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectWeight(Guid id)
        {
            return HandleResult(await Mediator.Send(new DetailsProjectWeight.Query { Id = id }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectWeight(Guid id)
        {
            return HandleResult(await Mediator.Send(new DeleteProjectWeight.Command { Id = id }));
        }

        [HttpGet("normalize")]
        public async Task<IActionResult> NormalizeProjectWeight([FromQuery] PagingParams param, DateTime initialDate, DateTime finalDate)
        {
            return HandlePagedResult(await Mediator.Send(new NormalizeProjectWeight.Query { Params = param, InitialDate = initialDate, FinalDate = finalDate }));
        }
    }
}