using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class Shift
    {
        [Key]
        public int WorkShiftId { get; set; }
        public string Description { get; set; }
        public string Enabled { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public ICollection<Historic> Historics { get; set; }
    }
}
