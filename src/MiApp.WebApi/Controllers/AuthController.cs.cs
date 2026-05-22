using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MiApp.Application.Auth.Commands.Login;
using MiApp.Application.Common;
using MiApp.WebApi.Contracts.Auth;

namespace MiApp.WebApi.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "Auth")]
[Route("api/auth")]
public sealed class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AuthResponse>> Login(LoginRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await mediator.Send(new LoginCommand(request.Email, request.Password), cancellationToken);
            return Ok(response);
        }
        catch (ValidationException exception)
        {
            return BadRequest(exception.Errors.Select(error => new { error.PropertyName, error.ErrorMessage }));
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
    }
}
