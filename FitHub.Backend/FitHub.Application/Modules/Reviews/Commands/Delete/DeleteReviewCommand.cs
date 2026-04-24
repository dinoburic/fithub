using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Reviews.Commands.Delete
{
    public class DeleteReviewCommand : IRequest
    {
        public int ReviewID { get; set; }
    }
}
