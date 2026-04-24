using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Reviews.Queries.GetById
{
    public class GetReviewByIdQuery : IRequest<GetReviewByIdQueryDto>
    {
        public int Id { get; set; }
    }
}
