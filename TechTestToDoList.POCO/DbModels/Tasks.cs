using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTestToDoList.POCO.DbModels
{
    public partial class Tasks
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool Checked { get; set; }
        [ForeignKey("TaskList")]
        public int TaskListId { get; set; }
        public virtual TaskList TaskList { get; set; }        
    }
}
