namespace Examen_jose_lira.DTOs
{
    public class ProductosViewModel
    {
        // Lista de productos para mostrar en la tabla.
        public List<ProductoDTO> ListaProductos { get; set; } = new List<ProductoDTO>();

        // DTO vacío que se llenará desde el modal.
        public ProductoDTO NuevoProducto { get; set; } = new ProductoDTO();
    }
}
