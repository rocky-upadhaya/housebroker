using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace domain.Entities.Auth
{
    public class ApplicationUser : IdentityUser
    {
        public string Role { get; set; } // Either "Customer" or "Broker" static for now
    }
}
