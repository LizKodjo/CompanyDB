using CompanyDB.ViewModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace CompanyDB.Models
{
    public class CompanyModel
    {               
        public int CompanyID { get; set; }

        [DisplayName("Name")]
        [StringLength(100)]
        public required string CompanyName { get; set; }

        [DisplayName("Email")]
        [StringLength(100)]
        public string? CompanyEmail { get; set; }


        public string? CompanyLogo { get; set; }
        [DisplayName("Logo")]
        [NotMapped]
        public IFormFile LogoImg { get; set; }

        [DisplayName("Website")]
        [StringLength(100)]
        public string? CompanyWebsite { get; set; }

        public virtual List<EmployeeModel> Employees { get; set; } = new List<EmployeeModel>();
    }
}

