using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTestToDoList.Dal.ViewModels
{
    public class UserViewModel
    {
        public int id { get; set; }
        public string username { get; set; }
        public DateTime sessionStartDate { get; set; }
    }
}
