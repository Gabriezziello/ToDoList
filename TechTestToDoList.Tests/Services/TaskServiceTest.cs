using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTestToDoList.Dal;
using TechTestToDoList.POCO.DbModels;
using TechTestToDoList.POCO.ViewModels;
using TechTestToDoList.Service.Interface;
using TechTestToDoList.Service.Services;

namespace TechTestToDoList.Tests.Services
{
    [TestClass]
    public class TaskServiceTest
    {
        private Mock<IApplicationContext> _context;
        private UserViewModel _sessionUser;
        private UserViewModel _sessionUserFake;

        [TestMethod]
        public void TestGetTaskList()
        {
            setUp();
            var service = new TaskService(_context.Object);
            var list = service.GetTaskLists(_sessionUser);
            var list_fake = service.GetTaskLists(_sessionUserFake);

            Assert.IsTrue(list.Count > 0);
            Assert.IsTrue(list_fake.Count == 0);
        }

        [TestMethod]
        public void TestGetTasks()
        {
            setUp();
            var service = new TaskService(_context.Object);
            var list = service.GetTasks(_sessionUser, 1);
            var list_fake = service.GetTasks(_sessionUserFake, 1);

            Assert.IsTrue(list.Count > 0);
            Assert.IsTrue(list_fake.Count == 0);
        }

        [TestMethod]
        public void TestGetTask()
        {
            setUp();
            var empty_model = new TaskViewModel();
            var service = new TaskService(_context.Object);
            var task = service.GetTask(_sessionUser, 1);
            var task_fake = service.GetTask(_sessionUserFake, 1);

            Assert.IsTrue(task != empty_model);
            Assert.AreEqual(task.Description, "Description 1");
            Assert.AreEqual(task.Title, "Task 1");            
            Assert.IsTrue(string.IsNullOrEmpty(task_fake.Description));
            Assert.IsTrue(string.IsNullOrEmpty(task_fake.Title));
        }

        [TestMethod]
        public void TestCheckTask()
        {
            setUp();            
            var service = new TaskService(_context.Object);
            var task = service.CheckTask(_sessionUser, 1);
            var task_fake = service.CheckTask(_sessionUserFake, 1);

            Assert.IsTrue(task);            
            Assert.IsFalse(task_fake);
            
        }

        [TestMethod]
        public void TestRemoveTask()
        {
            setUp();
            var service = new TaskService(_context.Object);
            var task_fake = service.RemoveTask(_sessionUserFake, 1);
            var task = service.RemoveTask(_sessionUser, 1);            
            var list = service.GetTasks(_sessionUser, 1);

            Assert.IsTrue(task);
            Assert.IsFalse(task_fake);
            Assert.IsTrue(list.Count == 2);

        }

        public void setUp()
        {
            var dataList = new List<TaskList>
            {
                new TaskList { Name = "List1", UserId = 1, Id=1 },
                new TaskList { Name = "List2", UserId = 1, Id=2 },
                new TaskList { Name = "List3", UserId = 1, Id=3 },
            }.AsQueryable();

            var dataTask = new List<Tasks>
            {
                new Tasks { TaskListId =1, Id=1, Checked = false, Description = "Description 1", Title = "Task 1" },
                new Tasks { TaskListId = 1, Id=2, Checked = false, Description = "Description 2", Title = "Task 2" },
                new Tasks { TaskListId = 1, Id=3, Checked = false, Description = "Description 3", Title = "Task 3" },
            }.AsQueryable();

            var mockSetList = new Mock<DbSet<TaskList>>();
            mockSetList.As<IQueryable<TaskList>>().Setup(m => m.Provider).Returns(dataList.Provider);
            mockSetList.As<IQueryable<TaskList>>().Setup(m => m.Expression).Returns(dataList.Expression);
            mockSetList.As<IQueryable<TaskList>>().Setup(m => m.ElementType).Returns(dataList.ElementType);
            mockSetList.As<IQueryable<TaskList>>().Setup(m => m.GetEnumerator()).Returns(dataList.GetEnumerator());

            var mockSetTasks = new Mock<DbSet<Tasks>>();
            mockSetTasks.As<IQueryable<Tasks>>().Setup(m => m.Provider).Returns(dataTask.Provider);
            mockSetTasks.As<IQueryable<Tasks>>().Setup(m => m.Expression).Returns(dataTask.Expression);
            mockSetTasks.As<IQueryable<Tasks>>().Setup(m => m.ElementType).Returns(dataTask.ElementType);
            mockSetTasks.As<IQueryable<Tasks>>().Setup(m => m.GetEnumerator()).Returns(dataTask.GetEnumerator());

            _context = new Mock<IApplicationContext>();
            _context.Setup(c => c.TaskList).Returns(mockSetList.Object);
            _context.Setup(c => c.Tasks).Returns(mockSetTasks.Object);

            _sessionUser = new UserViewModel()
            {
                id = 1,
                sessionStartDate = DateTime.Now,
                username = "testing"
            };

            _sessionUserFake = new UserViewModel()
            {
                id = 99,
                sessionStartDate = DateTime.Now,
                username = "fake"
            };
        }
    }
}
