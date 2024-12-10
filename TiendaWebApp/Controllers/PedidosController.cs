using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaWebApp.Models;

namespace TiendaWebApp.Controllers
{
    public class PedidosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PedidosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult MisPedidos()
        {
            // Obtener el ClienteID del usuario actual
            // (Asume que tienes un sistema para asociar el usuario autenticado con un cliente).
            int clienteId = ObtenerClienteIDActual(); // Método que debes implementar.

            // Obtener los pedidos del cliente actual, incluyendo sus detalles y productos relacionados.
            var pedidos = _context.Pedidos
                .Where(p => p.ClienteID == clienteId)
                .Include(p => p.Detalles) // Incluir los detalles del pedido
                    .ThenInclude(d => d.Producto) // Incluir la información de los productos en los detalles
                .ToList();

            return View(pedidos);
        }

        private int ObtenerClienteIDActual()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioID");
            var cliente = _context.Clientes.FirstOrDefault(c => c.UsuarioID == usuarioId);

            if (cliente == null)
            {
                throw new Exception("No se encontró un cliente asociado al usuario actual.");
            }

            return cliente.ClienteID;
        }
    }
}
