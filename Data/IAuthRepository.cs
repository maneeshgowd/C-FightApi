using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net_7.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Regiser(User user, string password);
        Task<ServiceResponse<string>> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}