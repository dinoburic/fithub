using FitHub.Application.Modules.Catalog.ProductCategories.Commands.Delete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FitHub.Application.Modules.Users.Commands.Delete
{
    public class DeleteUserCommandHandler(IAppDbContext context, IAppCurrentUser appCurrentUser)
      : IRequestHandler<DeleteUserCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            if (appCurrentUser.UserId is null)
                throw new MarketBusinessRuleException("123", "Not authenticated.");

            bool isSelfDelete = appCurrentUser.UserId == request.Id;
            
            bool isAdmin = appCurrentUser.RoleId == 3; if (!isSelfDelete && !isAdmin)
            {
                
                throw new UnauthorizedAccessException("Nemate dozvolu za brisanje ovog naloga.");
            }

            var user = await context.Users
                .FirstOrDefaultAsync(x => x.UserID == request.Id, cancellationToken);

            if (user is null)
                throw new MarketNotFoundException("User not found.");

            user.IsDeleted = true;
            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}