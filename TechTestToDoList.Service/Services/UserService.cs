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
    public class UserService : IUserService
    {
        private readonly ApplicationContext _context;

        public UserService(ApplicationContext context)
        {
            _context = context;
        }
        public UserViewModel Login(LoginViewModel model)
        {

            var result = _context.Users.FirstOrDefault(x => x.UserName == model.Email && x.Password == model.HashPassword);

            return result != null ? new UserViewModel { id = result.Id, username = result.UserName, sessionStartDate = DateTime.Now } : null;
        }
    }
}
