using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Orders.Queries.GetById
{
    public class GetOrderByIdQueryDto
    {
        public int OrderID { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }

        public int CenterID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }

        public List<GetOrderItemDto> Items { get; set; } = new();
    }

    public class GetOrderItemDto
    {
        public int OrderItemID { get; set; }
        public int FitnessPlanID { get; set; }
        public string PlanTitle { get; set; } 
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; } 
        public decimal TotalLineAmount { get; set; } 
    }
}
