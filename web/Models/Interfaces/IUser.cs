using Microsoft.AspNetCore.Mvc.ModelBinding;
using web.Models.DTO;

namespace web.Models.Interfaces
{
    public interface IUser
    {

    public Task<UserDTO> Register(RegisterUserDTO registerUser, ModelStateDictionary modelState);

    public Task<UserDTO> Authenticate(string username, string password);
}
}
