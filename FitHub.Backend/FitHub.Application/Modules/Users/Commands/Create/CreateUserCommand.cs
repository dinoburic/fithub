using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Users.Commands.Create
{
    public class CreateUserCommand : IRequest<int>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public int CenterID { get; set; }
        [Required]
        public bool Gender { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Status { get; set; }
        public string? Address { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public int RoleID { get; set; }
    }
}
