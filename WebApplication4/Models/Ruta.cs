using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class Ruta
    {
        [Key]
        public int HaulagePathId { get; set; }
        public string Description { get; set; }
        public decimal Distance { get; set; }
        public bool IsExtraction { get; set; }
        public bool IsEnabled { get; set; }
        public decimal LoadPointId { get; set; }
        public string LoadPointName { get; set; }
        public TimeOnly TimeInHour { get; set; }
        public decimal UnLoadPointId { get; set; }
        public string UnLoadPointName { get; set; }

        public Material Material { get; set; }
    }
}
