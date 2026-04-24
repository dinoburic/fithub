using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Auth.Commands.Registration
{
    public sealed class RegistrationCommandDto
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public bool Gender { get; set; } = true;             
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int CenterID { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public int RoleID { get; set; }

        public string CaptchaToken { get; set; } 
    }
}
