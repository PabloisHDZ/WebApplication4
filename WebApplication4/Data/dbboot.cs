using Microsoft.EntityFrameworkCore;
using WebApplication4.Models;

namespace WebApplication4.Data
{
    // DbContext para la aplicación
    public class dbboot : DbContext
    {
        // Constructor que recibe opciones de configuración para el DbContext
        public dbboot(DbContextOptions<dbboot> options) : base(options)
        {
        }

        // DbSet para cada entidad en el modelo de datos
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Historic> Historics { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<ProgrammingRecord> ProgrammingRecords { get; set; }
        public DbSet<TokenRegistry> TokenRegistries { get; set; }
        public DbSet<Haulage> Haulages { get; set; }
        public DbSet<WebApplication4.Models.Route> Routes { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Material> Materials { get; set; }

        // Configuración del modelo de datos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configura la eliminación en cascada para todas las claves foráneas como NoAction
            foreach (var forenkey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                forenkey.DeleteBehavior = DeleteBehavior.NoAction;
            }

            // Comentarios de configuración de relaciones entre entidades
            // Descomenta y ajusta según sea necesario para definir las relaciones

            // Configuración de la relación entre Vehicle y Company
            //modelBuilder.Entity<Vehicle>()
            //    .HasOne(v => v.Company)
            //    .WithMany()
            //    .HasForeignKey(v => v.CompanyId);

            // Configuración de la relación entre Employee y Company
            //modelBuilder.Entity<Employee>()
            //    .HasOne(e => e.Company)
            //    .WithMany()
            //    .HasForeignKey(e => e.CompanyId);

            // Configuración de la relación entre Historic y TokenRegistry
            //modelBuilder.Entity<Historic>()
            //    .HasOne(h => h.TokenRegistry)
            //    .WithMany()
            //    .HasForeignKey(h => h.TokenRegistryId);

            // Configuración de la relación entre Historic y Vehicle
            //modelBuilder.Entity<Historic>()
            //    .HasOne(h => h.VehicleNavigation)
            //    .WithMany()
            //    .HasForeignKey(h => h.VehicleId);

            // Configuración de la relación entre Historic y Employee
            //modelBuilder.Entity<Historic>()
            //    .HasOne(h => h.Employee)
            //    .WithMany()
            //    .HasForeignKey(h => h.EmployeeId);

            // Configuración de la relación entre Historic y LoadPoint
            //modelBuilder.Entity<Historic>()
            //    .HasOne(h => h.LoadPoint)
            //    .WithMany()
            //    .HasForeignKey(h => h.LoadPointId);

            // Configuración de la relación entre Historic y UnloadPoint
            //modelBuilder.Entity<Historic>()
            //    .HasOne(h => h.UnLoadPoint)
            //    .WithMany()
            //    .HasForeignKey(h => h.UnLoadPointId);

            // Configuración de la relación entre Historic y MaterialType
            //modelBuilder.Entity<Historic>()
            //    .HasOne(h => h.MaterialType)
            //    .WithMany()
            //    .HasForeignKey(h => h.MaterialTypeId);

            // Configuración de la relación entre Historic y WorkShift
            //modelBuilder.Entity<Historic>()
            //    .HasOne(h => h.WorkShift)
            //    .WithMany()
            //    .HasForeignKey(h => h.WorkShiftId);

            // Configuración de la relación entre ProgrammingRecord y Haulage
            //modelBuilder.Entity<ProgrammingRecord>()
            //    .HasOne(p => p.Haulage)
            //    .WithMany()
            //    .HasForeignKey(p => p.HaulageID);

            // Configuración de la relación entre ProgrammingRecord y Employee
            //modelBuilder.Entity<ProgrammingRecord>()
            //    .HasOne(p => p.Employee)
            //    .WithMany()
            //    .HasForeignKey(p => p.EmployeeId);

            // Configuración de la relación entre Haulage y Vehicle
            //modelBuilder.Entity<Haulage>()
            //    .HasOne(h => h.VehicleNavigation)
            //    .WithMany()
            //    .HasForeignKey(h => h.VehicleId);

            // Configuración de la relación entre Haulage y Employee
            //modelBuilder.Entity<Haulage>()
            //    .HasOne(h => h.Employee)
            //    .WithMany()
            //    .HasForeignKey(h => h.EmployeeId);

            // Configuración de la relación entre Haulage y Route
            //modelBuilder.Entity<Haulage>()
            //    .HasOne(h => h.HaulagePath)
            //    .WithMany()
            //    .HasForeignKey(h => h.PathId);

            // Configuración de la relación entre Haulage y MaterialType
            //modelBuilder.Entity<Haulage>()
            //    .HasOne(h => h.MaterialType)
            //    .WithMany()
            //    .HasForeignKey(h => h.MaterialTypeId);
        }
    }
}
