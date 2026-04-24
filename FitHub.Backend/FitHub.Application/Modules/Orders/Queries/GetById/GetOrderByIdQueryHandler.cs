using FitHub.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore; // Obavezno za Include i ThenInclude
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Orders.Queries.GetById
{
    public class GetOrderByIdQueryHandler(IAppDbContext ctx, IAppCurrentUser user) : IRequestHandler<GetOrderByIdQuery, GetOrderByIdQueryDto>
    {
        public async Task<GetOrderByIdQueryDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var query = ctx.Orders
                .AsNoTracking() 
                .Include(x => x.Center) 
                .Include(x => x.Items) 
                    // FIXED: Fetch related fitness plans in the SAME SQL query
                    .ThenInclude(i => i.FitnessPlan) 
                .Where(x => x.OrderID == request.Id)
                .Where(x => !x.IsDeleted);

            var orderEntity = await query.FirstOrDefaultAsync(cancellationToken);

            if (orderEntity == null)
            {
                throw new KeyNotFoundException($"Order not found.");
            }

            if (orderEntity.UserID != user.UserId)
            {
                throw new UnauthorizedAccessException("Nemate pristup ovoj narudžbi.");
            }

            var dto = new GetOrderByIdQueryDto
            {
                OrderID = orderEntity.OrderID,
                CreatedAt = orderEntity.CreatedAt,
                Status = orderEntity.Status,
                TotalAmount = orderEntity.TotalAmount,

                CenterID = orderEntity.CenterID,

                FirstName = orderEntity.FirstName,
                LastName = orderEntity.LastName,
                Email = orderEntity.Email,
                Address = orderEntity.Address,
                City = orderEntity.City,
                ZipCode = orderEntity.ZipCode,
                Country = orderEntity.Country,

                Items = orderEntity.Items.Select(i => new GetOrderItemDto
                {
                    OrderItemID = i.OrderItemID,
                    FitnessPlanID = i.FitnessPlanID,
                    
                    // FIXED: Read title directly from memory (without new database query)
                    PlanTitle = i.FitnessPlan?.Title ?? "Nepoznat/Obrisan Plan", 
                    
                    Quantity = i.Quantity,
                    UnitPrice = i.Price,
                    TotalLineAmount = i.Quantity * i.Price
                }).ToList()
            };

            return dto;
        }
    }
}
