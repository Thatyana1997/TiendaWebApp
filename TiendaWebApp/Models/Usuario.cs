namespace TiendaWebApp.Models
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public string Email { get; set; }
        public string Contrasena { get; set; }
        public int RolID { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public DateTime FechaAdicion { get; set; } = DateTime.Now;
        public string AdicionadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? ModificadoPor { get; set; }

        public Rol Rol { get; set; }
    }
}
