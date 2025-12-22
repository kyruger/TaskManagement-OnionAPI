using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagent.Domain.Entities.Concrete;
using TaskManagent.Domain.Enums;

namespace TaskManagement.Application.DTOs.Tasks
{
    public class CreateTaskDTO
    {
        public string Title { get; set; }= null!;
        public string? Description { get; set; }
        public TaskPriority Priority { get; set; }
        public DateTime? DueDate { get; set; }

        public int AppUserId { get; set; }

    }
}
