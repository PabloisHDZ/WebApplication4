using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models.ViewModels
{
    public class HisError
    {
        [ Key]
        public int HistoricId { get; set; }
        public int TokenRegistryId { get; set; }
        public int VehicleId { get; set; }
        public int EmployeeId { get; set; }
        public int loadPointId { get; set; }
        public int unLoadPointId { get; set; }
        public decimal Weight { get; set; }
        public int materialTypeId { get; set; }
        public string Dateofcarries { get; set; }
        public int WorkShiftId { get; set; }
        public string ErrorMessage { get; set; }
    }
}
