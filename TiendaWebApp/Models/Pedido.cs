namespace TiendaWebApp.Models
{
    public class Pedido
    {
        public int PedidoID { get; set; }
        public int ClienteID { get; set; }
        public DateTime FechaPedido { get; set; } = DateTime.Now;
        public string Estado { get; set; }
        public decimal PrecioTotal { get; set; }
        public DateTime FechaAdicion { get; set; } = DateTime.Now;
        public string AdicionadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? ModificadoPor { get; set; }

        public Cliente Cliente { get; set; }
    }
}
