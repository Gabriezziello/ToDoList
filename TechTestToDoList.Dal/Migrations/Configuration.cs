namespace TechTestToDoList.Dal.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TechTestToDoList.Dal.Helpers;

    internal sealed class Configuration : DbMigrationsConfiguration<TechTestToDoList.Dal.ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TechTestToDoList.Dal.ApplicationContext context)
        {
            //  This method will be called after migrating to the latest version.
            if(context.Users.Count(x=> x.UserName == "test") == 0)
            {
                var password = UserHelper.Encrypt("pwd123", "test");

                if (!string.IsNullOrEmpty(password))
                {
                    context.Users.Add(new Dal.DbModels.User { UserName = "test", DateUpdated = DateTime.Now, Password = UserHelper.Encrypt("pwd123", "test") });
                    context.SaveChanges();
                }
                
            }
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
