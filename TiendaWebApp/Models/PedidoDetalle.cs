namespace TiendaWebApp.Models
{
    public class PedidoDetalle
    {
        public int PedidoDetalleID { get; set; }
        public int PedidoID { get; set; }
        public int ProductoID { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public DateTime FechaAdicion { get; set; } = DateTime.Now;
        public string AdicionadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? ModificadoPor { get; set; }

        public Pedido Pedido { get; set; }
        public Producto Producto { get; set; }
    }
}
