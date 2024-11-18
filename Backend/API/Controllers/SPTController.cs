using Application.Core;
using Application.SalesPerformanceTeam;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class SPT : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> CreateSPT(SalesPerformanceTeam spt)
        {
            return HandleResult(await Mediator.Send(new CreateSPT.Command { SPT = spt }));
        }

        [HttpGet]
        public async Task<IActionResult> GetSPT([FromQuery] SPTParams param)
        {
            return HandlePagedResult(await Mediator.Send(new ListSPT.Query { Params = param }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSPTById(int id)
        {
            return HandleResult(await Mediator.Send(new DetailsSalesPerformaceTeam.Query { Id = id }));
        }

        [HttpGet("bySeller")]
        public async Task<IActionResult> GetSPTBySeller([FromQuery] SPTParams param)
        {
            return HandlePagedResult(await Mediator.Send(new ListGroupedBySeller.Query { Params = param }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditSPT(int id, SalesPerformanceTeam spt)
        {
            spt.SPTId = id;
            return HandleResult(await Mediator.Send(new SPTEdit.Command { SalesPerformanceTeam = spt }));
        }
    }
}