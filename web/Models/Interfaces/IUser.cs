using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;
using web.Models.DTO;

namespace web.Models.Interfaces
{
    public interface IUser
    {

    public Task<UserDTO> Register(RegisterUserDTO registerUser, ModelStateDictionary modelState);

    public Task<UserDTO> Authenticate(string username, string password);

        public Task<UserDTO> GetUser(ClaimsPrincipal principal);
    }
}
