using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TiendaWebApp.Models;

namespace TiendaWebApp.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ILogger<ProductosController> _logger;
        private readonly ApplicationDbContext _context;

        public ProductosController(ILogger<ProductosController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        } 

        public IActionResult Index(string searchTerm)
        {
            // Obtener categorías desde la base de datos
            var categorias = _context.Categorias
                .Include(c => c.Productos)  
                .ToList();



            // Filtrar productos si se proporciona un término de búsqueda
            if (!string.IsNullOrEmpty(searchTerm))
            {
                foreach (var categoria in categorias)
                {
                    categoria.Productos = categoria.Productos
                        .Where(p => p.Nombre.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                    p.Descripcion.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }

                // Eliminar categorías sin productos después del filtro
                categorias = categorias.Where(c => c.Productos.Any()).ToList();
            }

            // Pasar el término de búsqueda a la vista
            ViewData["SearchTerm"] = searchTerm;

            return View(categorias);
        }

        public IActionResult DetalleProducto(int id)
        {
            // Buscar el producto por ID
            var producto = _context.Productos.FirstOrDefault(p => p.ProductoID == id);

            if (producto == null)
            {
                return NotFound("Producto no encontrado.");
            }

            return View(producto); // Pasar el producto a la vista
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
