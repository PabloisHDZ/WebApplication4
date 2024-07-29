using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class RegistroDeToken
    {
        [Key]
        public int UserID { get; set; }
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string refresh_token { get; set; }
    }
}

