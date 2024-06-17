using Cooking.Api.Controllers.Request;
using Cooking.Application.Categories.Create;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Cooking.Web.Controllers;

[ApiController]
[Route("Categories")]
[EnableCors("AllowLocalhost")]
public class CategoryController : ControllerBase
{
    private readonly ISender _sender;

    public CategoryController(ISender sender)
    {
        _sender = sender;
    }

    [Route("")]
    [HttpPost]
    public async Task<IActionResult> CreateCategory(
            [FromBody] CreateCategoryRequest request,
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
}


