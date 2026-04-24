using FitHub.Application.Modules.Catalog.ProductCategories.Queries.List;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Users.Queries.List
{
    public class ListUsersQueryHandler(IAppDbContext ctx)
        : IRequestHandler<ListUsersQuery, PageResult<ListUsersQueryDto>>
    {
        public async Task<PageResult<ListUsersQueryDto>> Handle(
       ListUsersQuery request, CancellationToken ct)
        {
            var q = ctx.Users.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                q = q.Where(x => x.Name.Contains(request.Search));
            }


            var projectedQuery = q.OrderBy(x => x.Name)
                .Select(x => new ListUsersQueryDto
                {
                    Id = x.UserID,
                    Name = x.Name,
                    Surname = x.Surname,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    Gender = x.Gender ? "Male" : "Female",
                    Status = x.Status,
                    Address = x.Address,
                    RoleID = x.RoleID,
                    CenterID = x.CenterID
                });

            return await PageResult<ListUsersQueryDto>.FromQueryableAsync(projectedQuery, request.Paging, ct);
        }
    }
}
