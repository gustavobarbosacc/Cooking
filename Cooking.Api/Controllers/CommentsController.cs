using Cooking.Api.Controllers.Request;
using Cooking.Application.Comments.Create;
using Cooking.Application.Comments.Delete;
using Cooking.Application.Comments.Find;
using Cooking.Application.Comments.FindAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cooking.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentsController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost]
    public async Task<IActionResult> CreateOrder(
        CreateCommentRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateCommentCommand(
            Guid.Parse("b67a9122-0ddc-4e48-a35e-747f563a6f8f"),
            request.RecipeId,
            request.Remark);

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
        var query = new FindCommentQuery(id);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> FindAllComment([FromQuery] int? page, [FromQuery] int? size, CancellationToken cancellationToken)
    {
        var query = new FindAllCommentQuery(page ?? 1, size ?? 10);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteCommentCommand(id);

        var result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok() : NotFound();
    }
}
