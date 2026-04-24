using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Orders.Queries.GetById
{
    public sealed class GetOrderByIdQueryValidator : AbstractValidator<GetOrderByIdQuery>
    {
        public GetOrderByIdQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Order ID must be greater than zero.");
        }
    }
}
