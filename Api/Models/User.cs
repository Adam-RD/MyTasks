using System.Text.Json.Serialization;

namespace Api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
       
        [JsonIgnore]
        public ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
    }
}
