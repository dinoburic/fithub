using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Orders.Commands.Delete
{
    public class DeleteOrderCommand : IRequest
    {
        public int OrderID { get; set; }
    }
}
