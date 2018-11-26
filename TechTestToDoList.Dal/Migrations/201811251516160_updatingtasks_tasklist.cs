namespace TechTestToDoList.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatingtasks_tasklist : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaskList",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        Checked = c.Boolean(nullable: false),
                        TaskListId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TaskList", t => t.TaskListId, cascadeDelete: true)
                .Index(t => t.TaskListId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "TaskListId", "dbo.TaskList");
            DropForeignKey("dbo.TaskList", "UserId", "dbo.User");
            DropIndex("dbo.Tasks", new[] { "TaskListId" });
            DropIndex("dbo.TaskList", new[] { "UserId" });
            DropTable("dbo.Tasks");
            DropTable("dbo.TaskList");
        }
    }
}
