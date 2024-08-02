using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class Compañia
    {
        [Key]
        public int CompanyId { get; set; }
        public string Name { get; set; }
    }

}
