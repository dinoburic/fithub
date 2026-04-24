using FitHub.Application.Common.Interfaces; // Adjust namespace
using FitHub.Domain.Entities.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FitHub.Application.Modules.Orders.Commands.Update
{
    public class UpdateOrderCommandHandler(IAppDbContext ctx, IAppCurrentUser appCurrentUser) : IRequestHandler<UpdateOrderCommand>
    {
        public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            if (appCurrentUser.UserId is null)
                throw new UnauthorizedAccessException("Niste prijavljeni.");

            var order = await ctx.Orders
                 .FirstOrDefaultAsync(x => x.OrderID == request.OrderID && !x.IsDeleted, cancellationToken);

            bool isAdmin = appCurrentUser.RoleId == 3; 

           if (order == null || (order.UserID != appCurrentUser.UserId && !isAdmin))
            {
                throw new KeyNotFoundException($"Order with ID {request.OrderID} not found.");
            }

            // STATUS PROTECTION: Only admin can change order status
            if (!string.IsNullOrEmpty(request.Status))
            {
                if (!isAdmin) 
                {
                    throw new UnauthorizedAccessException("Samo administrator može mijenjati status narudžbe.");
                }
                order.Status = request.Status;
            }

            if (!string.IsNullOrEmpty(request.FirstName)) order.FirstName = request.FirstName;
            if (!string.IsNullOrEmpty(request.LastName)) order.LastName = request.LastName;
            if (!string.IsNullOrEmpty(request.Address)) order.Address = request.Address;
            if (!string.IsNullOrEmpty(request.City)) order.City = request.City;
            if (!string.IsNullOrEmpty(request.ZipCode)) order.ZipCode = request.ZipCode;
            if (!string.IsNullOrEmpty(request.Country)) order.Country = request.Country;

            await ctx.SaveChangesAsync(cancellationToken);
        }
    }
}
