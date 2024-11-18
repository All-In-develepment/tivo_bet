using Application.Bookmakers;
using Application.Core;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BookmakerController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetBookmakers([FromQuery] PagingParams param)
        {
            // Retorna a lista de bookmakers
            return HandlePagedResult(await Mediator.Send(new ListBookmaker.Query{Params = param}));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookmaker(Guid id)
        {
            // Retorna um bookmaker
            return HandleResult<BookmakerDto>(await Mediator.Send(new DetailBookmaker.Query{Id = id}));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBookmaker(Bookmaker bookmaker)
        {
            // Cria um novo bookmaker
            return HandleResult(await Mediator.Send(new CreateBookmaker.Command { Bookmaker = bookmaker }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditBookmaker(Guid id, Bookmaker bookmaker)
        {
            // Edita um bookmaker
            bookmaker.BookmakerId = id;
            return HandleResult(await Mediator.Send(new EditBookmaker.Command { Bookmaker = bookmaker }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookmaker(Guid id)
        {
            // Deleta um bookmaker
            return HandleResult(await Mediator.Send(new DeleteBookmaker.Command { Id = id }));
        }
    }
}