using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTestToDoList.POCO.DbModels;

namespace TechTestToDoList.Dal
{
    public class ApplicationContext : DbContext
    {

        public ApplicationContext() : base("DefaultConnection")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TaskList> TaskList { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        //public DbSet<Enrollment> Enrollments { get; set; }
        //public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<User>().HasKey(c => new { c.Id });
            modelBuilder.Entity<TaskList>().ToTable("TaskList");
            modelBuilder.Entity<TaskList>().HasKey(c => new { c.Id });
            modelBuilder.Entity<Tasks>().ToTable("Tasks");
            modelBuilder.Entity<Tasks>().HasKey(c => new { c.Id });

        }

    }

}
