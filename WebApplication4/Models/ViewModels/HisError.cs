using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models.ViewModels
{
    public class HisError
    {
        [ Key]
        public string Vehicle { get; set; }
        public string FullName { get; set; }
        public string LoadPointName { get; set; }
        public string UnLoadPointName { get; set; }
        public decimal Weight { get; set; }
        public string Name { get; set; }
        public string DateOfCarries { get; set; }
        public string Comments { get; set; }
        public string ErrorMessage { get; set; }
    }
}
