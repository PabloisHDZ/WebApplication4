using System;
using System.ComponentModel.DataAnnotations;
using WebApplication4.Models;


namespace WebApplication4.Models
{
    public class Haulage
    {
        [Key]
        public int HaulageId { get; set; }
        public int PathId { get; set; }
        public int VehicleId { get; set; }
        public int EmployeeId { get; set; }
        public int haulagePathId { get; set; }
        public decimal Weight { get; set; }
        public string Comments { get; set; }
        public string Dateofcarries { get; set; }
        public int materialTypeId { get; set; }

        public Vehicle Vehicle { get; set; }
        public Employee Employee { get; set; }
        public Route Route { get; set; }
        public Material Material { get; set; }
        public ICollection<ProgrammingRecord> ProgrammingRecords { get; set; }
    }
}
