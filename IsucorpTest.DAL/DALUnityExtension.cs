using IsucorpTest.DAL.Repositories;
using IsucorpTest.DAL.Repositories.Interfaces;
using IsucorpTest.Model.DBModel;
using Microsoft.Practices.Unity;

namespace IsucorpTest.DAL
{
    public class DALUnityExtension : UnityContainerExtension  
    {
        protected override void Initialize()
        {
            Container.RegisterInstance(new IsucorpTestContext(), new PerResolveLifetimeManager());

            var contextInjectionConstructor = new InjectionConstructor(new ResolvedParameter<IsucorpTestContext>());            

            Container.RegisterType<ICollectionRepository<Contact>, CollectionRepository<Contact>>();
            Container.RegisterType<ICollectionRepository<ContactType>, CollectionRepository<ContactType>>();
        }
    }

}
