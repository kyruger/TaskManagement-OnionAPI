using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagent.Domain.Entities.Abstract;
using TaskManagent.Domain.Enums;

namespace TaskManagent.Domain.Entities.Concrete
{
    public class TaskItem: BaseEntity
    {
        public string Title { get; set; } = null!;//null! this is a null-forgiving operator  came with C# 8.0
        public string? Description { get; set; }
        public TaskPriority Priority { get; set; }
        public ToDoStatus Status { get; set; }
        public DateTime? DueDate { get; set; }


        // Navigation property

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }= null!;
    }   
}
