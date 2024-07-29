using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class ProgramacionDeRegistro
    {
        [Key]
        public int AcarreoID { get; set; }
        public int UserID { get; set; }
        public TimeOnly Hora_de_registro { get; set; }
        public Acarreo Acarreo { get; set; }
        public RegistroDeToken RegistroDeToken { get; set; }
    }
}
