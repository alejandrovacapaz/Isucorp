using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IsucorpTest.Model.DBModel;
using IsucorpTest.Model.WebApiModel;
using Microsoft.AspNet.Identity;

namespace IsucorpTest.DAL.Repositories.Interfaces
{
    public interface IAuthRepository : IDisposable
    {
        Task<IdentityResult> RegisterUser(AuthUser userModel);
        Task<AuthUser> FindUser(string userName, string password);
        //Task<IEnumerable<OAuthClient>> FindClient(string clientId);
        
        //Task<OAuthUser> FindAsync(UserLoginInfo loginInfo);
        //Task<IdentityResult> CreateAsync(OAuthUser user);
        //Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login);
        //Task<bool> AddRefreshToken(RefreshTokens token);
        //Task<bool> RemoveRefreshToken(string refreshTokenId);
        //Task<bool> RemoveRefreshToken(RefreshTokens refreshToken);
        //Task<RefreshTokens> FindRefreshToken(string refreshTokenId);
        //List<RefreshToken> GetAllRefreshTokens();
        
        Task<ApiResult> ResetPassword(string userId, PasswordCheck passwords);
        Task<AuthUser> GetOAuthUserByEmail(string email);

        Task<string> GenerateEmailToken(string userId);
        Task<IdentityResult> ConfirmEmailToken(string userId, string token);
        Task<bool> IsEmailConfirmed(string userId);
        Task<AuthUser> FindUserByEmail(string email);

        Task<string> GenerateRecoverPasswordToken(string userId);
        Task<IdentityResult> ResetPassword(string userId, string token, string newPassword);

        Task<bool> IsEmailInUse(string email);

        Task<ApiResult> UpdateEmail(string oldEmail, string newEmail);
        Task<ApiResult> DeleteUserByEmail(string email);
        Task<ApiResult> DeleteUserById(string id);
    }
}
