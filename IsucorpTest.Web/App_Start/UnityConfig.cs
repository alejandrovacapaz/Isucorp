using IsucorpTest.BLL;
using Microsoft.Owin.Security;
using Microsoft.Practices.Unity;
using System;
using System.Web;

namespace IsucorpTest.Web.App_Start
{
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IAuthenticationManager>(new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));

            container.AddNewExtension<BLLUnityExtension>();
        }
    }
}