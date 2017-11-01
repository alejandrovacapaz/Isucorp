using IsucorpTest.BLL.BuisnessLogic;
using IsucorpTest.BLL.BuisnessLogic.Interfaces;
using IsucorpTest.DAL;
using Microsoft.Practices.Unity;

namespace IsucorpTest.BLL
{
    public class BLLUnityExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.AddNewExtension<DALUnityExtension>();
            Container.RegisterType<IAuthLogic, AuthLogic>();
            Container.RegisterType<IContactLogic, ContactLogic>();
            Container.RegisterType<IContactTypeLogic, ContactTypeLogic>();
        }
    }
}
