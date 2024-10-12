using CompanyDB.Data;
using CompanyDB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyDB.Controllers
{
    public class CompanyController : Controller
    {
        private readonly AppDbContext _context;
        public CompanyController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult List()
        {
            List<Company> companies;
            companies = _context.Companies.ToList();
            return View(companies);
        }

        #region -- Create Company --

        [HttpGet]
        public IActionResult Create()
        {
            Company company = new Company() { CompanyName = "Liz Company Ltd"};
            company.Employees.Add(new Employee() { FirstName = "Liz", LastName = "Lizzy" });              
        
            return View(company);
        }

        [HttpPost]
        public IActionResult Create(Company company)
        {
            foreach (Employee employee in company.Employees)
            {
                if (employee.Company.CompanyName == null || employee.Company.CompanyName.Length == 0)
                    company.Employees.Remove(employee);
            }
                
            _context.Add(company);
            _context.SaveChanges();
            return RedirectToAction("List");
        }

        #endregion
    }
}
