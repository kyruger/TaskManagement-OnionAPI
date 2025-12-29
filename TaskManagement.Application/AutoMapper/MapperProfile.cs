using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.DTOs.Tasks;
using TaskManagement.Domain.Entities.Concrete;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.AutoMapper
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<CreateTaskDTO, TaskItem>().ForMember(x => x.Status, y => y.MapFrom(_ => ToDoStatus.ToDo));
            CreateMap<UpdateTaskDTO,TaskItem>().ReverseMap();
            CreateMap<TaskItem,TaskDTO>().ReverseMap();

        }
    }
}
