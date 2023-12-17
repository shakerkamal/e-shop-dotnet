using EShop.Application.Commands.CreateProduct;
using EShop.Application.Queries.GetProducts;
using EShop.Shared.DataTransferObjects.ProductDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ISender _sender;

        public ProductsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet(Name ="GetProducts")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _sender.Send(new GetProductsQuery());
            return Ok(products);
        }

        [HttpPost(Name = "CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody]CreateProductDto productDto)
        {
            var product = await _sender.Send(new CreateProductCommand(productDto));
            return Ok(product);
        }
    }
}
