using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Infrastructure.Repositorys;
using TaskManagent.Domain.Interfaces;
using TaskManagement.Application.DTOs.Tasks;

using TaskManagent.Domain.Enums;
using TaskManagent.Domain.Entities.Concrete;
using TaskManagement.Application.Interfaces;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           return Ok( await _taskService.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _taskService.GetByIdAsync(id);

            return result == null ? NotFound() : Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskDTO model)
        {
            var id = await _taskService.CreateAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = id }, null);
        }
        [HttpPut]
        public async Task<IActionResult> Update(int id, UpdateTaskDTO model)
        {
           var result = await _taskService.UpdateAsync(id, model);
            return result ? NoContent() : NotFound();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) 
        {   
            var result = await _taskService.DeleteAsync(id);

            return result ? NoContent() : NotFound();
        }


    }
}
