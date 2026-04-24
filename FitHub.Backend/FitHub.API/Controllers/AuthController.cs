using FitHub.Application.Modules.Auth.Commands.ExternalLogin;
using FitHub.Application.Modules.Auth.Commands.Login;
using FitHub.Application.Modules.Auth.Commands.Logout;
using FitHub.Application.Modules.Auth.Commands.Refresh;
using FitHub.Application.Modules.Auth.Commands.Registration;

[ApiController]
[Route("api/auth")]
public sealed class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("registration")]
    [AllowAnonymous]
    public async Task<ActionResult<RegistrationCommandDto>> Register([FromBody] RegistrationCommand command, CancellationToken ct )
    {
        return Ok(await mediator.Send(command, ct));
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<LoginCommandDto>> Login([FromBody] LoginCommand command, CancellationToken ct)
    {
        return Ok(await mediator.Send(command, ct));
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    public async Task<ActionResult<LoginCommandDto>> Refresh([FromBody] RefreshTokenCommand command, CancellationToken ct)
    {
        return Ok(await mediator.Send(command, ct));
    }

    
    [HttpPost("logout")]
    [AllowAnonymous]
    public async Task Logout([FromBody] LogoutCommand command, CancellationToken ct)
    {
        await mediator.Send(command, ct);
    }

    [HttpPost("external-login")]
    [AllowAnonymous]
    public async Task<ActionResult<LoginCommandDto>> ExternalLogin([FromBody] ExternalLoginCommand command, CancellationToken ct)
    {
        try
        {
            var result = await mediator.Send(command, ct);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return Unauthorized(new { message = "Auth doesnt work", error = ex.Message });
        }
    }
}