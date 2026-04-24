using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Users.Queries.List
{
    public class ListUsersQuery : BasePagedQuery<ListUsersQueryDto>
    {
        public string? Search { get; init; }
    }
}
