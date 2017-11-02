using IsucorpTest.DAL.IdentityExtensions;
using IsucorpTest.Model.DBModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.DataProtection;

namespace IsucorpTest.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<IsucorpTest.DAL.IsucorpTestContext>
    {
      
        public Configuration()
        {                  
            AutomaticMigrationsEnabled = true;
        }
      
        protected override void Seed(IsucorpTestContext context)
        {
            //In casse debug is needed
            //if (!System.Diagnostics.Debugger.IsAttached)
            //    System.Diagnostics.Debugger.Launch();
            InitialContactTypes(context);
            InitialContacts(context);
        }

        private void InitialContactTypes(IsucorpTestContext context)
        {
            var contactTypes = context.ContactTypes.Count();
            // add initial ContactTypes here
            if (contactTypes <= 0)
            {
                context.ContactTypes.Add(new ContactType { Name = "ContactType1" });
                context.ContactTypes.Add(new ContactType { Name = "ContactType2" });
                context.ContactTypes.Add(new ContactType { Name = "ContactType3" });
            }
            context.SaveChanges();
        }

        private void InitialContacts(IsucorpTestContext context)
        {
            var contacts = context.Contacts.Count();
            // add initial contacts for Contact List
            if (contacts <= 0)
            {
                context.Contacts.Add(new Contact { Name = "David Gilmour", PhoneNumber = "(01) 7777-7771", BirthDate = new System.DateTime(1981, 05, 31), ContactTypeId = 1, ContactType = context.ContactTypes.First(x => x.Id == 1) });
                context.Contacts.Add(new Contact { Name = "Paul McCartney", PhoneNumber = "(01) 7777-7772", BirthDate = new System.DateTime(1982, 06, 1), ContactTypeId = 2, ContactType = context.ContactTypes.First(x => x.Id == 2) });
                context.Contacts.Add(new Contact { Name = "Roger Waters", PhoneNumber = "(01) 7777-7773", BirthDate = new System.DateTime(1983, 07, 15), ContactTypeId = 3, ContactType = context.ContactTypes.First(x => x.Id == 3) });
                context.Contacts.Add(new Contact { Name = "Paul Hewson", PhoneNumber = "(01) 7777-7774", BirthDate = new System.DateTime(1984, 08, 1), ContactTypeId = 1, ContactType = context.ContactTypes.First(x => x.Id == 1) });
                context.Contacts.Add(new Contact { Name = "Robert Plant", PhoneNumber = "(01) 7777-7775", BirthDate = new System.DateTime(1985, 09, 15), ContactTypeId = 2, ContactType = context.ContactTypes.First(x => x.Id == 2) });
                context.Contacts.Add(new Contact { Name = "Axl Rose", PhoneNumber = "(01) 7777-7776", BirthDate = new System.DateTime(1986, 10, 1), ContactTypeId = 3, ContactType = context.ContactTypes.First(x => x.Id == 3) });
            }
            context.SaveChanges();
        }

        private void InitialRoles(IsucorpTestContext context)
        {
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // Admin
            if (!RoleManager.RoleExists(Core.Constants.RoleName_Admin))
            {
                RoleManager.Create(new IdentityRole(Core.Constants.RoleName_Admin));
            }

            // User
            if (!RoleManager.RoleExists(Core.Constants.RoleName_User))
            {
                RoleManager.Create(new IdentityRole(Core.Constants.RoleName_User));
            }
        }

        private void InitialAdmin(IsucorpTestContext context)
        {
            var userManager = new GPUserManager(context, new DpapiDataProtectionProvider("Default Provider"), new UserStore<AuthUser>(context));

            if (userManager.FindByEmail(Core.Constants.DefaultAdmin_Email) != null)
                return;

            // Create and save user
            AuthUser superAdmin = new AuthUser
            {
                Email = Core.Constants.DefaultAdmin_Email,
                EmailConfirmed = true,
                UserName = Core.Constants.DefaultAdmin_Email
            };

            var result = userManager.Create(superAdmin, Core.Constants.DefaultAdmin_Password);

            // Get and assign admin role Role
            if (result.Succeeded)
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var adminRole = roleManager.FindByName(Core.Constants.RoleName_Admin);

                var sa = userManager.FindByEmail(Core.Constants.DefaultAdmin_Email);
                userManager.AddToRole(sa.Id, Core.Constants.RoleName_Admin);
            }
        }
    }
}
