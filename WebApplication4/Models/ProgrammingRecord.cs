using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class ProgrammingRecord
    {
        [Key]
        public int ProgrammingRecordId { get; set; }
        public int HaulageId { get; set; }
        public int EmployeeId { get; set; }
        public TimeSpan Dateofcarries { get; set; }

        public Haulage Haulage { get; set; }
        public Employee Employee { get; set; }
    }
}
