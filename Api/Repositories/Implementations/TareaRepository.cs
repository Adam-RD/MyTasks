using Api.Data;
using Api.Models;
using Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories.Implementations
{
    public class TareaRepository : ITareaRepository
    {
        private readonly AppDbContext _context;

        public TareaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UserExistsAsync(int userId)
        {
            return await _context.Users.AnyAsync(u => u.Id == userId);
        }

        public async Task<IEnumerable<Tarea>> GetAllTasksAsync()
        {
            return await _context.Tareas.Include(t => t.User).ToListAsync();
        }

        public async Task<Tarea> GetTaskByIdAsync(int id)
        {
            return await _context.Tareas.Include(t => t.User).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Tarea>> GetTasksByUserIdAsync(int userId)
        {
            return await _context.Tareas.Where(t => t.UserId == userId).ToListAsync();
        }

        public async Task AddTaskAsync(Tarea task)
        {
            await _context.Tareas.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(Tarea task)
        {
            _context.Tareas.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = await GetTaskByIdAsync(id);
            if (task != null)
            {
                _context.Tareas.Remove(task);
                await _context.SaveChangesAsync();
            }
        }
    }
}
