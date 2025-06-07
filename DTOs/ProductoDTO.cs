using System.ComponentModel.DataAnnotations;

namespace Examen_jose_lira.DTOs
{
    public class ProductoDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del producto es requerido.")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "La marca del producto es requerida.")]
        public string? Marca { get; set; }

        [Required(ErrorMessage = "El costo del producto es requerido.")]
        public string? Costo { get; set; }

        [Required(ErrorMessage = "El precio de venta del producto es requerido.")]
        public string? PrecioVenta { get; set; }
    }
}
