using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class Compa�ia
    {
        [Key]
        public int Compa�iaID { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
    }
}
