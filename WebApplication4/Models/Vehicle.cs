using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class Vehicle
    {
        [Key]
        public int VehicleId { get; set; }
        public string Plates { get; set; }
        public string EconomicNumber { get; set; }
        public int CompanyId { get; set; }
        public string Model { get; set; }
        public decimal EmptyWeight { get; set; }
        public decimal FuelTankCapacity { get; set; }
        public decimal Weight { get; set; }
        public int VehicleTypeId { get; set; }

        public Company Company { get; set; }
        public ICollection<Historic> Historics { get; set; }
        public ICollection<Haulage> Haulages { get; set; }
    }
}