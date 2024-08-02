using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication4.Models
{
    public class Operador
    {
        [Key]
        public int EmployeeId { get; set; }
        public decimal NoEmployee { get; set; }
        public string Name { get; set; }
        public string PaternalLastName { get; set; }
        public string MaternalLastName { get; set; }
        public string FullName { get; set; }
        public string Company { get; set; }
        public int CompanyId { get; set; }

        public Compañia Compañia { get; set; }
    }

}
