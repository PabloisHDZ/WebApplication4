using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class Material
    {
        [Key]
        public int MaterialTypeId { get; set; }
        public string Name { get; set; }
    }
}

