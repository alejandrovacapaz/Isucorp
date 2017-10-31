using System;
using System.Threading.Tasks;
using IsucorpTest.DAL.IdentityExtensions;
using IsucorpTest.DAL.Repositories.Interfaces;
using IsucorpTest.Model.DBModel;
using IsucorpTest.Model.WebApiModel;
using Microsoft.AspNet.Identity;

namespace IsucorpTest.DAL.Repositories
{
    public class AuthRepository : IAuthRepository
    {

        private readonly IsucorpTestContext _ctx;

        private readonly GPUserManager _userManager;
        private readonly IUserStore<AuthUser> _store;       


        public AuthRepository(IsucorpTestContext context, GPUserManager userManager, IUserStore<AuthUser> store)
        {
            _ctx = context;
            _userManager = userManager;
            _store = store;          
        }
      
        public async Task<AuthUser> FindUser(string userName, string password)
        {
            var user = await _userManager.FindAsync(userName, password);
            return user;
        }

        public async Task<IdentityResult> RegisterUser(AuthUser userModel)
        {
            var result = await _userManager.CreateAsync(userModel, userModel.PasswordHash);
            return result;
        }
      
        public async Task<ApiResult> ResetPassword(string userId, PasswordCheck passwords)
        {
            var result = new ApiResult(true);

            AuthUser cUser = await _store.FindByIdAsync(userId);
            if (await _userManager.CheckPasswordAsync(cUser, passwords.CurrentPassword))
            {              
                Exception ex = null;
                IdentityResult identityResult = null;

                try
                {
                    string token = await _userManager.GeneratePasswordResetTokenAsync(userId);
                    identityResult = await _userManager.ResetPasswordAsync(userId, token, passwords.NewPassword);               
                }
                catch (Exception e)
                {
                    ex = e;
                }
                finally
                {
                    if (!await _userManager.HasPasswordAsync(userId))
                    {
                        await _userManager.AddPasswordAsync(userId, passwords.CurrentPassword);

                        result.Success = false;

                        result.Errors = ex != null ? new[] { ex.Message } : (identityResult != null && !identityResult.Succeeded) ? identityResult.Errors : null;
                    }
                }
            }
            else
            {
                result.Success = false;
                result.Errors = new[] { "Wrong password" };
            }
            return result;
        }

        public async Task<AuthUser> GetOAuthUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<string> GenerateEmailToken(string userId)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(userId);
        }

        public async Task<IdentityResult> ConfirmEmailToken(string userId, string token)
        {
            return await _userManager.ConfirmEmailAsync(userId, token);
        }

        public async Task<bool> IsEmailConfirmed(string userId)
        {
            return await _userManager.IsEmailConfirmedAsync(userId);
        }

        public async Task<AuthUser> FindUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<string> GenerateRecoverPasswordToken(string userId)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(userId);
        }

        public async Task<IdentityResult> ResetPassword(string userId, string token, string newPassword)
        {
            return await _userManager.ResetPasswordAsync(userId, token, newPassword);
        }

        public async Task<bool> IsEmailInUse(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        /// <summary>
        /// Changes user email and username
        /// </summary>
        /// <param name="oldEmail">Used to find user by email</param>
        /// <param name="newEmail">New user email and username</param>
        /// <returns></returns>
        public async Task<ApiResult> UpdateEmail(string oldEmail, string newEmail)
        {
            var user = await FindUserByEmail(oldEmail);

            if (user == null)
                return new ApiResult(false, "Invalid user Email.");

            user.Email = newEmail;
            user.UserName = newEmail;
            user.EmailConfirmed = false;

            var res = await _userManager.UpdateAsync(user);

            if (!res.Succeeded)
                return new ApiResult(false, res.Errors);

            return new ApiResult(true, user);
        }

        /// <summary>
        /// Deletes an OAuthUser by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<ApiResult> DeleteUserByEmail(string email)
        {
            var user = await FindUserByEmail(email);

            if (user == null)
                return new ApiResult(false, "Invalid user Email.");

            var result = await _userManager.DeleteAsync(user);

            return new ApiResult(result.Succeeded, result.Errors);
        }

        /// <summary>
        /// Deletes an OAuthUser by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiResult> DeleteUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return new ApiResult(false, "Invalid user Email.");

            var result = await _userManager.DeleteAsync(user);

            return new ApiResult(result.Succeeded, result.Errors);
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();
        }

    }
}
