using System;
using System.ComponentModel.DataAnnotations;
using WebApplication4.Models;

namespace WebApplication4.Models
{
    // Clase que representa un hist�rico de acarreo
    public class Historic
    {
        // Propiedad que act�a como la clave primaria de la entidad Historic
        [Key]
        public int HistoricId { get; set; }

        // Identificador del registro del token asociado con el hist�rico
        public int TokenRegistryId { get; set; }

        // Identificador del veh�culo asociado con el hist�rico, debe coincidir con el EconomicNumber del Vehicle
        public string vehicle { get; set; }

        // Nombre completo del empleado asociado con el hist�rico, debe coincidir con el FullName del Employee
        public string FullName { get; set; }

        // Nombre del punto de carga
        public string loadPointName { get; set; }

        // Nombre del punto de descarga
        public string unLoadPointName { get; set; }

        // Peso transportado durante el hist�rico
        public decimal Weight { get; set; }

        // Identificador del tipo de material asociado con el hist�rico
        public int materialTypeId { get; set; }

        // Fecha del acarreo representada como una cadena
        public string Dateofcarries { get; set; }

        // Identificador del turno de trabajo asociado con el hist�rico
        public int WorkShiftId { get; set; }

        // Propiedad de navegaci�n que representa la relaci�n con la entidad TokenRegistry
        public virtual TokenRegistry TokenRegistry { get; set; }

        // Propiedad de navegaci�n que representa la relaci�n con la entidad Vehicle
        public virtual Vehicle VehicleNavigation { get; set; }

        // Propiedad de navegaci�n que representa la relaci�n con la entidad Employee
        public virtual Employee Employee { get; set; }

        // Propiedad de navegaci�n que representa la relaci�n con la entidad Route
        public virtual Route HaulagePath { get; set; }

        // Propiedad de navegaci�n que representa la relaci�n con la entidad Material
        public virtual Material MaterialType { get; set; }

        // Propiedad de navegaci�n que representa la relaci�n con la entidad Shift
        public virtual Shift WorkShift { get; set; }
    }
}
