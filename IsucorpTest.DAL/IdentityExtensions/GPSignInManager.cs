using IsucorpTest.Model.DBModel;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Constants = IsucorpTest.Core.Constants;

namespace IsucorpTest.DAL.IdentityExtensions
{
    // ReSharper disable once InconsistentNaming
    public class GPSignInManager : SignInManager<AuthUser, string>
    {
        public GPSignInManager(GPUserManager userManager, IAuthenticationManager authenticationManager)
                : base(userManager, authenticationManager)
        {

        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(AuthUser user)
        {
            return ((GPUserManager)UserManager).GenerateUserIdentityAsync(user);
        }

        public static GPSignInManager Create(IdentityFactoryOptions<GPSignInManager> options, IOwinContext context)
        {
            return new GPSignInManager(context.GetUserManager<GPUserManager>(), context.Authentication);
        }

        public override async Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            // Verify if user is admin enabled
            var user = await UserManager.FindByEmailAsync(userName);
            if (user == null) return await base.PasswordSignInAsync(userName, password, isPersistent, shouldLockout);
            var userIsAdmin = await UserManager.IsInRoleAsync(user.Id.ToString(), Constants.RoleName_Admin);
            if (!userIsAdmin && (user.AdminEnabled != true)) //|| !user.EmailConfirmed)
                return SignInStatus.RequiresVerification;

            return await base.PasswordSignInAsync(userName, password, isPersistent, shouldLockout);
        }
    }
}
