using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class TokenRegistry
    {
        [Key]
        public int TokenRegistryId { get; set; }
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string refresh_token { get; set; }

        public ICollection<Historic> Historics { get; set; }
    }
}

