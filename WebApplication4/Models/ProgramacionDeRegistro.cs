using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class ProgramacionDeRegistro
    {
        [Key]
        public int CarryId { get; set; }
        public int EmployeeId { get; set; }
        public TimeSpan DateOfCarries { get; set; }

        public RegistroDeToken RegistroDeToken { get; set; }
        public Acarreo Acarreo { get; set; }
    }
}
