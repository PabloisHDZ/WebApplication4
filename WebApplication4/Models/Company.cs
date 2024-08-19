using System;
using System.ComponentModel.DataAnnotations;
using WebApplication4.Models;

namespace WebApplication4.Models
{
    // Clase que representa una empresa
    public class Company
    {
        // Propiedad que actúa como la clave primaria de la entidad Company
        [Key]
        public int CompanyId { get; set; }

        // Nombre de la empresa
        public string Name { get; set; }
    }
}
