using Identity.DataTransferObjects;
using System.Threading.Tasks;

namespace Identity.Services
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
        Task<string> CreateToken();
    }
}
