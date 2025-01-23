namespace Api.DTOs
{
    public class TareaDto
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Priority { get; set; }
        public required string Status { get; set; }
        public DateTime DueDate { get; set; }
        public int UserId
        {
            get; set;
        }
    }
}
