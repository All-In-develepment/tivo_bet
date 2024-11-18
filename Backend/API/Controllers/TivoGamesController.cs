using Application.Core;
using Application.TivoGame;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [AllowAnonymous]
    public class TivoGamesController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetTivoGames([FromQuery] PagingParams param)
        {
            return HandlePagedResult(await Mediator.Send(new ListTivoGames.Query { Params = param }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTivoGame(Domain.TivoGame tivoGame)
        {
            return HandleResult(await Mediator.Send(new CreateTivoGame.Command { TivoGame = tivoGame }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTivoGame(string id)
        {
            return HandleResult(await Mediator.Send(new DetailTivoGame.Query { Id = id }));
        }
    }
}