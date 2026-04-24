using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Auth.Commands.Registration
{
    public class RegistrationCommandValidator : AbstractValidator<RegistrationCommandDto>
    {
        public RegistrationCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ime je obavezno.")
                .MaximumLength(100).WithMessage("Ime ne smije biti duže od 100 karaktera.");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Prezime je obavezno.")
                .MaximumLength(100).WithMessage("Prezime ne smije biti duže od 100 karaktera.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email je obavezan.")
                .EmailAddress().WithMessage("Email nije validan.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Lozinka je obavezna.")
                .MinimumLength(8).WithMessage("Lozinka mora imati najmanje 8 karaktera.")
                .Matches(@"[A-Z]").WithMessage("Lozinka mora sadržavati barem jedno veliko slovo.")
                .Matches(@"[a-z]").WithMessage("Lozinka mora sadržavati barem jedno malo slovo.")
                .Matches(@"\d").WithMessage("Lozinka mora sadržavati barem jednu cifru.")
                .Matches(@"[\W_]").WithMessage("Lozinka mora sadržavati barem jedan specijalni karakter.");

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^\+?\d{9,15}$").WithMessage("Broj telefona nije validan.");

            RuleFor(x => x.CenterID)
                .GreaterThan(0).WithMessage("Fitness centar mora biti odabran.");

            RuleFor(x => x.CaptchaToken)
                .NotEmpty().WithMessage("CAPTCHA verifikacije je obavezna.");
        }
    }
}
