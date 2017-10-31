using IsucorpTest.BLL.BuisnessLogic.Interfaces;
using IsucorpTest.DAL.IdentityExtensions;
using IsucorpTest.Model.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Threading.Tasks;
using IsucorpTest.Model.DBModel;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IsucorpTest.BLL.BuisnessLogic
{
    public class AuthLogic : IAuthLogic
    {
        private readonly RoleManager<IdentityRole> _roleManager;      
        private readonly IAuthenticationManager _authenticationManager;
        private readonly GPSignInManager _signInManager;
        private readonly GPUserManager _userManager;     

        public AuthLogic(GPUserManager userManager, GPSignInManager signInManager, IAuthenticationManager authenticationManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;           
            _authenticationManager = authenticationManager;
            _roleManager = roleManager;        
        }

        public async Task<IdentityResult> RegisterUserAsync(Register model)
        {
            var user = new AuthUser { FirstName = model.FirstName, LastName = model.LastName, UserName = model.Email, Email = model.Email, Description = model.Description };
            var result = await _userManager.CreateAsync(user, model.Password);

            // If user added then assign User role
            if (result.Succeeded)
            {
                result = await _userManager.AddToRoleAsync(user.Id.ToString(), Core.Constants.RoleName_User);
            }
            return result;
        }

        public async Task<SignInStatus> SignInUserAsync(Login model, bool isPersistent)
        {
            return await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
        }

        public async Task<SignInStatus> SignInUserAsync(Register model, bool isPersistent, bool rememberBrowser)
        {
            return await _signInManager.PasswordSignInAsync(model.Email, model.Password, rememberBrowser, shouldLockout: false);
        }

        public async Task<bool> IsUserInRole(string userId, string roleName)
        {
            if (!string.IsNullOrWhiteSpace(userId) && !string.IsNullOrWhiteSpace(roleName))
                return await _userManager.IsInRoleAsync(userId, roleName);
            return false;
        }

        public IEnumerable<AuthUserViewModel> GetUsersToEnable()
        {           
            var adminRole = _roleManager.Roles.FirstOrDefault(r => r.Name == Core.Constants.RoleName_Admin);
            if (adminRole == null)
                return null;

            var users = from user in _userManager.Users
                        where user.Roles.All(r => r.RoleId != adminRole.Id) && user.AdminEnabled == null
                        select user;
            return users.ToList().Select(u => new AuthUserViewModel(u)).ToList();            
        }

        public async Task<bool> EnableUser(string userId, bool enabled, string url)
        {
            var user = _userManager.FindById(userId);
            if (user == null)
                return false;

            user.AdminEnabled = enabled;

            var result = await _userManager.UpdateAsync(user);
            if (enabled)
                await SendEmailValidationToken(user.Email, url);

            return result.Succeeded;
        }

        public async Task<bool> ValidateEmailToken(string userId, string token)
        {
            await ConfirmEmailToken(userId, token);
            return true;
        }

        public void SignOut()
        {
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        public async Task<ResetPasswordResult> ForgotPassword(ForgotPassword forgotPassword, string actionUrl)
        {
            var user = await _userManager.FindByEmailAsync(forgotPassword.Email);
            if (user == null)
                return new ResetPasswordResult(false, ResetPasswordError.InvalidEmail);

            try
            {
                var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user.Id.ToString());

                var url = GenerateTokenUrl(actionUrl, Core.Constants.SendGridAuthLink_ResetPassword, user.Id.ToString(), resetToken);
               
                return new ResetPasswordResult(true);
            }
            catch (Exception e)
            {
                return new ResetPasswordResult(false, ResetPasswordError.Exception, e);
            }
        }

        public async Task<IdentityResult> ResetPassword(ResetPassword resetPassword)
        {
            return await _userManager.ResetPasswordAsync(resetPassword.UserId, resetPassword.Code, resetPassword.Password);
        }

        public bool ExistsUser(string email)
        {
            return _userManager.FindByEmail(email) != null;
        }

        public IEnumerable<AuthUser> GetUsers()
        {
            return _userManager.Users;
        }
       
        public async Task<IdentityResult> ConfirmEmailToken(string userId, string token)
        {
            var user = await _userManager.ConfirmEmailAsync(userId, token);
            return user;
        }

        public async Task<string> GenerateEmailToken(string userId)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(userId);
        }

        private Uri GenerateTokenUrl(string actionUrl, string oauthLink, params string[] tokens)
        {
            Uri callbackUrl = new Uri(actionUrl);

            var paramList = new List<string>();
            paramList.AddRange(tokens.Select(t => HttpUtility.UrlEncode(t)));

            var confirmUrl = string.Format(oauthLink, paramList.ToArray());

            callbackUrl = new Uri(callbackUrl, confirmUrl);

            return callbackUrl;
        }

        private async Task<bool> SendEmailValidationToken(string email, string actionUrl)
        {
            AuthUser user = _userManager.FindByEmail(email);

            if (user == null)
                return false;

            var token = await GenerateEmailToken(user.Id.ToString());

            Uri callbackUrl = GenerateTokenUrl(actionUrl, Core.Constants.SendGridAuthLink_ConfirmEmail, user.Id.ToString(), token);
         
            return true;
        }
    }
}
