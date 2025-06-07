namespace Examen_jose_lira.DTOs
{
    public class ClientesViewModel
    {
        // Lista de clientes para mostrar en la tabla.
        public List<ClienteDTO> ListaClientes { get; set; } = new List<ClienteDTO>();

        // DTO vacío que se llenará desde el modal.
        public ClienteDTO NuevoCliente { get; set; } = new ClienteDTO();
    }
}
