using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace TiendaWebApp.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoDetalle> PedidoDetalles { get; set; }
        public DbSet<Rol> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Categoria>().HasKey(ca => ca.CategoriaID);
            modelBuilder.Entity<Categoria>()
             .HasMany(c => c.Productos)
             .WithOne(p => p.Categoria)
             .HasForeignKey(p => p.CategoriaID);


            modelBuilder.Entity<Producto>().HasKey(p => p.ProductoID);
            modelBuilder.Entity<Usuario>().HasKey(u => u.UsuarioID);
            modelBuilder.Entity<Cliente>().HasKey(c => c.ClienteID);
            modelBuilder.Entity<Pedido>().HasKey(pe => pe.PedidoID);
            modelBuilder.Entity<PedidoDetalle>().HasKey(pd => pd.PedidoDetalleID);
            modelBuilder.Entity<Rol>().HasKey(r => r.RolID);

            base.OnModelCreating(modelBuilder);
        }
    }
}
