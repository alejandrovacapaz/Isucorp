using IsucorpTest.DAL.IdentityExtensions;
using IsucorpTest.DAL.Repositories;
using IsucorpTest.DAL.Repositories.Interfaces;
using IsucorpTest.Model.DBModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Practices.Unity;

namespace IsucorpTest.DAL
{
    public class DALUnityExtension : UnityContainerExtension  
    {
        protected override void Initialize()
        {
            Container.RegisterInstance(new IsucorpTestContext(), new PerResolveLifetimeManager());

            var contextInjectionConstructor = new InjectionConstructor(new ResolvedParameter<IsucorpTestContext>());
            Container.RegisterType<IUserStore<AuthUser>, UserStore<AuthUser>>(contextInjectionConstructor);
            Container.RegisterType<RoleStore<IdentityRole>>(contextInjectionConstructor);           

            Container.RegisterType<GPUserManager>();

            var roleStoreInjectionConstructor = new InjectionConstructor(new ResolvedParameter<RoleStore<IdentityRole>>());
            Container.RegisterType<RoleManager<IdentityRole>>(roleStoreInjectionConstructor);

            Container.RegisterType<ICollectionRepository<Contact>, CollectionRepository<Contact>>();
            Container.RegisterType<ICollectionRepository<ContactType>, CollectionRepository<ContactType>>();

            Container.RegisterType<GPSignInManager>();
            Container.RegisterType<IAuthRepository, AuthRepository>();         
            Container.RegisterType<AuthUser>();
            Container.RegisterType<BHPUserManager>();           
        }
    }

}
