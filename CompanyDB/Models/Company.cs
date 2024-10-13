using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyDB.Models
{
    public class Company
    {
        public int CompanyID { get; set; }

        [StringLength(100)]
        public required string CompanyName { get; set; }
        [StringLength(100)]
        public string? CompanyEmail { get; set; }


        [DisplayName("Logo")]        
        public string? CompanyLogo { get; set; }
        [NotMapped]
        public IFormFile? LogoImg { get; set; }


        [StringLength(100)]
        public string? CompanyWebsite { get; set; }

        public virtual List<Employee> Employees { get; set; } = new List<Employee>();

    }
}
