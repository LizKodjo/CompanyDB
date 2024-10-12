using System.ComponentModel.DataAnnotations;

namespace CompanyDB.Models
{
    public class Company
    {
        public int CompanyID { get; set; }

        [StringLength(100)]
        public required string CompanyName { get; set; }
        [StringLength(100)]
        public string? CompanyEmail { get; set; }
        public string CompanyLogo { get; set; }
        [StringLength(100)]
        public string? CompanyWebsite { get; set; }

        public virtual List<Employee> Employees { get; set; } = new List<Employee>();

    }
}
