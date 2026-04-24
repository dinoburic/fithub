using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Orders.Queries.List
{
    public class ListOrdersQuery : BasePagedQuery<ListOrdersQueryDto>
    {
        public int? CenterID { get; set; }
        public string? Status { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
