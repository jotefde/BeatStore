using BeatStore.API.DTO;
using BeatStore.API.DTO.Responses;
using BeatStore.API.Entities;
using BeatStore.API.Helpers.Constants;
using BeatStore.API.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace BeatStore.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;

        public UserRepository(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> CheckPassword(AppUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public Task<StandardResponse> ConfirmEmail(string id, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<StandardResponse> Create(AppUser appUser, string password)
        {
            appUser.Id = Guid.NewGuid().ToString();

            var identityResult = await _userManager.CreateAsync(appUser, password);
            if (!identityResult.Succeeded)
            {
                var errors = identityResult.Errors.Select(e => e.Description);
                return new StandardResponse(errors, HttpStatusCode.Conflict);
            }

            identityResult = await _userManager.AddToRoleAsync(appUser, UserRole.Customer);
            if (!identityResult.Succeeded)
            {
                var errors = identityResult.Errors.Select(e => e.Description);
                return new StandardResponse(errors, HttpStatusCode.Conflict);
            }

            //var token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
            //var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { token, email = user.Email }, Request.Scheme);
            //var tokenBytes = Encoding.UTF8.GetBytes(token);
            //var confirmationLink = "http://localhost/login/email-verification/" + appUser.Id + "/" + Convert.ToBase64String(tokenBytes);
            //var errors = identityResult.Succeeded ? null : identityResult.Errors.Select(e => new Error(StatusCodes.Status409Conflict, e.Description));
            return new StandardResponse(appUser.Id);
        }

        public Task<ValueResponse<string>> GenerateNewPasswordToken(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<AppUser> GetByUsername(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public Task<AppUser> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<string>> GetUserRoles(AppUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public Task<bool> IsEmailConfirmed(AppUser user)
        {
            throw new NotImplementedException();
        }

        public Task<StandardResponse> SetNewPassword(string userID, string token, string password)
        {
            throw new NotImplementedException();
        }

        public Task<ValueResponse<AppUser>> Update(AppUser user, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}
