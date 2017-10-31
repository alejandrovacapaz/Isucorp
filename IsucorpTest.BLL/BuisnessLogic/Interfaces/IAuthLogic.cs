using IsucorpTest.Model.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Threading.Tasks;
using IsucorpTest.Model.DBModel;

namespace IsucorpTest.BLL.BuisnessLogic.Interfaces
{
    public interface IAuthLogic
    {
        Task<IdentityResult> RegisterUserAsync(Register model);       
        Task<SignInStatus> SignInUserAsync(Register model, bool isPersistent, bool rememberBrowser);
        Task<bool> IsUserInRole(string userId, string roleName);
        IEnumerable<AuthUserViewModel> GetUsersToEnable();
        Task<bool> EnableUser(string userId, bool enabled, string url);
        Task<bool> ValidateEmailToken(string userId, string token);
        void SignOut();
        Task<ResetPasswordResult> ForgotPassword(ForgotPassword forgotPassword, string actionUrl);
        Task<IdentityResult> ResetPassword(ResetPassword resetPassword);
        bool ExistsUser(string email);
        Task<IdentityResult> ConfirmEmailToken(string userId, string token);
        IEnumerable<AuthUser> GetUsers();
    }
}
