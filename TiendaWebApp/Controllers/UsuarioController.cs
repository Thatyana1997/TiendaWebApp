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
            // Excluir campos  de la validación porque se asigna automáticamente despues 
            ModelState.Remove(nameof(Usuario.RolID));
            ModelState.Remove(nameof(Usuario.FechaRegistro));
            ModelState.Remove(nameof(Usuario.FechaAdicion));
            ModelState.Remove(nameof(Usuario.AdicionadoPor));

            if (ModelState.IsValid)
            {
                nuevoUsuario.FechaAdicion = DateTime.Now;
                nuevoUsuario.AdicionadoPor = "AppUser"; //Fue registrado por el app
                nuevoUsuario.FechaRegistro = DateTime.Now;
                nuevoUsuario.RolID = 3; // Cliente 
                _context.Usuarios.Add(nuevoUsuario);
                _context.SaveChanges();

                // Redirigir al inicio de sesión después del registro
                //return RedirectToAction("Login");

                // Volver a la misma vista con el modelo actualizado
                return View(nuevoUsuario);
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
