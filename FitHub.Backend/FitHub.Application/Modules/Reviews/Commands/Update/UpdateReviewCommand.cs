using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Reviews.Commands.Update
{
    public class UpdateReviewCommand : IRequest
    {
        public int ReviewID { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}
