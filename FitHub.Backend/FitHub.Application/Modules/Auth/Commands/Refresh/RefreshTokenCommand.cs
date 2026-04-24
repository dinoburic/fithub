namespace FitHub.Application.Modules.Auth.Commands.Refresh;

/// <summary>
/// Request to rotate the refresh token and issue a new token pair.
/// </summary>
public sealed class RefreshTokenCommand : IRequest<RefreshTokenCommandDto>
{
    /// <summary>
    /// Refresh token that the client sends for rotation.
    /// </summary>
    public string RefreshToken { get; init; }

}