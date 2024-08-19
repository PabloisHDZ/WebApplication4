using System;
using System.ComponentModel.DataAnnotations;
using WebApplication4.Models;

namespace WebApplication4.Models
{
    // Clase que representa un registro de programaci�n de acarreo
    public class ProgrammingRecord
    {
        // Propiedad que act�a como la clave primaria de la entidad ProgrammingRecord
        [Key]
        public int ProgrammingRecordId { get; set; }

        // Identificador del acarreo asociado con el registro de programaci�n
        public int HaulageID { get; set; }

        // Identificador del empleado asociado con el registro de programaci�n
        public int EmployeeId { get; set; }

        // Tiempo de acarreo representado como un TimeSpan
        public TimeSpan Dateofcarries { get; set; }

        // Propiedad de navegaci�n que representa la relaci�n con la entidad Haulage
        public virtual Haulage Haulage { get; set; }

        // Propiedad de navegaci�n que representa la relaci�n con la entidad Employee
        public virtual Employee Employee { get; set; }
    }
}

