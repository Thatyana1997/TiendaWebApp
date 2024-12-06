namespace TiendaWebApp.Models
{
    public class Categoria
    {
        public int CategoriaID { get; set; } // Llave primaria
        public string Nombre { get; set; } // Nombre de la categoría
        public string Descripcion { get; set; } // Descripción de la categoría

        // Relación con la tabla Productos
        public ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}
