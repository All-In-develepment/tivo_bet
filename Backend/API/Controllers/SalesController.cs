using Application.Sales;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class SalesController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetSales([FromQuery] SaleParams param)
        {
            return HandlePagedResult(await Mediator.Send(new SaleList.Query { Params = param }));
        }

        [HttpGet("groupbyseler")]
        public async Task<IActionResult> GetSalesGroupBySeller([FromQuery] SaleParams param)
        {
            return HandlePagedResult(await Mediator.Send(new SaleListGroupBySeller.Query { Params = param }));
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateSale(Sale sale)
        {
            return HandleResult(await Mediator.Send(new CreateSale.Command { Sale = sale }));
        }

        [HttpGet("grouped-by-project")]
        public async Task<IActionResult> GetSalesGroupedByProject([FromQuery] SaleParams param)
        {
            return HandlePagedResult(await Mediator.Send(new SaleListGroupByProject.Query { Params = param }));
        }
    }
}