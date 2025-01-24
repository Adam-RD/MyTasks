using Api.Models;

namespace Api.Repositories.Interfaces
{
    public interface ITareaRepository
    {
        Task<IEnumerable<Tarea>> GetAllTasksAsync();
        Task<Tarea> GetTaskByIdAsync(int id);
        Task<IEnumerable<Tarea>> GetTasksByUserIdAsync(int userId);
        Task AddTaskAsync(Tarea task);
        Task UpdateTaskAsync(Tarea task);
        Task DeleteTaskAsync(int id);
        Task<bool> UserExistsAsync(int userId);
    }
}
