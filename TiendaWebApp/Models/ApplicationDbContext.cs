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
            modelBuilder.Entity<Categoria>()
                 .HasMany(c => c.Productos)
                 .WithOne(p => p.Categoria)
                 .HasForeignKey(p => p.CategoriaID);


            modelBuilder.Entity<PedidoDetalle>()
                .ToTable("PedidoDetalle"); // Nombre correcto de la tabla

            modelBuilder.Entity<PedidoDetalle>()
                .HasOne(pd => pd.Pedido)
                .WithMany(p => p.Detalles)
                .HasForeignKey(pd => pd.PedidoID);


            modelBuilder.Entity<Rol>().HasKey(r => r.RolID);
            modelBuilder.Entity<Usuario>().HasKey(u => u.UsuarioID);
            modelBuilder.Entity<Cliente>().HasKey(c => c.ClienteID);

            modelBuilder.Entity<Producto>().HasKey(p => p.ProductoID);

            modelBuilder.Entity<PedidoDetalle>().HasKey(p => p.PedidoDetalleID);

            base.OnModelCreating(modelBuilder);
        }
    }
}
