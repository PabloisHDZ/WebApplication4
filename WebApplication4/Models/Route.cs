using System;
using System.ComponentModel.DataAnnotations;
using WebApplication4.Models;

namespace WebApplication4.Models
{
    // Clase que representa una ruta de acarreo
    public class Route
    {
        // Propiedad que act�a como la clave primaria de la entidad Route
        [Key]
        public int haulagePathId { get; set; }

        // Descripci�n de la ruta
        public string description { get; set; }

        // Distancia de la ruta en kil�metros (o la unidad que se est� utilizando)
        public decimal distance { get; set; }

        // Indica si la ruta es para extracci�n
        public bool isExtraction { get; set; }

        // Indica si la ruta est� habilitada
        public bool isEnabled { get; set; }

        // Identificador del punto de carga asociado con la ruta
        public int loadPointId { get; set; }

        // Nombre del punto de carga
        public string loadPointName { get; set; }

        // Tiempo estimado en horas para recorrer la ruta
        public TimeSpan timeInHour { get; set; }

        // Identificador del punto de descarga asociado con la ruta
        public int unLoadPointId { get; set; }

        // Nombre del punto de descarga
        public string unLoadPointName { get; set; }
    }
}