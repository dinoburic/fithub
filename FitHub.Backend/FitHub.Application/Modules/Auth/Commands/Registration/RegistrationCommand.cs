using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Auth.Commands.Registration
{
    public sealed class RegistrationCommand : IRequest<int>
    {
        public RegistrationCommandDto Dto { get; init; }

        public RegistrationCommand(RegistrationCommandDto dto)
        {
            Dto = dto;
        }
    }
}
