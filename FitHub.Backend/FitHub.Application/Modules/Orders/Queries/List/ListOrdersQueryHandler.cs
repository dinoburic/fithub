using FitHub.Application.Modules.Users.Queries.List;
using FitHub.Application.Modules.Workout.FitnessPlans.Queries.List;
using FitHub.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Orders.Queries.List
{
    public sealed class ListOrdersQueryHandler(IAppDbContext ctx, IAppCurrentUser user) : IRequestHandler<ListOrdersQuery, PageResult<ListOrdersQueryDto>>
    {
        public async Task<PageResult<ListOrdersQueryDto>> Handle(ListOrdersQuery request, CancellationToken ct)
        {
            var query = ctx.Orders.AsQueryable();

            query = query.Where(x => !x.IsDeleted);

            if (user.UserId != null) 
            {
                query = query.Where(x => x.UserID == user.UserId);
            }

            if (request.CenterID.HasValue)
            {
                query = query.Where(x => x.CenterID == request.CenterID);
            }

            if (!string.IsNullOrEmpty(request.Status))
            {
                query = query.Where(x => x.Status == request.Status);
            }

            if (request.FromDate.HasValue)
            {
                query = query.Where(x => x.CreatedAt >= request.FromDate.Value);
            }

            var projectedQuery = query
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new ListOrdersQueryDto
                {
                    OrderID = x.OrderID,
                    CreatedAt = x.CreatedAt,
                    CenterID = x.CenterID,
                    TotalAmount = x.TotalAmount,
                    Status = x.Status,
                    OrderItems = x.Items.Select(i => new ListOrderItemDto
                    {
                        OrderItemID = i.OrderItemID,
                        FitnessPlanID = i.FitnessPlanID,
                        PlanTitle = i.FitnessPlan.Title,
                        UnitPrice = i.Price,
                        Quantity = i.Quantity,
                        TotalLineAmount = i.Price * i.Quantity
                    }).ToList()
                });

            return await PageResult<ListOrdersQueryDto>.FromQueryableAsync(projectedQuery, request.Paging, ct);
        }
    }
}