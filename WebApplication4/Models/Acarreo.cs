using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class Acarreo
    {
        [Key]
        public int CarryId { get; set; }
        public int VehicleId { get; set; }
        public int EmployeeId { get; set; }
        public int HaulageSiteId { get; set; }
        public decimal Weight { get; set; }
        public string Comments { get; set; }
        public string DateOfCarries { get; set; }
        public int MaterialTypeId { get; set; }

        public Vehiculo Vehiculo { get; set; }
        public Operador Operador { get; set; }
        public Ruta Ruta { get; set; }
        public Material Material { get; set; }
        public ICollection<ProgramacionDeRegistro> ProgramacionesDeRegistro { get; set; }
    }
}
