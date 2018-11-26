using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTestToDoList.POCO.DbModels
{
    public partial class TaskList
    {
        [Key]
        public int Id{get;set;}
        public string Name { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        
    }
}
