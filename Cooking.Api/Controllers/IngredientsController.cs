using Cooking.Api.Controllers.Request;
using Cooking.Application.Ingredients.Create;
using Cooking.Application.Ingredients.Delete;
using Cooking.Application.Ingredients.Find;
using Cooking.Application.Ingredients.FindAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cooking.Api.Controllers;



[Route("api/[controller]")]
[ApiController]
public class IngredientsController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost]
    public async Task<IActionResult> CreateOrder(
        CreateIngredientRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateIngredientCommand(
            Guid.Parse("b67a9122-0ddc-4e48-a35e-747f563a6f8f"),
            request.ProductId,
            request.measure,
            request.Name,
            request.Quantity);

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
        var query = new FindIngredientQuery(id);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> FindAllIngredient([FromQuery] int? page, [FromQuery] int? size, CancellationToken cancellationToken)
    {
        var query = new FindAllIngredientQuery(page ?? 1, size ?? 10);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteIngredient(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteIngredientCommand(id);

        var result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok() : NotFound();
    }
}