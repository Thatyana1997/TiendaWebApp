namespace TiendaWebApp.Models
{
    public class Producto
    {
        public int ProductoID { get; set; } // Llave primaria
        public string Nombre { get; set; } // Nombre del producto
        public string Descripcion { get; set; } // Descripción del producto
        public decimal Precio { get; set; } // Precio del producto
        public int Stock { get; set; } // Cantidad en stock

        // Llave foránea hacia Categorias
        public int CategoriaID { get; set; }
        public Categoria Categoria { get; set; } // Propiedad de navegación

        // Metadatos de auditoría
        public DateTime FechaAdicion { get; set; } = DateTime.Now;
        public string AdicionadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? ModificadoPor { get; set; }
    }
}
