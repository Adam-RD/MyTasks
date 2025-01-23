namespace Api.DTOs
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public List<TareaDto> Tareas { get; set; } 
            
            }
}
