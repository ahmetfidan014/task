using System.Web.Mvc;
using Task.BLL.Abstract;
using Task.BLL.Managers;
using Task.BLL.Tools;
using Task.DAL;
using Task.DAL.Models;
using Task.DAL.Repository;
using Unity;
using Unity.Mvc5;

namespace Task
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
             container.RegisterType<IRepository<Product>, GenericRepository<Product>>();
             container.RegisterType<IRepository<Photos>, GenericRepository<Photos>>();
             container.RegisterType<IRepository<User>, GenericRepository<User>>();
             container.RegisterType<IProductService, ProductBLL>();
             container.RegisterType<IUserService, UserBLL>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}