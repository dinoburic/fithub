using FitHub.Application.Modules.Catalog.ProductCategories.Commands.Update;
using FitHub.Application.Common.Interfaces; // Dodano za IAppCurrentUser
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FitHub.Application.Modules.Users.Commands.Update
{
    public sealed class UpdateUserCommandHandler(
        IAppDbContext ctx,
        IPasswordHasher<FitHub.Domain.Entities.Identity.Users> hasher,
        IAppCurrentUser currentUser) 
            : IRequestHandler<UpdateUserCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken ct)
        {
            bool isSelfUpdate = currentUser.UserId == request.Id;
            
            // Adjust role check to your implementation (e.g., currentUser.IsAdmin)
            bool isAdmin = currentUser.RoleId != null && currentUser.RoleId==3;

            // If user edits someone else's profile and is not admin, reject request
            if (!isSelfUpdate && !isAdmin)
            {
                throw new UnauthorizedAccessException("Nemate pravo mijenjati tuđi profil.");
            }

            var entity = await ctx.Users
                .Where(x => x.UserID == request.Id)
                .FirstOrDefaultAsync(ct);

            if (entity is null)
                throw new MarketNotFoundException($"User (ID={request.Id}) not found.");

            if (request.RoleID.HasValue || !string.IsNullOrWhiteSpace(request.Status) || request.CenterID.HasValue)
            {
                if (!isAdmin)
                {
                    throw new UnauthorizedAccessException("Samo administratori mogu mijenjati uloge, statuse i centre.");
                }
                else
                {
                    // Primjenjujemo promjene samo ako je pozivalac zaista admin
                    entity.RoleID = request.RoleID ?? entity.RoleID;
                    entity.Status = request.Status ?? entity.Status;
                    entity.CenterID = request.CenterID ?? entity.CenterID;
                }
            }

            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                var exists = await ctx.Users
                    .AnyAsync(x => x.UserID != request.Id && x.Email.ToLower() == request.Email.ToLower(), ct);

                if (exists)
                    throw new MarketConflictException("Email already exists.");

                entity.Email = request.Email.Trim();
            }

            entity.Name = request.Name ?? entity.Name;
            entity.Surname = request.Surname ?? entity.Surname;
            entity.Gender = request.Gender ?? entity.Gender;
            entity.Address = request.Address ?? entity.Address;
            entity.PhoneNumber = request.PhoneNumber ?? entity.PhoneNumber;
            entity.BadgeID = request.BadgeID ?? entity.BadgeID;
            entity.Weight = request.Weight ?? entity.Weight;
            entity.Height = request.Height ?? entity.Height;
            entity.FitnessPlanTypeID = request.FitnessPlanTypeID ?? entity.FitnessPlanTypeID;

            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                entity.PasswordHash = hasher.HashPassword(null, request.Password);
            }

            await ctx.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
