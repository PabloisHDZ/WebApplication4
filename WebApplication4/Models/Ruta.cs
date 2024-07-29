using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class Ruta
    {
        [Key]
        public int RutaID { get; set; }
        public string Sito_De_Carga { get; set; }
        public string Sito_De_Descarga { get; set; }
        public decimal Distancia { get; set; }
        public TimeOnly Tiempo_De_Ciclo { get; set; }
        public string Tipo_De_Material { get; set; }
        public int MaterialID { get; set; }
        public Material Material { get; set; }
    }
}
