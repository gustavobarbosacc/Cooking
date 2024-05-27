using Cooking.Api.Controllers.Request;
using Cooking.Application.Products.Create;
using Cooking.Application.Products.Delete;
using Cooking.Application.Products.Find;
using Cooking.Application.Products.FindAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cooking.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost]
    public async Task<IActionResult> CreateOrder(
        CreateProductRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateProductCommand(
            Guid.Parse("b67a9122-0ddc-4e48-a35e-747f563a6f8f"),
            request.Name);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> FindOrder(Guid id, CancellationToken cancellationToken)
    {
        var query = new FindProductQuery(id);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> FindAllProduct([FromQuery] int? page, [FromQuery] int? size, CancellationToken cancellationToken)
    {
        var query = new FindAllProductQuery(page ?? 1, size ?? 10);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteProductCommand(id);

        var result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok() : NotFound();
    }
}
