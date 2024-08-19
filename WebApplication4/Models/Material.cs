using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    // Clase que representa un tipo de material en la aplicación
    public class Material
    {
        // Propiedad que actúa como la clave primaria de la entidad Material
        [Key]
        public int materialTypeId { get; set; }

        // Nombre del tipo de material
        public string name { get; set; }
    }
}
