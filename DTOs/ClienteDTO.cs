using System.ComponentModel.DataAnnotations;

namespace Examen_jose_lira.DTOs
{
    public class ClienteDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del usuario es requerido.")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El domicilio del usuario es requerido.")]
        public string? Domicilio { get; set; }

        [Required(ErrorMessage = "El email del usuario es requerido.")]
        public string? Email { get; set; }
    }
}
