﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using CompanyDB.ViewModel;

namespace CompanyDB.Data.Entity
{
    public class Companies
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

        public ICollection<Employees> Employees { get; set; }

        //public virtual List<EmployeeViewModel> Employees { get; set; } = new List<EmployeeViewModel>();
    }
}
