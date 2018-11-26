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

        public int AddTask(UserViewModel user, string name, int ListId)
        {
            var model = new POCO.DbModels.Tasks
            {
                Title = name,
                CreatedDate = DateTime.Now,
                LastUpdate = DateTime.Now,
                TaskListId = ListId,
                Checked = false
                
            };
            _context.Tasks.Add(model);
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
                Id = x.Id,
                Title = x.Title
            }).ToList() : new List<TaskViewModel>();

            return result ?? new List<TaskViewModel>();
        }

        public TaskViewModel GetTask(UserViewModel user, int Id)
        {
            var listIds = _context.TaskList.Where(x => x.UserId == user.id).Select(x=> x.Id).ToList();

            var result = listIds != null ? _context.Tasks.Where(x => x.Id == Id && listIds.Contains(x.TaskListId)).Select(x => new TaskViewModel
            {
                Checked = x.Checked,
                CreatedDate = x.CreatedDate,
                Description = x.Description,
                LastUpdate = x.LastUpdate,
                Title = x.Title,
                Id = x.Id
                
            }).FirstOrDefault() : new TaskViewModel();

            return result ?? new TaskViewModel();
        }

        public bool CheckTask(UserViewModel user, int Id)
        {
            var listIds = _context.TaskList.Where(x => x.UserId == user.id).Select(x => x.Id).ToList();

            var model = _context.Tasks.FirstOrDefault(x => x.Id == Id && listIds.Contains(x.TaskListId));

            model.LastUpdate = DateTime.Now;
            model.Checked = !model.Checked;
            _context.SaveChanges();

            return model.Checked;
        }

        public int UpdateTask(UserViewModel user, TaskViewModel newmodel)
        {
            var listIds = _context.TaskList.Where(x => x.UserId == user.id).Select(x => x.Id).ToList();
            var model = _context.Tasks.FirstOrDefault(x => x.Id == newmodel.Id && listIds.Contains(x.TaskListId));
            model.Title = newmodel.Title;
            model.Description = newmodel.Description;
            model.Checked = newmodel.Checked;
            model.LastUpdate = DateTime.Now;
            _context.SaveChanges();

            return model.TaskListId;
        }
    }
}
