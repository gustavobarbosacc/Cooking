using Cooking.Api.Controllers.Request;
using Cooking.Application.Categories.Create;
using Cooking.Application.Categories.Delete;
using Cooking.Application.Categories.Find;
using Cooking.Application.Categories.FindAll;
using Cooking.Application.Users.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cooking.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost]
    public async Task<IActionResult> CreateOrder(
        CreateCategoryRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateCategoryCommand(
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
        var query = new FindCategoryQuery(id);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> FindAllCategory([FromQuery] int? page, [FromQuery] int? size, CancellationToken cancellationToken)
    {
        var query = new FindAllCategoryQuery(page ?? 1, size ?? 10);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteCategoryCommand(id);

        var result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok() : NotFound();
    }
}
