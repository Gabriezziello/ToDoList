using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTestToDoList.Dal;
using System.Data.Entity;
using TechTestToDoList.Service.Interface;
using TechTestToDoList.POCO.ViewModels;

namespace TechTestToDoList.Service.Services
{
    public class TaskService : ITaskService
    {
        private readonly ApplicationContext _context;

        public TaskService(ApplicationContext context)
        {
            _context = context;
        }
        public UserViewModel Login(LoginViewModel model)
        {

            var result = _context.Users.FirstOrDefault(x => x.UserName == model.Email && x.Password == model.HashPassword);

            return result != null ? new UserViewModel { id = result.Id, username = result.UserName, sessionStartDate = DateTime.Now } : null;
        }

        public List<TaskListViewModel> GetTaskLists(UserViewModel user)
        {
            var result = _context.TaskList.Where(x => x.UserId == user.id).Select(x => new TaskListViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return result ?? new List<TaskListViewModel>();
        }

        public int AddTaskList(UserViewModel user, string name)
        {
            var model = new POCO.DbModels.TaskList
            {
                Name = name,
                UserId = user.id
            };
            _context.TaskList.Add(model);
            _context.SaveChanges();

            return model.Id;
        }

        public List<TaskViewModel> GetTasks(UserViewModel user, int ListId)
        {
            var list = _context.TaskList.FirstOrDefault(x => x.Id == ListId && x.UserId == user.id);

            var result = list != null ? _context.Tasks.Where(x => x.TaskListId == ListId).Select(x=> new TaskViewModel {
                Checked = x.Checked,
                CreatedDate = x.CreatedDate,
                Description = x.Description,
                LastUpdate = x.LastUpdate,
                Title = x.Title
            }).ToList() : new List<TaskViewModel>();

            return result ?? new List<TaskViewModel>();
        }
    }
}
