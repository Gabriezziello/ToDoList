using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTestToDoList.Dal.DbModels;

namespace TechTestToDoList.Dal
{
    public interface IApplicationContext
    {
        int SaveChanges();
        IDbSet<User> Users { get; set; }
        IDbSet<TaskList> TaskList { get; set; }
        IDbSet<Tasks> Tasks { get; set; }
    }
}
