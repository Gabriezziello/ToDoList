using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTestToDoList.POCO.ViewModels;

namespace TechTestToDoList.Service.Interface
{
    public interface ITaskService
    {
        List<TaskListViewModel> GetTaskLists(UserViewModel user);

        int AddTaskList(UserViewModel user, string name);

        List<TaskViewModel> GetTasks(UserViewModel user, int ListId);
    }
}
