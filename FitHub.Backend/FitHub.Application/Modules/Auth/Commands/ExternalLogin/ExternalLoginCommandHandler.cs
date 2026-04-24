using FitHub.Application.Modules.Auth.Commands.Login;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity; 
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Auth.Commands.ExternalLogin
{
    public sealed class ExternalLoginCommandHandler(
     IAppDbContext ctx,
     IJwtTokenService jwtService,
     IConfiguration config,
     HttpClient httpClient)
     : IRequestHandler<ExternalLoginCommand, LoginCommandDto>
    {
        public async Task<LoginCommandDto> Handle(ExternalLoginCommand request, CancellationToken ct)
        {
            string email = string.Empty;
            string firstName = string.Empty;
            string lastName = string.Empty;
            var googleClientId = config["Authentication:GoogleClientId"];

            if (request.Provider.Equals("Google", StringComparison.OrdinalIgnoreCase))
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { googleClientId }
                };

                var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, settings);

                email = payload.Email;
                firstName = payload.GivenName;
                lastName = payload.FamilyName;
            }
            else if (request.Provider.Equals("Facebook", StringComparison.OrdinalIgnoreCase))
            {
                var fbResponse = await httpClient.GetAsync($"https://graph.facebook.com/me?fields=email,first_name,last_name&access_token={request.IdToken}", ct);
                if (!fbResponse.IsSuccessStatusCode) throw new UnauthorizedAccessException("Nevalidan Facebook token.");

                var fbData = await fbResponse.Content.ReadAsStringAsync(ct);
                using var doc = JsonDocument.Parse(fbData);

                email = doc.RootElement.GetProperty("email").GetString()!;
                firstName = doc.RootElement.GetProperty("first_name").GetString()!;
                lastName = doc.RootElement.GetProperty("last_name").GetString()!;
            }
            else
            {
                throw new ArgumentException("Nepoznat provider.");
            }

            var user = await ctx.Users.FirstOrDefaultAsync(u => u.Email == email, ct);

            if (user == null)
            {
                var hasher = new PasswordHasher<Domain.Entities.Identity.Users>();
                var randomPassword = Guid.NewGuid().ToString() + "A1!";

                user = new Domain.Entities.Identity.Users
                {
                    Email = email,
                    Name = firstName,
                    Surname = lastName ?? "",
                    PasswordHash = hasher.HashPassword(null!, randomPassword),
                    RoleID = 1,
                    CenterID = 1,
                    Gender = true, 
                    Status = "Active",
                    IsDeleted = false
                };

                ctx.Users.Add(user);
                
                // Save user immediately so database assigns UserID
                // koji nam je potreban ispod za RefreshTokenEntity
                await ctx.SaveChangesAsync(ct); 
            }

            var tokens = jwtService.IssueTokens(user);

            // 1. FIXED: Identical logic as in LoginCommandHandler
            ctx.RefreshTokens.Add(new RefreshTokenEntity
            {
                TokenHash = tokens.RefreshTokenHash,
                ExpiresAtUtc = tokens.RefreshTokenExpiresAtUtc,
                UserId = user.UserID
            });

            await ctx.SaveChangesAsync(ct);

            return new LoginCommandDto
            {
                AccessToken = tokens.AccessToken,
                RefreshToken = tokens.RefreshTokenRaw, 
                ExpiresAtUtc = tokens.AccessTokenExpiresAtUtc
            };
        }
    }
}
