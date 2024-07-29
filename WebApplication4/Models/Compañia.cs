using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class Compañia
    {
        [Key]
        public int CompañiaID { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
    }
}
