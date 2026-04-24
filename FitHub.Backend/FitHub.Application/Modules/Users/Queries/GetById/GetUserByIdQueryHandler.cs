using FitHub.Application.Modules.Catalog.ProductCategories.Queries.GetById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Users.Queries.GetById
{
    public class GetUserByIdQueryHandler(IAppDbContext context) : IRequestHandler<GetUserByIdQuery,GetUserByIdQueryDto>
    {
        public async Task<GetUserByIdQueryDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await context.Users
                .Where(c => c.UserID == request.Id)
                .Select(x => new GetUserByIdQueryDto
                {
                    Id = x.UserID,
                    Name = x.Name,
                    Surname=x.Surname,
                    Email=x.Email,
                    PhoneNumber=x.PhoneNumber,
                    Gender=x.Gender?"Male":"Female",
                    Status=x.Status,
                    Address=x.Address,
                    RoleID=x.RoleID,
                    CenterID=x.CenterID
                    
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (user == null)
            {
                throw new MarketNotFoundException($"User with Id {request.Id} not found.");
            }

            return user;
        }
    }
}
