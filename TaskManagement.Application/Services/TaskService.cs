using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.DTOs.Tasks;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities.Concrete;
using TaskManagement.Domain.Enums;
using TaskManagement.Domain.Interfaces;

namespace TaskManagement.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TaskService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> CreateAsync(CreateTaskDTO model)
        {
            var task =  _mapper.Map<TaskItem>(model);

            await _unitOfWork.Tasks.AddAsync(task);
            await _unitOfWork.SaveChangesAsync();

            return task.Id;

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var task = await _unitOfWork.Tasks.GetByIdAsync(id);

            if (task == null || task.IsDeleted)
            {
                return false;
            }

            task.IsDeleted = true;
            _unitOfWork.Tasks.Update(task);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TaskDTO>> GetAllAsync()
        {
            var tasks = await _unitOfWork.Tasks.GetAllAsync();

            var filteredTasks = tasks.Where(t => !t.IsDeleted);

            return _mapper.Map<IEnumerable<TaskDTO>>(filteredTasks);

        }

        public async Task<TaskDTO?> GetByIdAsync(int id)
        {
            var task = await _unitOfWork.Tasks.GetByIdAsync(id);

            if(task==null|| task.IsDeleted)
            {
                return null;
            }


            return _mapper.Map<TaskDTO>(task);

        }

        public async Task<bool> UpdateAsync(int id, UpdateTaskDTO model)
        {
            var task = await _unitOfWork.Tasks.GetByIdAsync(id);


            if (task == null || task.IsDeleted)
            {
                return false;
            }

            _mapper.Map(model, task);
            _unitOfWork.Tasks.Update(task);
            await _unitOfWork.SaveChangesAsync();

            return true;


        }
    }
}
