using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Cart.Commands.DeleteFromCart
{
    public class DeleteFromCartCommandValidator : AbstractValidator<DeleteFromCartCommand>
    {
        public DeleteFromCartCommandValidator()
        {
            RuleFor(x => x.CartItemID)
                .GreaterThan(0).WithMessage("CartItemID must be greater than 0.");
        }
    }
}
