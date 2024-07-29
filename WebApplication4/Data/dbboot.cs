using Microsoft.EntityFrameworkCore;
using WebApplication4.Models;

namespace WebApplication4.Data
{
    public class dbboot : DbContext
    {
        public dbboot(DbContextOptions<dbboot> options)
            : base(options)
        {
        }

        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Historico> Historicos { get; set; }
        public DbSet<Operador> Operadores { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<RegistroDeToken> RegistrosDeToken { get; set; }
        public DbSet<Acarreo> Acarreos { get; set; }
        public DbSet<Ruta> Rutas { get; set; }
        public DbSet<Compañia> Compañias { get; set; }
        public DbSet<Material> Materiales { get; set; }
        public DbSet<ProgramacionDeRegistro> ProgramacionesDeRegistro { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vehiculo>()
                .Property(v => v.Capacidad)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Ruta>()
                .Property(r => r.Distancia)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Acarreo>()
                .Property(a => a.Toneladas)
                .HasColumnType("decimal(18,2)");

            // Relaciones existentes
            modelBuilder.Entity<Vehiculo>()
                .HasOne(v => v.Compañia)
                .WithMany()
                .HasForeignKey(v => v.CompañiaID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Operador>()
                .HasOne(o => o.Turno)
                .WithMany()
                .HasForeignKey(o => o.TurnoID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Operador>()
                .HasOne(o => o.Compañia)
                .WithMany()
                .HasForeignKey(o => o.CompañiaID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Acarreo>()
                .HasOne(a => a.Vehiculo)
                .WithMany()
                .HasForeignKey(a => a.VehiculoID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Acarreo>()
                .HasOne(a => a.Operador)
                .WithMany()
                .HasForeignKey(a => a.OperadorID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Acarreo>()
                .HasOne(a => a.Ruta)
                .WithMany()
                .HasForeignKey(a => a.RutaID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ruta>()
                .HasOne(r => r.Material)
                .WithMany()
                .HasForeignKey(r => r.MaterialID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Acarreo>()
                .HasOne(a => a.Material)
                .WithMany()
                .HasForeignKey(a => a.MaterialID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProgramacionDeRegistro>()
                .HasOne(p => p.Acarreo)
                .WithMany()
                .HasForeignKey(p => p.AcarreoID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProgramacionDeRegistro>()
                .HasOne(p => p.RegistroDeToken)
                .WithMany()
                .HasForeignKey(p => p.UserID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
