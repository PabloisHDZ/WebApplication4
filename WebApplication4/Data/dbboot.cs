using Microsoft.EntityFrameworkCore;
using WebApplication4.Models;

namespace WebApplication4.Data
{
    using Microsoft.EntityFrameworkCore;

    public class dbboot : DbContext
    {
        public dbboot(DbContextOptions<dbboot> options) : base(options)
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
        public DbSet<Sitio> Sitios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relaciones existentes
            modelBuilder.Entity<Vehiculo>()
                .HasOne(v => v.Compañia)
                .WithMany()
                .HasForeignKey(v => v.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Operador>()
                .HasOne(o => o.Compañia)
                .WithMany()
                .HasForeignKey(o => o.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Acarreo>()
                .HasOne(a => a.Vehiculo)
                .WithMany()
                .HasForeignKey(a => a.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Acarreo>()
                .HasOne(a => a.Operador)
                .WithMany()
                .HasForeignKey(a => a.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Acarreo>()
                .HasOne(a => a.Ruta)
                .WithMany()
                .HasForeignKey(a => a.HaulageSiteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Acarreo>()
                .HasOne(a => a.Material)
                .WithMany()
                .HasForeignKey(a => a.MaterialTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProgramacionDeRegistro>()
                .HasOne(p => p.Acarreo)
                .WithMany(a => a.ProgramacionesDeRegistro)
                .HasForeignKey(p => p.CarryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProgramacionDeRegistro>()
                .HasOne(p => p.RegistroDeToken)
                .WithMany()
                .HasForeignKey(p => p.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
