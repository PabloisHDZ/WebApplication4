using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }
        public string Name { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }

}
