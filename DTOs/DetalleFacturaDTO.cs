namespace Examen_jose_lira.DTOs
{
    public class DetalleFacturaDTO
    {
        public int Id { get; set; }

        public int? IdFactura { get; set; }

        public int? IdProducto { get; set; }

        public int? Cantidad { get; set; }
        public string NombreProducto { get; set; }
        public double Costo { get; set; }
        public double Total { get; set; }
    }
}
