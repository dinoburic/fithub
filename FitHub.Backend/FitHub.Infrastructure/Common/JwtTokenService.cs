using FitHub.Application.Abstractions;
using FitHub.Domain.Entities.Identity; // Pretpostavljam da ti treba za Users entitet
using FitHub.Shared.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FitHub.Infrastructure.Common;

public sealed class JwtTokenService : IJwtTokenService
{
    private readonly JwtOptions _jwt;
    private readonly TimeProvider _time;

    public JwtTokenService(IOptions<JwtOptions> options, TimeProvider time)
    {
        _jwt = options?.Value ?? throw new ArgumentNullException(nameof(options));
        _time = time ?? throw new ArgumentNullException(nameof(time));
    }

    public JwtTokenPair IssueTokens(Users user)
    {
        var nowInstant = _time.GetUtcNow();
        var nowUtc = nowInstant.UtcDateTime;
        var accessExpires = nowInstant.AddMinutes(_jwt.AccessTokenMinutes).UtcDateTime;
        var refreshExpires = nowInstant.AddDays(_jwt.RefreshTokenDays).UtcDateTime;

        // 1. MAPIRANJE ULOGA (RoleID 3 = Admin, 2 = Manager, 1 = Employee)
        bool isAdmin = user.RoleID == 3; 
        bool isManager = user.RoleID == 2;
        bool isEmployee = user.RoleID == 1;

        // --- Claims ---
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.UserID.ToString()),
            new(ClaimTypes.NameIdentifier, user.UserID.ToString()), 
            new(ClaimTypes.Name, user.Name?.ToString() ?? ""), 
            new(ClaimTypes.Email, user.Email ?? ""),
            new("CenterId", user.CenterID.ToString() ?? ""),
            new("RoleId", user.RoleID.ToString()),
            
            // 2. CUSTOM CLAIM-OVI ZA FRONTEND (Angular facade)
            new("is_admin",    isAdmin.ToString().ToLowerInvariant()),
            new("is_manager",  isManager.ToString().ToLowerInvariant()),
            new("is_employee", isEmployee.ToString().ToLowerInvariant()),
            
            new(JwtRegisteredClaimNames.Iat, ToUnixTimeSeconds(nowInstant).ToString(), ClaimValueTypes.Integer64),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
            new(JwtRegisteredClaimNames.Aud, _jwt.Audience)
        };

        // 3. STANDARDNI CLAIM-OVI ZA BACKEND ([Authorize(Roles = "Admin")])
        if (isAdmin) claims.Add(new Claim(ClaimTypes.Role, "Admin"));
        if (isManager) claims.Add(new Claim(ClaimTypes.Role, "Manager"));
        if (isEmployee) claims.Add(new Claim(ClaimTypes.Role, "Employee"));

        // --- Signature ---
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
        var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        // --- access token (JWT) ---
        var jwt = new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            notBefore: nowUtc,
            expires: accessExpires,
            signingCredentials: creds
        );

        var accessToken = new JwtSecurityTokenHandler().WriteToken(jwt);

        // --- refresh token (raw + hash) ---
        var refreshRaw = GenerateRefreshTokenRaw(64); // base64url
        var refreshHash = HashRefreshToken(refreshRaw); // base64url hash

        return new JwtTokenPair
        {
            AccessToken = accessToken,
            AccessTokenExpiresAtUtc = accessExpires,
            RefreshTokenRaw = refreshRaw,
            RefreshTokenHash = refreshHash,
            RefreshTokenExpiresAtUtc = refreshExpires
        };
    }

    public string HashRefreshToken(string rawToken)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(rawToken));
        return Base64UrlEncoder.Encode(bytes);
    }

    private static string GenerateRefreshTokenRaw(int numBytes)
    {
        var bytes = RandomNumberGenerator.GetBytes(numBytes);
        return Base64UrlEncoder.Encode(bytes);
    }

    private static long ToUnixTimeSeconds(DateTimeOffset dto) => dto.ToUnixTimeSeconds();
}