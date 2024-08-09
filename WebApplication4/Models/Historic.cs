using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class Historic
    {
        [Key]
        public int HistoricId { get; set; }

        public int TokenRegistryId { get; set; }
        public int VehicleId { get; set; } // Agrega esta propiedad
        public Vehicle Vehicle { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        // Cambia los nombres y tipos para que sean compatibles con Route
        public int loadPointId { get; set; }
        public Route LoadPoint { get; set; }

        public int unLoadPointId { get; set; }
        public Route UnloadPoint { get; set; }

        public decimal Weight { get; set; }
        public int materialTypeId { get; set; }
        public string Dateofcarries { get; set; }
        public int WorkShiftId { get; set; }

        public TokenRegistry TokenRegistry { get; set; }
        public Material Material { get; set; }
        public Shift Shift { get; set; }
    }
}