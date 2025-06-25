using domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace application.DTO
{
    public class RegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        [Required]
        [EnumDataType(typeof(UserRole), ErrorMessage = "Role must be either 'Customer' or 'Broker'.")]
        public string Role { get; set; } // "Customer" or "Broker"  hardcoded
    }

    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
