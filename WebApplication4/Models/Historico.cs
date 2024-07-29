using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class Historico
    {
        [Key]
        public int Id { get; set; }
        // La variable vehiculo es la referencia de numero economico
        public string Vehiculo { get; set; }
        public string Operador { get; set; }
        public string Sitio_de_carga { get; set; }
        public string Sitio_de_descarga { get; set; }
        public decimal Toneladas { get; set; }
        public string Material { get; set; }
        public DateTime Fecha_de_acarreos { get; set; }
        public string Comentarios { get; set; }
    }
}
