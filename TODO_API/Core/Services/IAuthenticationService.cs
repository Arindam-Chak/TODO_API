using System.Threading.Tasks;
using ToDoAPI.Core.Services.Communication;

namespace ToDoAPI.Core.Services
{
    public interface IAuthenticationService
    {
         Task<TokenResponse> CreateAccessTokenAsync(string email, string password);
         Task<TokenResponse> RefreshTokenAsync(string refreshToken, string userEmail);
       
    }
}