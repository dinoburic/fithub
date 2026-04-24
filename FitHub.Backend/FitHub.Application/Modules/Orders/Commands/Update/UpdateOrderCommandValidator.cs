using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Orders.Commands.Update
{
    public sealed class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator() {
            RuleFor(x => x.OrderID)
                 .GreaterThan(0).WithMessage("OrderID is required.");

            var allowedStatuses = new[] { "Pending", "Completed", "Cancelled", "Shipped" };

            RuleFor(x => x.Status)
                .Must(status => string.IsNullOrEmpty(status) || allowedStatuses.Contains(status))
                .WithMessage($"Status must be one of: {string.Join(", ", allowedStatuses)}");

            RuleFor(x => x.Address)
                .MaximumLength(100).When(x => !string.IsNullOrEmpty(x.Address));

        }
    }
}
