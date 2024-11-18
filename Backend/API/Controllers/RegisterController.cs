using Application.Core;
using Application.Register;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class RegisterController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetRegisters([FromQuery] PagingParams param)
        {
            return HandlePagedResult(await Mediator.Send(new ListRegister.Query { Params = param }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegister(Guid id)
        {
            return HandleResult(await Mediator.Send(new DetailsRegister.Query { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateRegister(Domain.Register register)
        {
            return HandleResult(await Mediator.Send(new CreateRegister.Command { Register = register }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditRegister(Guid id, Domain.Register register)
        {
            register.RegisterId = id;
            return HandleResult(await Mediator.Send(new EditRegister.Command { Register = register }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegister(Guid id)
        {
            return HandleResult(await Mediator.Send(new DeleteRegister.Command { Id = id }));
        }

        [HttpGet("grouped-by-project")]
        public async Task<IActionResult> GetRegistersGroupedByProject([FromQuery] RegisterParams param)
        {
            return HandlePagedResult(await Mediator.Send(new ListGroupedByProjectRegister.Query { Params = param }));
        }

        [HttpGet("grouped-by-seller")]
        public async Task<IActionResult> GetRegistersGroupedBySeller([FromQuery] RegisterParams param)
        {
            return HandlePagedResult(await Mediator.Send(new ListGroupedByProjectSeller.Query { Params = param }));
        }
    }
}