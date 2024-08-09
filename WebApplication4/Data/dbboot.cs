using Microsoft.EntityFrameworkCore;
using WebApplication4.Models;

namespace WebApplication4.Data
{
    public class dbboot : DbContext
    {
        public dbboot(DbContextOptions<dbboot> options) : base(options)
        {
        }

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de relaciones
            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.Company)
                .WithMany(c => c.Vehicles)
                .HasForeignKey(v => v.CompanyId);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Company)
                .WithMany(c => c.Employees)
                .HasForeignKey(e => e.CompanyId);

            modelBuilder.Entity<Historic>()
                .HasOne(h => h.TokenRegistry)
                .WithMany(t => t.Historics)
                .HasForeignKey(h => h.TokenRegistryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Historic>()
                .HasOne(h => h.Vehicle)
                .WithMany(v => v.Historics)
                .HasForeignKey(h => h.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Historic>()
                .HasOne(h => h.LoadPoint)
                .WithMany(r => r.loadHistorics)
                .HasForeignKey(h => h.loadPointId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Historic>()
                .HasOne(h => h.UnloadPoint)
                .WithMany(r => r.unloadHistorics)
                .HasForeignKey(h => h.unLoadPointId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Historic>()
                .HasOne(h => h.UnloadPoint)
                .WithMany(r => r.unloadHistorics)
                .HasForeignKey(h => h.unLoadPointId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Historic>()
                .HasOne(h => h.Material)
                .WithMany(m => m.Historics)
                .HasForeignKey(h => h.materialTypeId);

            modelBuilder.Entity<Historic>()
                .HasOne(h => h.Shift)
                .WithMany(s => s.Historics)
                .HasForeignKey(h => h.WorkShiftId);

            modelBuilder.Entity<ProgrammingRecord>()
                .HasOne(p => p.Haulage)
                .WithMany(h => h.ProgrammingRecords)
                .HasForeignKey(p => p.HaulageId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProgrammingRecord>()
                .HasOne(p => p.Employee)
                .WithMany(e => e.ProgrammingRecords)
                .HasForeignKey(p => p.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Haulage>()
                .HasOne(h => h.Vehicle)
                .WithMany(v => v.Haulages)
                .HasForeignKey(h => h.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Haulage>()
                .HasOne(h => h.Employee)
                .WithMany(e => e.Haulages)
                .HasForeignKey(h => h.EmployeeId);

            modelBuilder.Entity<Haulage>()
                .HasOne(h => h.Route)
                .WithMany(r => r.Haulages)
                .HasForeignKey(h => h.haulagePathId);

            modelBuilder.Entity<Haulage>()
                .HasOne(h => h.Material)
                .WithMany(m => m.Haulages)
                .HasForeignKey(h => h.materialTypeId);
        }
    }
}
