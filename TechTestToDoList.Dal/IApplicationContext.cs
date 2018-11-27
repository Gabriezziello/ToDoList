using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTestToDoList.POCO.DbModels;

namespace TechTestToDoList.Dal
{
    public interface IApplicationContext
    {
        int SaveChanges();
        DbSet<User> Users { get; set; }
        DbSet<TaskList> TaskList { get; set; }
        DbSet<Tasks> Tasks { get; set; }
    }
}
