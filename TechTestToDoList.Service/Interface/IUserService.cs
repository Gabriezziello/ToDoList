using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTestToDoList.POCO.ViewModels;

namespace TechTestToDoList.Service.Interface
{
    public interface IUserService
    {
        UserViewModel Login(LoginViewModel model);
    }
}
