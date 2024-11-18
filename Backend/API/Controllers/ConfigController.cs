using Application.Config;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ConfigController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetConfigs()
        {
            // Retorna a lista de configurações
            return HandleResult<List<ConfigurationDto>>(await Mediator.Send(new List.Query{}));
        }

        [HttpPost]
        public async Task<IActionResult> CreateConfig(Configuration config)
        {
            // Cria uma nova configuração
            return HandleResult(await Mediator.Send(new Create.Command { Config = config }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditConfig(Guid id, Configuration config)
        {
            // Edita uma configuração
            config.ConfigurationId = id;
            return HandleResult(await Mediator.Send(new Edit.Command { Config = config }));
        }
    }
}