using System.ComponentModel.DataAnnotations;

namespace CompanyDB.Models
{
    public class CompanyModel
    {
        public int CompanyID { get; set; }

        [StringLength(150)]
        [Required(ErrorMessage ="Please enter the Company name")]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [StringLength(150)]
        [Display(Name = "Email")]
        public string? CompanyEmail { get; set; }

        [Display(Name = "Logo")]
        public string? CompanyLogo { get; set; }
        public IFormFile? LogoImg { get; set; }

        [StringLength(200)]
        [Display(Name ="Website")]
        public string? CompanyWebsite { get; set; }
    }
}
