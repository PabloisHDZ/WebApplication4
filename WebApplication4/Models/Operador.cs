using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication4.Models
{
    public class Operador
    {
        public int OperadorID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
     
        public string Fullname { get; set; }

        public int TurnoID { get; set; }
        public int Compa�iaID { get; set; }

        public Turno Turno { get; set; } // Propiedad de navegaci�n
        public Compa�ia Compa�ia { get; set; } // Propiedad de navegaci�n
    }

}
