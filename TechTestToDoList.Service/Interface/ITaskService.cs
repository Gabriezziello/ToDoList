using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTestToDoList.Dal.ViewModels;

namespace TechTestToDoList.Service.Interface
{
    public interface ITaskService
    {
        List<TaskListViewModel> GetTaskLists(UserViewModel user);
        int AddTaskList(UserViewModel user, string name);
        List<TaskViewModel> GetTasks(UserViewModel user, int ListId);
        TaskViewModel GetTask(UserViewModel user, int Id);
        int AddTask(UserViewModel user, string name, int ListId);
        bool CheckTask(UserViewModel user, int Id);
        int UpdateTask(UserViewModel user, TaskViewModel newmodel);
        bool RemoveListTask(UserViewModel user, int ListId);
        bool RemoveTask(UserViewModel user, int Id);
    }
}
