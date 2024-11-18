using Application.Core;
using Application.Events;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class EventsController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetEvents([FromQuery] PagingParams param)
        {
            return HandlePagedResult(await Mediator.Send(new ListEvent.Query { Params = param }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(Guid id)
        {
            return HandleResult(await Mediator.Send(new DetailsEvent.Query { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(Events even)
        {
            return HandleResult(await Mediator.Send(new CreateEvent.Command { Event = even }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditEvent(Guid id, Events even)
        {
            even.EventsId = id;
            return HandleResult(await Mediator.Send(new EditEvent.Command { Event = even }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            return HandleResult(await Mediator.Send(new DeleteEvent.Command { Id = id }));
        }
    }
}