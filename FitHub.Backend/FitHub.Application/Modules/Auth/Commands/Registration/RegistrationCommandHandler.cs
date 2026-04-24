using FitHub.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Auth.Commands.Registration
{
    public sealed class RegistrationCommandHandler(
    IAppDbContext ctx,
    IJwtTokenService jwt,
    IPasswordHasher<FitHub.Domain.Entities.Identity.Users> hasher,
    IValidator<RegistrationCommandDto> validator,
    ICaptchaService captchaService)
    : IRequestHandler<RegistrationCommand, int> {

        public async Task<int> Handle(RegistrationCommand request, CancellationToken ct)
        {
            var validationResult = await validator.ValidateAsync(request.Dto, ct);
            if(!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var isCaptchaValid = await captchaService.VerifyAsync(request.Dto.CaptchaToken);
            if (!isCaptchaValid)
            {
                throw new InvalidOperationException("CAPTCHA Verifikacija nije uspjela.");
            }

            var email = request.Dto.Email.Trim().ToLowerInvariant();

            var existingUser = await ctx.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email, ct);
            if (existingUser != null)
                throw new InvalidOperationException("Email je već u upotrebi.");

            // 3. Hashiranje lozinke
            var passwordHash = hasher.HashPassword(null, request.Dto.Password);

            var user = new Domain.Entities.Identity.Users
            {
                Name = request.Dto.Name,
                Surname = request.Dto.Surname,
                Email = email,
                PasswordHash = passwordHash,
                Gender = request.Dto.Gender,
                CenterID = request.Dto.CenterID,
                PhoneNumber = request.Dto.PhoneNumber,
                Address = request.Dto.Address,
                Status = "Active",
                RoleID=1,
                IsDeleted = false
            };

            ctx.Users.Add(user);
            await ctx.SaveChangesAsync(ct);
            return user.UserID;
        }
    }
}
