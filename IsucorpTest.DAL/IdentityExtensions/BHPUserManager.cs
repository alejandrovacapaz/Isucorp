using System;
using IsucorpTest.Model.DBModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using Owin;

namespace IsucorpTest.DAL.IdentityExtensions
{
    public class BHPUserManager : UserManager<AuthUser>
    {
        public BHPUserManager(IsucorpTestContext context, IDataProtectionProvider protectionProvider, UserStore<AuthUser> userStore) : base(userStore)
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

        public static void InitializeUserManager(BHPUserManager manager, IAppBuilder app)
        {
            manager.UserValidator = new UserValidator<AuthUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            var dataProtectionProvider = app.GetDataProtectionProvider();

            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<AuthUser>(
                  dataProtectionProvider.Create("ASP.NET Identity"))
                {
                    TokenLifespan = TimeSpan.FromHours(3)
                };
            }
        }
    }
}
