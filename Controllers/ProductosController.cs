using Examen_jose_lira.DTOs;
using Examen_jose_lira.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Examen_jose_lira.Controllers
{
    public class ProductosController : Controller
    {
        private readonly Examen_jose_lira_Context _BD;

        public ProductosController(Examen_jose_lira_Context context)
        {
            _BD = context;
        }

        //Cargar productos al ver la página.
        public async Task<IActionResult> Index()
        {
            var listaDto = await _BD.TProductos.Select(x => new ProductoDTO
            {
                Id = x.IdProducto,
                Nombre = x.Nombre,
                Marca = x.Marca,
                Costo = x.Costo.ToString().Replace('.', ','),
                PrecioVenta=x.PrecioVenta.ToString().Replace('.',',')
            }).ToListAsync();

            return View(new ProductosViewModel
            {
                ListaProductos = listaDto,
                NuevoProducto = new ProductoDTO()
            });
        }

        //Guardar productos.
        [HttpPost]
        public async Task<IActionResult> Guardar(ProductosViewModel vm)
        {
            using var transaction = await _BD.Database.BeginTransactionAsync();
            try
            {
                //Verificar si el modelo es valido.
                if (!ModelState.IsValid)
                {
                    var listaDto = await _BD.TProductos.Select(x => new ProductoDTO
                    {
                        Id = x.IdProducto,
                        Nombre = x.Nombre,
                        Marca = x.Marca,
                        Costo = x.Costo.ToString().Replace('.', ','),
                        PrecioVenta = x.PrecioVenta.ToString().Replace('.', ',')
                    }).ToListAsync();

                    vm.ListaProductos = listaDto;
                    TempData["ErrorMessage"] = "Por favor corrija los errores del formulario.";
                    return View("Index", vm);
                }

                //Verificar si guarda o actualiza.
                var existe = await _BD.TProductos.Where(x => x.IdProducto == vm.NuevoProducto.Id).FirstOrDefaultAsync();
                if (existe is null)
                {
                    //Guardando.
                    _BD.TProductos.Add(new TProducto
                    {
                        Nombre = vm.NuevoProducto.Nombre!,
                        Marca = vm.NuevoProducto.Marca!,
                        Costo = Convert.ToDecimal(vm.NuevoProducto.Costo),
                        PrecioVenta = Convert.ToDecimal(vm.NuevoProducto.PrecioVenta),
                    });
                    await _BD.SaveChangesAsync();
                    await transaction.CommitAsync();
                    TempData["SuccessMessage"] = "Producto guardado exitosamente!";
                }
                else
                {
                    existe.Nombre = vm.NuevoProducto.Nombre;
                    existe.Marca = vm.NuevoProducto.Marca;
                    existe.Costo = Convert.ToDecimal(vm.NuevoProducto.Costo);
                    existe.PrecioVenta = Convert.ToDecimal(vm.NuevoProducto.PrecioVenta);
                    await _BD.SaveChangesAsync();
                    await transaction.CommitAsync();
                    TempData["SuccessMessage"] = "Producto actualizado exitosamente!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                //Si ocurre algún error.
                await transaction.RollbackAsync();
                Console.WriteLine("Error al procesar la petición: " + ex.ToString());
                TempData["ErrorMessage"] = "Ocurrió un error.";

                var listaDto = await _BD.TProductos.Select(x => new ProductoDTO
                {
                    Id = x.IdProducto,
                    Nombre = x.Nombre,
                    Marca = x.Marca,
                    Costo = x.Costo.ToString().Replace('.', ','),
                    PrecioVenta = x.PrecioVenta.ToString().Replace('.', ',')
                }).ToListAsync();

                return View(new ProductosViewModel
                {
                    ListaProductos = listaDto,
                    NuevoProducto = new ProductoDTO()
                });
            }
        }
    }
}
