using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Data
{
    public static class DbInitializer
    {
        public static async Task Seed(AppDbContext context)
        {
            // Asegúrate de que la base de datos esté creada
            await context.Database.EnsureCreatedAsync();

            // Verifica si ya hay datos
            if (context.Users.Any() || context.Tareas.Any())
                return;

            // Usuarios iniciales
            var users = new List<User>
            {
                new User { Name = "John Doe", Email = "john@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123") },
                new User { Name = "Jane Smith", Email = "jane@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123") }
            };

            context.Users.AddRange(users);
            await context.SaveChangesAsync();

            // Tareas iniciales
            var tasks = new List<Tarea>
            {
                new Tarea
                {
                    Title = "Setup Backend",
                    Description = "Configure the backend project structure.",
                    Priority = "High",
                    Status = "Pending",
                    DueDate = DateTime.Now.AddDays(5),
                    UserId = users[0].Id
                },
                new Tarea
                {
                    Title = "Design Frontend",
                    Description = "Create the UI for the task management system.",
                    Priority = "Medium",
                    Status = "In Progress",
                    DueDate = DateTime.Now.AddDays(10),
                    UserId = users[1].Id
                }
            };

            context.Tareas.AddRange(tasks);
            await context.SaveChangesAsync();
        }
    }
}
