using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; } // Clave primaria

        public decimal NoEmployee { get; set; }
        public string Name { get; set; }
        public string PaternalLastName { get; set; }
        public string MaternalLastName { get; set; }
        public string FullName { get; set; }
        public int CompanyId { get; set; }

        public Company Company { get; set; }

        // Navegación inversa
        public ICollection<Historic> Historics { get; set; }
        public ICollection<ProgrammingRecord> ProgrammingRecords { get; set; }
        public ICollection<Haulage> Haulages { get; set; }
    }
}