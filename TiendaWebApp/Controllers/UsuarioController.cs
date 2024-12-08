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

            int rolIdCliente = 0;
            // Excluir campos  de la validación porque se asigna automáticamente despues 
            //Usuario
            ModelState.Remove(nameof(Usuario.RolID));
            ModelState.Remove(nameof(Usuario.FechaRegistro));
            ModelState.Remove(nameof(Usuario.FechaAdicion));
            ModelState.Remove(nameof(Usuario.AdicionadoPor));

            //cliente
            ModelState.Remove(nameof(Cliente.UsuarioID));
            ModelState.Remove(nameof(Cliente.FechaAdicion));
            ModelState.Remove(nameof(Cliente.AdicionadoPor));

            if (_context.Usuarios.Any(u => u.Email == nuevoUsuario.Email))
            {
                ModelState.AddModelError("Email", "El correo electrónico ya está registrado.");
            }
            else
            {
                var rolCliente = _context.Roles.FirstOrDefault(r => r.NombreRol == "Cliente");
                if (rolCliente != null)
                {
                     rolIdCliente = rolCliente.RolID; 
                }
            }

            if (ModelState.IsValid)
            {
                nuevoUsuario.FechaAdicion = DateTime.Now;
                nuevoUsuario.AdicionadoPor = "AppUser"; //Fue registrado por el app
                nuevoUsuario.FechaRegistro = DateTime.Now; 
                nuevoUsuario.RolID = rolIdCliente;
                _context.Usuarios.Add(nuevoUsuario);
                var respuesta = _context.SaveChanges();

                if (respuesta > 0)
                {
                    //Registro el cliente tambien 
                    // Obtener los datos adicionales del formulario
                    var nombre = Request.Form["Nombre"];
                    var apellido = Request.Form["Apellido"];
                    var direccion = Request.Form["Direccion"];
                    var telefono = Request.Form["Telefono"];
                    var fechaNacimiento = DateTime.Parse(Request.Form["FechaNacimiento"]);

                    // Crear un cliente relacionado con el usuario
                    var nuevoCliente = new Cliente
                    {
                        UsuarioID = nuevoUsuario.UsuarioID,
                        Nombre = nombre,
                        Apellido = apellido,
                        Direccion = direccion,
                        Telefono = telefono,
                        FechaNacimiento = fechaNacimiento,
                        FechaAdicion = DateTime.Now,
                        AdicionadoPor = "AppUser"
                    };

                    _context.Clientes.Add(nuevoCliente);
                    _context.SaveChanges();

                    // Volver a la misma vista con el modelo actualizado
                    return View(nuevoUsuario);
                }
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
