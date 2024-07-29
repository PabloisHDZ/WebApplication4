using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class Vehiculo
    {
        [Key]
        public int VehiculoID { get; set; }
        public string Placas { get; set; }
        public string Numero_economico { get; set; }
        public string Modelo { get; set; }
        public decimal Capacidad { get; set; }
        public string Tipo_de_vehiculo { get; set; }    
        public int CompañiaID { get; set; }
        public Compañia Compañia { get; set; }
    }
}