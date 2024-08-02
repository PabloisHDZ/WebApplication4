using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class Vehiculo
    {
        [Key]
        public int VehicleId { get; set; }
        public string Vehicle { get; set; }
        public string Plates { get; set; }
        public int CompanyId { get; set; }
        public string EconomicNumber { get; set; }
        public string Modelo { get; set; }
        public decimal EmptyWeight { get; set; }
        public decimal FuelTankCapacity { get; set; }
        public decimal Weight { get; set; }
        public int VehicleTypeId { get; set; }

        public Compañia Compañia { get; set; }
    }
}