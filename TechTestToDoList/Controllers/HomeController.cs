using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechTestToDoList.Filters;
using TechTestToDoList.POCO.ViewModels;
using TechTestToDoList.Service.Interface;

namespace TechTestToDoList.Controllers
{
    [UserAuthenticationFilter]
    public class HomeController : Controller
    {
        private readonly ITaskService _taskService;

        public HomeController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public ActionResult Index()
        {
            var tasks = _taskService.GetTaskLists((UserViewModel)Session["User"]);
            return View(tasks);
        }

        public ActionResult AddTaskList(string Name)
        {
            var result = _taskService.AddTaskList((UserViewModel)Session["User"], Name);

            return Json(result,JsonRequestBehavior.AllowGet );       

        }

        public ActionResult GetTasks(int ListId)
        {
            

            return View();
        }
    }
}