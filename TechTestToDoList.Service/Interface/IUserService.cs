using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTestToDoList.Dal.ViewModels;

namespace TechTestToDoList.Service.Interface
{
    public interface IUserService
    {
        UserViewModel Login(LoginViewModel model);
        UserViewModel Create(RegisterViewModel register);
    }
}
