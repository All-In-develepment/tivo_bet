using Application.Core;
using Application.Products;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] PagingParams param)
        {
            return HandlePagedResult(await Mediator.Send(new ListProduct.Query { Params = param }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            return HandleResult(await Mediator.Send(new CreateProduct.Command { Product = product }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditProduct(Guid id, Product product)
        {
            product.ProductId = id;
            return HandleResult(await Mediator.Send(new EditProduct.Command { Product = product }));
        }
    }
}