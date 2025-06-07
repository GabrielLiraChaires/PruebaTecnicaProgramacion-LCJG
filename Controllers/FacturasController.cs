using Examen_jose_lira.DTOs;
using Examen_jose_lira.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Examen_jose_lira.Controllers
{
    public class FacturasController : Controller
    {
        private readonly Examen_jose_lira_Context _BD;

        public FacturasController(Examen_jose_lira_Context context)
        {
            _BD = context;
        }

        //Cargar facturas con sus respectivos detalles al ver la página.
        public async Task<IActionResult> Index()
        {
            var listaDto = await _BD.TFacturas.Select(x => new FacturaDTO
            {
                Id = x.IdFactura,
                NumeroFactura = x.NumeroFactura,
                FechaHora = x.FechaHora,
                Cliente = new ClienteDTO
                {
                    Id = x.IdClienteNavigation.IdCliente,
                    Nombre = x.IdClienteNavigation.Nombre,
                    Domicilio = x.IdClienteNavigation.Domicilio,
                    Email = x.IdClienteNavigation.Email
                },
                DetallesFactura = x.TDetalleFacturas
                .Select(z => new DetalleFacturaDTO
                {
                    Id = z.IdDetalle,
                    IdFactura = z.IdFactura,
                    IdProducto = z.IdProducto,
                    NombreProducto=z.IdProductoNavigation.Nombre,
                    Cantidad = z.Cantidad,
                    Costo = Convert.ToDouble(z.IdProductoNavigation.Costo),
                    Total = (Convert.ToDouble(z.IdProductoNavigation.Costo)*z.Cantidad) ?? new double()
                })
                .ToList()
            }).ToListAsync();


            return View(listaDto);
        }

        //Actualizar cantidad.
        [HttpPost]
        public async Task<IActionResult> ActualizarCantidad([FromBody] IdDetalleDTO model)
        {
            try
            {
                var detalle = await _BD.TDetalleFacturas.FindAsync(model.idDetalle);

                if (detalle == null)
                    return Json(new { success = false, message = "Detalle no encontrado" });

                detalle.Cantidad = model.cantidad;
                await _BD.SaveChangesAsync();

                // Obtener la factura completa actualizada
                var facturaActualizada = await _BD.TFacturas
                    .Where(f => f.TDetalleFacturas.Any(d => d.IdDetalle == model.idDetalle))
                    .Select(x => new FacturaDTO
                    {
                        Id = x.IdFactura,
                        NumeroFactura = x.NumeroFactura,
                        FechaHora = x.FechaHora,
                        Cliente = new ClienteDTO
                        {
                            Id = x.IdClienteNavigation.IdCliente,
                            Nombre = x.IdClienteNavigation.Nombre,
                            Domicilio = x.IdClienteNavigation.Domicilio,
                            Email = x.IdClienteNavigation.Email
                        },
                        DetallesFactura = x.TDetalleFacturas.Select(z => new DetalleFacturaDTO
                        {
                            Id = z.IdDetalle,
                            IdFactura = z.IdFactura,
                            IdProducto = z.IdProducto,
                            NombreProducto = z.IdProductoNavigation.Nombre,
                            Cantidad = z.Cantidad,
                            Costo = Convert.ToDouble(z.IdProductoNavigation.Costo),
                            Total = (Convert.ToDouble(z.IdProductoNavigation.Costo) * z.Cantidad) ?? new double()
                        }).ToList()
                    })
                    .FirstOrDefaultAsync();

                return Json(new
                {
                    success = true,
                    factura = facturaActualizada
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
