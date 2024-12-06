using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TiendaWebApp.Models;

namespace TiendaWebApp.Controllers
{
    public class CarritoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarritoController(ApplicationDbContext context)
        {
            _context = context;
        } 
        public IActionResult AgregarAlCarrito(int idProducto, int cantidad = 1)
        {
            // Obtener el producto de la base de datos
            var producto = _context.Productos.Find(idProducto);
            if (producto == null)
            {
                return NotFound(); // Producto no encontrado
            }

            // Obtener el carrito de la sesión
            var carritoJson = HttpContext.Session.GetString("Carrito");
            var carrito = carritoJson != null
                ? JsonConvert.DeserializeObject<List<PedidoDetalle>>(carritoJson)
                : new List<PedidoDetalle>();

            // Verificar si el producto ya está en el carrito
            var detalleExistente = carrito.FirstOrDefault(d => d.ProductoID == idProducto);
            if (detalleExistente != null)
            {
                // Incrementar la cantidad
                detalleExistente.Cantidad += cantidad;
            }
            else
            {
                // Crear un nuevo detalle del pedido
                var detalle = new PedidoDetalle
                {
                    ProductoID = idProducto,
                    Cantidad = cantidad,
                    PrecioUnitario = producto.Precio,
                    FechaAdicion = DateTime.Now,
                    AdicionadoPor = User.Identity?.Name ?? "Anonimo"
                };
                carrito.Add(detalle);
            }

            // Guardar el carrito en la sesión
            HttpContext.Session.SetString("Carrito", JsonConvert.SerializeObject(carrito));

            return RedirectToAction("VerCarrito");
        }
        // Acción para mostrar el carrito
        public IActionResult VerCarrito()
        {
            var carritoJson = HttpContext.Session.GetString("Carrito");
            var carrito = carritoJson != null
                ? JsonConvert.DeserializeObject<List<PedidoDetalle>>(carritoJson)
                : new List<PedidoDetalle>();

            // Cargar los datos del producto para cada detalle
            foreach (var item in carrito)
            {
                item.Producto = _context.Productos.FirstOrDefault(p => p.ProductoID == item.ProductoID);
            }

            return View(carrito);
        }

        // Acción para confirmar el pedido
        public IActionResult ConfirmarPedido()
        {
            var carritoJson = HttpContext.Session.GetString("Carrito");
            var carrito = carritoJson != null
                ? JsonConvert.DeserializeObject<List<PedidoDetalle>>(carritoJson)
                : null;

            if (carrito == null || !carrito.Any())
            {
                return RedirectToAction("VerCarrito");
            }

            // Crear el pedido
            var pedido = new Pedido
            {
                ClienteID = 5/* Obtener el ID del cliente logueado */,
                Estado = "Pendiente",
                PrecioTotal = carrito.Sum(c => c.Cantidad * c.PrecioUnitario),
                FechaAdicion = DateTime.Now,
                AdicionadoPor = User.Identity?.Name ?? "Anonimo"
            };

            _context.Pedidos.Add(pedido);
            _context.SaveChanges();

            // Asociar los detalles del pedido
            foreach (var item in carrito)
            {
                item.PedidoID = pedido.PedidoID;
                _context.PedidoDetalles.Add(item);
            }

            _context.SaveChanges();

            // Limpiar el carrito
            HttpContext.Session.Remove("Carrito");

            return RedirectToAction("Index", "Home");
        }

        // Acción para eliminar un producto del carrito
        public IActionResult EliminarDelCarrito(int idProducto)
        {
            var carritoJson = HttpContext.Session.GetString("Carrito");
            var carrito = carritoJson != null
                ? JsonConvert.DeserializeObject<List<PedidoDetalle>>(carritoJson)
                : new List<PedidoDetalle>();

            var item = carrito.FirstOrDefault(d => d.ProductoID == idProducto);
            if (item != null)
            {
                carrito.Remove(item);
            }

            // Actualizar el carrito en la sesión
            HttpContext.Session.SetString("Carrito", JsonConvert.SerializeObject(carrito));

            return RedirectToAction("VerCarrito");
        }
    }
}
