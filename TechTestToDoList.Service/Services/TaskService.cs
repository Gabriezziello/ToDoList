using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTestToDoList.Dal;
using System.Data.Entity;
using TechTestToDoList.Service.Interface;
using TechTestToDoList.Dal.ViewModels;

namespace TechTestToDoList.Service.Services
{
    public class TaskService : ITaskService
    {
        private readonly IApplicationContext _context;

        public TaskService(IApplicationContext context)
        {
            _context = context;
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
            var model = new Dal.DbModels.TaskList
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
            var model = new Dal.DbModels.Tasks
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

            var result = list != null ? _context.Tasks.Where(x => x.TaskListId == ListId).Select(x => new TaskViewModel
            {
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
            var listIds = _context.TaskList.Where(x => x.UserId == user.id).Select(x => x.Id).ToList();

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
            try
            {
                var listIds = _context.TaskList.Where(x => x.UserId == user.id).Select(x => x.Id).ToList();
                var model = _context.Tasks.FirstOrDefault(x => x.Id == Id && listIds.Contains(x.TaskListId));
                model.LastUpdate = DateTime.Now;
                model.Checked = !model.Checked;
                _context.SaveChanges();

                return model.Checked;
            }
            catch
            {
                return false;
            }

        }

        public bool RemoveTask(UserViewModel user, int Id)
        {
            var listIds = _context.TaskList.Where(x => x.UserId == user.id).Select(x => x.Id).ToList();
            if (listIds.Count == 0)
                return false;

            var model = _context.Tasks.FirstOrDefault(x => x.Id == Id && listIds.Contains(x.TaskListId));


            _context.Tasks.Remove(model);
            _context.SaveChanges();

            return true;

        }

        public bool RemoveListTask(UserViewModel user, int ListId)
        {
            try
            {
                var model = _context.TaskList.FirstOrDefault(x => x.UserId == user.id && x.Id == ListId);
                var tasklist = _context.Tasks.Where(x => x.TaskListId == ListId).Select(x => x);
                _context.TaskList.Remove(model);
                foreach(var item in tasklist)
                {
                    _context.Tasks.Remove(item);
                }
                
                _context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
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
