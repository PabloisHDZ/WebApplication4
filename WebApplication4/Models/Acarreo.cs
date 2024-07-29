using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class Acarreo
    {
        [Key]
        public int AcarreoID { get; set; }
        public int VehiculoID { get; set; }
        public int OperadorID { get; set; }
        public int RutaID { get; set; }
        public decimal Toneladas { get; set; }
        public string Comentarios { get; set; }
        public int MaterialID { get; set; } 
        public Vehiculo Vehiculo { get; set; }
        public Operador Operador { get; set; }
        public Ruta Ruta { get; set; }
        public Material Material { get; set; }
    }
}
