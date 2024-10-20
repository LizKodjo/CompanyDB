using CompanyDB.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace CompanyDB.Models
{
    public class EmployeeModel
    {
        public int EmployeeID { get; set; }
        public int CompanyID { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Please enter first name.")]
        [Display(Name = "First name")]
        public required string Firstname { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Please enter last name.")]
        [Display(Name = "Last name")]
        public required string Lastname { get; set; }

        [StringLength(150)]
        public string? Email { get; set; }

        [StringLength(15)]
        public string? Phone { get; set; }

       
    }
}
