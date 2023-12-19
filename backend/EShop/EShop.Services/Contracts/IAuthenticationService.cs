using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Services.Contracts
{
    public interface IAuthenticationService
    {
        Task Authenticate(string username, string password);
        Task RefreshToken(string token);
    }
}
