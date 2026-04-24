using FitHub.Application.Modules.Auth.Commands.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Auth.Commands.ExternalLogin
{
    public class ExternalLoginCommand : IRequest<LoginCommandDto>
    {
        public required string Provider { get; set; } 
        public required string IdToken { get; set; }  
    }
}
