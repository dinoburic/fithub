using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Reviews.Commands.Create
{
    public class CreateReviewCommand : IRequest<int>
    {
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public int FitnessPlanID { get; set; }
    }
}
