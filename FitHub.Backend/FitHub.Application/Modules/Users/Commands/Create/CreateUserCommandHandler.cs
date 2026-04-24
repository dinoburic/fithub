using FitHub.Application.Common.Interfaces; // Adjust namespace if needed
using Microsoft.AspNetCore.Identity; // Often needed for IPasswordHasher
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Users.Commands.Create
{
    
    public class CreateUserCommandHandler(
        IAppDbContext context, 
        IPasswordHasher<FitHub.Domain.Entities.Identity.Users> hasher) : IRequestHandler<CreateUserCommand, int>
    {
        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var normalizedName = request.Name?.Trim();
            var normalizedSurname = request.Surname?.Trim();
            var normalizedEmail = request.Email?.Trim();

         
            var passwordHash = hasher.HashPassword(null, request.Password);

            var user = new FitHub.Domain.Entities.Identity.Users
            {
                Name = normalizedName,
                Surname = normalizedSurname,
                Email = normalizedEmail,
                
                // 3. SAVE HASHED VALUE
                PasswordHash = passwordHash,
                
                Gender = request.Gender,
                CenterID = request.CenterID,
                RoleID = request.RoleID,
                PhoneNumber = request.PhoneNumber,
                Status = request.Status,
                Address = request.Address,
                IsDeleted = false
            };

            context.Users.Add(user);
            await context.SaveChangesAsync(cancellationToken);

            return user.UserID;
        }
    }
}
