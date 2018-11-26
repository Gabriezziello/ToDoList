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
    public class HomeController : ControllerBase
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

        public ActionResult AddTask(string Name, int ListId)
        {
            var result = _taskService.AddTask((UserViewModel)Session["User"], Name, ListId);

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetTasks(int ListId)
        {
            var result = _taskService.GetTasks((UserViewModel)Session["User"], ListId);

            return View("_tasks", result);
        }

        public ActionResult GetTask(int Id)
        {
            var result = _taskService.GetTask((UserViewModel)Session["User"], Id);

            return View("_task", result);
        }

        public ActionResult CheckTask(int Id)
        {
            var result = _taskService.CheckTask((UserViewModel)Session["User"], Id);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateTask(TaskViewModel model)
        {
            var result = _taskService.UpdateTask((UserViewModel)Session["User"], model);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}