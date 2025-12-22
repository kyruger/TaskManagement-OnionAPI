using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Infrastructure.Repositorys;
using TaskManagent.Domain.Interfaces;
using TaskManagement.Application.DTOs.Tasks;

using TaskManagent.Domain.Enums;
using TaskManagent.Domain.Entities.Concrete;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TasksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _unitOfWork.Tasks.GetAllAsync();

            var result= tasks
                .Where(t=> !t.IsDeleted)
                .Select(t => new TaskDTO
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Status = t.Status,
                    Priority = t.Priority,
                    DueDate = t.DueDate
                });


            return Ok(result);
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            var task = await _unitOfWork.Tasks.GetByIdAsync(id);

            if(task == null || task.IsDeleted)
            {
                return NotFound();
            }

            return Ok(new TaskDTO
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                Priority = task.Priority,
                DueDate = task.DueDate
            });
        }   
        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskDTO model)
        {
            var task = new TaskItem
            {
                Title = model.Title,
                Description = model.Description,
                Priority = model.Priority,
                DueDate = model.DueDate,
                Status = ToDoStatus.ToDo,
                AppUserId = model.AppUserId

            };
            await _unitOfWork.Tasks.AddAsync(task);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = task.Id }, null);
        }
        [HttpPut]
        public async Task<IActionResult> Update(int id,UpdateTaskDTO model)
        {
            var task = await _unitOfWork.Tasks.GetByIdAsync(id);

            if(task == null || task.IsDeleted)
            {
                return NotFound();
            }

            task.Title = model.Title;
            task.Description = model.Description;
            task.Status = model.Status;
            task.Priority = model.Priority;
            task.DueDate = model.DueDate;
           
            _unitOfWork.Tasks.Update(task);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id) 
        {   
            var task = await _unitOfWork.Tasks.GetByIdAsync(id);


            if(task == null || task.IsDeleted)
            {
                return NotFound();
            }


            task.IsDeleted = true;
            _unitOfWork.Tasks.Update(task);
            await _unitOfWork.SaveChangesAsync();


            return NoContent();
        }


    }
}
