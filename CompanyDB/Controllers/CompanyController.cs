using CompanyDB.Data;
using CompanyDB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyDB.Controllers
{
    public class CompanyController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHost;
        public CompanyController(AppDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
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
            Company company = new Company() { CompanyName = "" };
            company.Employees.Add(new Employee() { FirstName = "", LastName = "" });              
        
            return View(company);
        }

        [HttpPost]
        public IActionResult Create(Company company)
        {
            //foreach (Employee employee in company.Employees)
            //{
            //    if (employee.Company.CompanyName == null || employee.Company.CompanyName.Length == 0)
            //    {
            //        company.Employees.Remove(employee);
            //    }
            //}

            //string uniqueFileName = GetUploadedLogo(company);
            //company.LogoImg = uniqueFileName;
                
            _context.Add(company);
            _context.SaveChanges();
            return RedirectToAction("List");
        }

        #endregion

        //private string GetUploadedLogo(Company company)
        //{
        //    string uniqueFileName = null;

        //    if (company.CompanyLogo != null)
        //    {
        //        string uploadsFolder = Path.Combine(_webHost.WebRootPath, "images");
        //        uniqueFileName = Guid.NewGuid().ToString() + "_" + company.CompanyLogo.FileName;
        //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            company.CompanyLogo.CopyTo(fileStream);
        //        }
        //    }
        //    return uniqueFileName;
        //}

        #region -- Company Details --
        public IActionResult Details(int id)
        {
            Company company = _context.Companies
                .Include(c => c.Employees)
                .Where(c => c.CompanyID == id)
                .FirstOrDefault();
            return View(company);
        }
        #endregion


        #region -- Delete --
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Company company = _context.Companies
                .Include(e => e.Employees)
                .Where(c => c.CompanyID == id).FirstOrDefault();
            return View(company);
        }

        [HttpPost]
        public IActionResult Delete(Company company)
        {
            _context.Attach(company);
            _context.Entry(company).State = EntityState.Deleted;
            _context.SaveChanges();
            return RedirectToAction("List");
        }

        #endregion

    }
}
