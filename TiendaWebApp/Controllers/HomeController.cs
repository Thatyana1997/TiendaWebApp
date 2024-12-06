using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TiendaWebApp.Models;

namespace TiendaWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        } 

        public IActionResult Index()
        {
            // Obtener categorías desde la base de datos
            var categorias = _context.Categorias
                .Include(c => c.Productos)  
                .ToList();

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
