using IsucorpTest.Model.DBModel;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;

namespace IsucorpTest.DAL.IdentityExtensions
{
    // ReSharper disable once InconsistentNaming
    public class GPUserManager : UserManager<AuthUser>
    {
        public GPUserManager(IsucorpTestContext context, IDataProtectionProvider protectionProvider, IUserStore<AuthUser> userStore) : base(userStore)
        {
            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            UserTokenProvider = new DataProtectorTokenProvider<AuthUser>(protectionProvider.Create("ASP.NET Identity"));

            UserValidator = new UserValidator<AuthUser>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(AuthUser user)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityTokenAsync(AuthUser user)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await CreateIdentityAsync(user, DefaultAuthenticationTypes.ExternalBearer);
            // Add custom user claims here
            return userIdentity;
        }

    }
}

