using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class Sitio
    {
        [Key]
        public int PlaceId { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public bool IsProduction { get; set; }
        public decimal PlaceLocation { get; set; }
        public decimal PlaceType { get; set; }
        public decimal PositionX { get; set; }
        public decimal PositionY { get; set; }
        public decimal PositionZ { get; set; }
        public string ReferencePoints { get; set; }
        public string Zone { get; set; }
    }
}
