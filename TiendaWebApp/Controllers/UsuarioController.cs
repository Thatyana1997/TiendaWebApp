using Microsoft.AspNetCore.Mvc;
using System;
using TiendaWebApp.Models;

namespace TiendaWebApp.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsuarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Acción para la página de inicio de sesión
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string contrasena)
        {
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Email == email && u.Contrasena == contrasena);

            if (usuario != null)
            {
                // Establecer una sesión o cookie de usuario
                HttpContext.Session.SetInt32("UsuarioID", usuario.UsuarioID);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Correo o contraseña incorrectos.";
            return View();
        }

        // Acción para el registro de usuarios
        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registro(Usuario nuevoUsuario)
        {
            if (ModelState.IsValid)
            {
                nuevoUsuario.FechaRegistro = DateTime.Now;
                _context.Usuarios.Add(nuevoUsuario);
                _context.SaveChanges();

                // Redirigir al inicio de sesión después del registro
                return RedirectToAction("Login");
            }

            return View(nuevoUsuario);
        }

        // Acción para cerrar sesión
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UsuarioID");
            return RedirectToAction("Login");
        }

    }
}
