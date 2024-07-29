using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class Material
    {
        [Key]
        public int MaterialID { get; set; }
        public string Descripcion { get; set; }
    }
}

