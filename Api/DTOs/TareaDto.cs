using System.ComponentModel.DataAnnotations;

namespace Api.DTOs
{
    public class TareaDto
    {
        [Required]
        [MaxLength(100)] 
        public string Title { get; set; } = string.Empty;

        [MaxLength(500)] 
        public string Description { get; set; }

        [Required]
        [RegularExpression("Alta|Media|baja", ErrorMessage = "Debe ser 'Alta', 'Media', o 'Baja'")]
        public string Priority { get; set; }

        [Required]
        [RegularExpression("Pendiente|En Progreso|Completo", ErrorMessage = "Stado debe ser 'Pendiente', 'En Progreso', o 'Completo'")]
        public string Status { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}

