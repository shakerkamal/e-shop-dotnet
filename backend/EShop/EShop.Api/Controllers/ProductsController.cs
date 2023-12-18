using EShop.Application.Commands.CreateProduct;
using EShop.Application.Commands.DeleteProduct;
using EShop.Application.Commands.UpdateProduct;
using EShop.Application.Queries.GetProduct;
using EShop.Application.Queries.GetProducts;
using EShop.Shared.DataTransferObjects.ProductDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Api.Controllers;

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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(string id)
    {
        var product = await _sender.Send(new GetProductDetailsQuery(id));
        return Ok(product);
    }

    [HttpPost(Name = "CreateProduct")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto productDto)
    {
        var product = await _sender.Send(new CreateProductCommand(productDto));
        return Ok(product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(string id, [FromBody] UpdateProductDto updateProduct)
    {
        await _sender.Send(new UpdateProductCommand(id, updateProduct));
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        await _sender.Send(new DeleteProductCommand(id));
        return NoContent();
    }
}
