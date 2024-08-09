using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class Route
    {
        [Key]
        public int haulagePathId { get; set; }
        public string description { get; set; }
        public decimal distance { get; set; }
        public bool isExtraction { get; set; }
        public bool isEnabled { get; set; }
        public int loadPointId { get; set; }
        public string loadPointName { get; set; }
        public TimeSpan timeInHour { get; set; }
        public int unLoadPointId { get; set; }
        public string unloadPointName { get; set; }

        public ICollection<Historic> loadHistorics { get; set; }
        public ICollection<Historic> unloadHistorics { get; set; }
        public ICollection<Haulage> Haulages { get; set; }
    }
}
