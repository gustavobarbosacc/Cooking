using Cooking.Api.Controllers.Request;
using Cooking.Application.Users.Create;
using Cooking.Application.Users.Delete;
using Cooking.Application.Users.Find;
using Cooking.Application.Users.FindAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cooking.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost]
    public async Task<IActionResult> CreateOrder(
        CreateUserRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateUserCommand(
            request.Name,
            request.Email,
            request.Password,
            request.Role);

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
        var query = new FindUserQuery(id);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> FindAllUser([FromQuery] int? page, [FromQuery] int? size, CancellationToken cancellationToken)
    {
        var query = new FindAllUserQuery(page ?? 1, size ?? 10);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteUserCommand(id);

        var result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok() : NotFound();
    }
}
