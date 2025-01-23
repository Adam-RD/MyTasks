namespace Api.Models
{
    public class Tarea
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public string Status { get; set; }  = string.Empty;
        public DateTime DueDate { get; set; }
        public int UserId { get; set; }
        public required User User { get; set; }
    }
}
