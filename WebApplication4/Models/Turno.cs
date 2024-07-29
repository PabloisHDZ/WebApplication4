using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class Turno
    {
        [Key]
        public int TurnoID { get; set; }
        public string Nombre { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFin { get; set; }
    }
}
