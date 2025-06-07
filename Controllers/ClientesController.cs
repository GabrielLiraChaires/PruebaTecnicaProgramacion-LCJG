using Examen_jose_lira.DTOs;
using Examen_jose_lira.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Examen_jose_lira.Controllers
{
    public class ClientesController : Controller
    {
        private readonly Examen_jose_lira_Context _BD;

        public ClientesController(Examen_jose_lira_Context context)
        {
            _BD = context;
        }

        //Cargar clientes al ver la página.
        public async Task<IActionResult> Index()
        {
            var listaDto = await _BD.TClientes.Select(x => new ClienteDTO
            {
                Id = x.IdCliente,
                Nombre = x.Nombre,
                Domicilio = x.Domicilio,
                Email = x.Email
            }).ToListAsync();

            return View(new ClientesViewModel
            {
                ListaClientes = listaDto,
                NuevoCliente = new ClienteDTO()
            });
        }

        //Guardar clientes.
        [HttpPost]
        public async Task<IActionResult> Guardar(ClientesViewModel vm)
        {
            using var transaction = await _BD.Database.BeginTransactionAsync();
            try
            {
                //Verificar si el modelo es valido.
                if (!ModelState.IsValid)
                {
                    var listaDto = await _BD.TClientes.Select(x => new ClienteDTO
                    {
                        Id = x.IdCliente,
                        Nombre = x.Nombre,
                        Domicilio = x.Domicilio,
                        Email = x.Email
                    }).ToListAsync();

                    vm.ListaClientes = listaDto;
                    TempData["ErrorMessage"] = "Por favor corrija los errores del formulario.";
                    return View("Index", vm);
                }

                //Verificar si guarda o actualiza.
                var existe = await _BD.TClientes.Where(x => x.IdCliente == vm.NuevoCliente.Id).FirstOrDefaultAsync();
                if (existe is null)
                {
                    //Guardando.
                    _BD.TClientes.Add(new TCliente
                    {
                        Nombre = vm.NuevoCliente.Nombre!,
                        Domicilio = vm.NuevoCliente.Domicilio!,
                        Email = vm.NuevoCliente.Email!
                    });
                    await _BD.SaveChangesAsync();
                    await transaction.CommitAsync();
                    TempData["SuccessMessage"] = "Cliente guardado exitosamente!";
                }
                else
                {
                    existe.Nombre = vm.NuevoCliente.Nombre;
                    existe.Domicilio = vm.NuevoCliente.Domicilio;
                    existe.Email = vm.NuevoCliente.Email;
                    _BD.TClientes.Update(existe);
                    await _BD.SaveChangesAsync();
                    await transaction.CommitAsync();
                    TempData["SuccessMessage"] = "Cliente actualizado exitosamente!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                //Si ocurre algún error.
                await transaction.RollbackAsync();
                Console.WriteLine("Error al procesar la petición: " + ex.ToString());
                TempData["ErrorMessage"] = "Ocurrió un error.";

                var listaDto = await _BD.TClientes.Select(x => new ClienteDTO
                {
                    Id = x.IdCliente,
                    Nombre = x.Nombre,
                    Domicilio = x.Domicilio,
                    Email = x.Email
                }).ToListAsync();

                return View("Index", new ClientesViewModel
                {
                    ListaClientes = listaDto,
                    NuevoCliente = vm.NuevoCliente
                });
            }
        }
    }
}
