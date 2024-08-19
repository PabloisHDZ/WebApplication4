using System;
using System.ComponentModel.DataAnnotations;
using WebApplication4.Models;

namespace WebApplication4.Models
{
    // Clase que representa un turno de trabajo
    public class Shift
    {
        // Propiedad que act�a como la clave primaria de la entidad Shift
        [Key]
        public int WorkShiftId { get; set; }

        // Identificador del turno (podr�a ser un c�digo o n�mero de turno)
        public int ShiftId { get; set; }

        // Descripci�n del turno
        public string Description { get; set; }

        // Estado de habilitaci�n del turno, indicando si est� activo o no
        public string Enabled { get; set; }

        // Hora de inicio del turno
        public TimeSpan StartTime { get; set; }

        // Hora de finalizaci�n del turno
        public TimeSpan EndTime { get; set; }

        // Tiempo total de operaci�n del turno (duraci�n del turno)
        public TimeSpan OperationTime { get; set; }
    }
}
