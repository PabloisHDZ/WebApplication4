using System;
using System.ComponentModel.DataAnnotations;
using WebApplication4.Models;

namespace WebApplication4.Models
{
    // Clase que representa un acarreo
    public class Haulage
    {
        // Propiedad que act�a como la clave primaria de la entidad Haulage
        [Key]
        public int HaulageID { get; set; }

        // Identificador del veh�culo, debe coincidir con el EconomicNumber del Vehicle
        public int VehicleId { get; set; }

        // Identificador del empleado, debe coincidir con el FullName del Employee
        public int EmployeeId { get; set; }

        // Identificador del recorrido del acarreo
        public int PathId { get; set; }

        // Peso transportado en el acarreo
        public decimal Weight { get; set; }

        // Comentarios adicionales sobre el acarreo
        public string Comments { get; set; }

        // Fecha del acarreo en formato de cadena
        public string Dateofcarries { get; set; }

        // Identificador opcional del tipo de material
        public int? materialTypeId { get; set; }

        // Identificador opcional del punto de carga
        public int? LoadPointId { get; set; }

        // Identificador opcional del punto de descarga
        public int? UnloadPointId { get; set; }

        // Identificador opcional del turno de trabajo
        public int? ShiftId { get; set; }

        // Nombre opcional del punto de carga
        public string? LoadPointName { get; set; }

        // Nombre opcional del punto de descarga
        public string? UnloadPointName { get; set; }

        // Tipo de ley aplicable al acarreo
        public string? LawType { get; set; }

        // Kil�metros recorridos durante el acarreo
        public decimal? Kilometers { get; set; }

        // Tipo de material transportado
        public string MaterialType { get; set; }

        // Propiedad de navegaci�n que representa la relaci�n con la entidad Vehicle
        public virtual Vehicle VehicleNavigation { get; set; }

        // Propiedad de navegaci�n que representa la relaci�n con la entidad Employee
        public virtual Employee Employee { get; set; }

        // Propiedad de navegaci�n que representa la relaci�n con la entidad Route
        public virtual Route HaulagePath { get; set; }

        // Propiedad de navegaci�n que representa la relaci�n con la entidad Material
        public virtual Material MaterialTypee { get; set; }
    }
}
