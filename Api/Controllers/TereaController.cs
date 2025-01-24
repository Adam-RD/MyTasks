using Api.DTOs;
using Api.Models;
using Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] 
    public class TareaController : ControllerBase
    {
        private readonly ITareaRepository _taskRepository;

        public TareaController(ITareaRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");

           
            var tasks = await _taskRepository.GetTasksByUserIdAsync(userId);
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");

           
            var task = await _taskRepository.GetTaskByIdAsync(id);
            if (task == null || task.UserId != userId)
                return Unauthorized(new { message = "You are not authorized to access this task." });

            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> AddTask([FromBody] TareaDto taskDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");

           
            var task = new Tarea
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                Priority = taskDto.Priority,
                Status = taskDto.Status,
                DueDate = taskDto.DueDate,
                UserId = userId 
            };

            await _taskRepository.AddTaskAsync(task);

            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TareaDto taskDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");

            var existingTask = await _taskRepository.GetTaskByIdAsync(id);
            if (existingTask == null || existingTask.UserId != userId)
                return Unauthorized(new { message = "You are not authorized to update this task." });

            existingTask.Title = taskDto.Title;
            existingTask.Description = taskDto.Description;
            existingTask.Priority = taskDto.Priority;
            existingTask.Status = taskDto.Status;
            existingTask.DueDate = taskDto.DueDate;

            await _taskRepository.UpdateTaskAsync(existingTask);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");

            var existingTask = await _taskRepository.GetTaskByIdAsync(id);
            if (existingTask == null || existingTask.UserId != userId)
                return Unauthorized(new { message = "You are not authorized to delete this task." });

            await _taskRepository.DeleteTaskAsync(id);
            return NoContent();
        }
    }
}
