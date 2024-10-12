using System.ComponentModel.DataAnnotations;

namespace CompanyDB.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public int CompanyID { get; set; }
        public virtual Company Company { get; private set; }

        [StringLength(50)]
        public required string FirstName { get; set; }
        [StringLength(50)]
        public required string LastName { get; set; }
      
        [StringLength(100)]
        public string? Email { get; set; }

        [StringLength(15)]
        public string? Phone { get; set; }

    }
}
