using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.DTOs.Tasks
{
    public class UpdateTaskDTO
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public ToDoStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
