using CompanyDB.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace CompanyDB.Data.Entity
{
    public class Employees
    {
        public int EmployeeID { get; set; }
        public int CompanyID { get; set; }
        public virtual CompanyViewModel? Company { get; private set; }

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
