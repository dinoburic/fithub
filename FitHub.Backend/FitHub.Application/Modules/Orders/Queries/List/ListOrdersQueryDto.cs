using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Orders.Queries.List
{
    public class ListOrdersQueryDto 
    {
        public int OrderID { get; set; }
        public DateTime CreatedAt { get; set; } 
        public int CenterID { get; set; }

        public decimal TotalAmount { get; set; } 
        public string Status { get; set; }

        public List<ListOrderItemDto> OrderItems { get; set; } = new();
    }

    public class ListOrderItemDto
    {
        public int OrderItemID { get; set; } 
        public int FitnessPlanID { get; set; }
        public string PlanTitle { get; set; } 

        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalLineAmount { get; set; }
    }
}
