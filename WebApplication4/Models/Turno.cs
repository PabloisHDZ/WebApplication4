using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class Turno
    {
        [Key]
        public int WorkShiftId { get; set; }
        public string Description { get; set; }
        public string Enabled { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
