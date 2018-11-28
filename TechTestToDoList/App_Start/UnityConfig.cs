using System.Web.Mvc;
using TechTestToDoList.Dal;
using TechTestToDoList.Service.Interface;
using TechTestToDoList.Service.Services;
using Unity;
using Unity.Mvc5;

namespace TechTestToDoList
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IApplicationContext, ApplicationContext>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<ITaskService, TaskService>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}