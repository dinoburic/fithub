using FitHub.Application.Common.Interfaces; // Adjust namespace for IAppCurrentUser
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FitHub.Application.Modules.Orders.Commands.Delete
{
    public class DeleteOrderCommandHandler(IAppDbContext ctx, IAppCurrentUser appCurrentUser) : IRequestHandler<DeleteOrderCommand>
    {
        public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            if (appCurrentUser.UserId is null)
                throw new UnauthorizedAccessException("Niste prijavljeni.");

            var order = await ctx.Orders
                .FirstOrDefaultAsync(x => x.OrderID == request.OrderID, cancellationToken);

            bool isAdmin = appCurrentUser.RoleId == 3; 

            if (order == null || (order.UserID != appCurrentUser.UserId && !isAdmin))
            {
                throw new KeyNotFoundException($"Order with ID {request.OrderID} does not exist.");
            }

            if (order.IsDeleted)
            {
                 throw new InvalidOperationException("Order already deleted.");
            }

            order.IsDeleted = true;

            await ctx.SaveChangesAsync(cancellationToken);
        }
    }
}
