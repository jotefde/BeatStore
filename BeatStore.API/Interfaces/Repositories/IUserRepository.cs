using BeatStore.API.DTO.Responses;
using BeatStore.API.Entities;
using BeatStore.API.Helpers.Constants;

namespace BeatStore.API.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<StandardResponse> Create(AppUser user, string password);
        Task<IEnumerable<string>> GetUserRoles(AppUser user);
        Task<AppUser> GetByUsername(string username);
        Task<AppUser> GetById(string id);
        Task<bool> CheckPassword(AppUser user, string password);
        Task<bool> IsEmailConfirmed(AppUser user);
        Task<StandardResponse> ConfirmEmail(string id, string token);
        Task<ValueResponse<AppUser>> Update(AppUser user, string newPassword);
        Task<ValueResponse<string>> GenerateNewPasswordToken(string email);
        Task<StandardResponse> SetNewPassword(string userID, string token, string password);
    }
}
