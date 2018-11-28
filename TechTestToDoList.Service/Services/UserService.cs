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

        public UserViewModel Create(RegisterViewModel register)
        {
            try
            {
                var model = new Dal.DbModels.User()
                {
                    Password = register.HashPassword,
                    UserName = register.Email,
                    DateUpdated = DateTime.Now
                };

                _context.Users.Add(model);
                _context.SaveChanges();
                return model != null ? new UserViewModel { id = model.Id, username = model.UserName, sessionStartDate = DateTime.Now } : null;
            }
            catch
            {
                return null;
            }
        }
    }
}
