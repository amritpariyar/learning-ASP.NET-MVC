using SOIT.Data;
using SOIT.Repos.Interface;
using SOIT.Repos.Repository;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace SOIT
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IEntityBaseRepository<Province>, EntityBaseRepository<Province>>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}