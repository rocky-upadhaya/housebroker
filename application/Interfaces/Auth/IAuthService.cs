using application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace application.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<(bool Success, IEnumerable<string> Errors)> RegisterAsync(RegisterDto model);
        Task<string?> LoginAsync(LoginDto model);
    }
}
