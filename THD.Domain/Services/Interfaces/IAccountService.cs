using System.Threading.Tasks;
using THD.Domain.Models.AccountModels.Request;
using THD.Domain.Models.AccountModels.Response;

namespace THD.Domain.Services.Interfaces
{
    public interface IAccountService
    {
        Task<JwtAuthentificationResponse> Login(LoginRequest requestModel);
        Task<JwtAuthentificationResponse> RefreshToken(RefreshTokenRequest model);
        Task<UserResponse> CreateUser(CreateUserRequest createUserRequest);
        Task<GetAllUserResponse> GetAllUsers();
        Task<UserResponse> GetUserById(string Id);
        Task<string> UpdateUser(UpdateUserRequest updateUserRequest);
        Task<string> DeleteUser(string id);
    }
}
