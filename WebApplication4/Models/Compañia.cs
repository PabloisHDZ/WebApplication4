using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class Compa�ia
    {
        [Key]
        public int CompanyId { get; set; }
        public string Name { get; set; }
    }

}
