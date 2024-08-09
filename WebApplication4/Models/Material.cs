using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class Material
    {
        [Key]
        public int materialTypeId { get; set; }
        public string name { get; set; }

        public ICollection<Historic> Historics { get; set; }
        public ICollection<Haulage> Haulages { get; set; }
    }
}

