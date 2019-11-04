using Banijya.Data.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SOIT.Controllers;
using SOIT.Data;
using SOIT.Models;
using SOIT.Repos.Infrastructure;
using SOIT.Repos.Interface;
using SOIT.Repos.Repository;
using System.Data.Entity;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
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

            #region IdentityRegister           
            container.RegisterType<DbContext, ApplicationDbContext>(new HierarchicalLifetimeManager());
            container.RegisterType<UserManager<ApplicationUser>>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(new HierarchicalLifetimeManager());
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());
            container.RegisterType<IDbFactory, DbFactory>(new PerResolveLifetimeManager());
            container.RegisterType<IUnitOfWork, UnitOfWork>(new PerResolveLifetimeManager());
            #endregion
            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IEntityBaseRepository<Province>, EntityBaseRepository<Province>>();
            container.RegisterType<IEntityBaseRepository<UserProfile>, EntityBaseRepository<UserProfile>>();
            container.RegisterType<IEntityBaseRepository<UserQualification>, EntityBaseRepository<UserQualification>>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}