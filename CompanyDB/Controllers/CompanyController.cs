using CompanyDB.Data;
using CompanyDB.Models;
using CompanyDB.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyDB.Controllers
{
    public class CompanyController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHost;
        private readonly CompanyRepository _companyRepository;
        public CompanyController(AppDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
            _companyRepository = new CompanyRepository();
        }

        public ViewResult GetCompanies()
        {
            var data = _companyRepository.GetCompanies();
            return View(data);
        }

        public async  Task<IActionResult> List()
        {
            List<Company> companies;
            companies = await _context.Companies.ToListAsync();
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
            company.Employees.RemoveAll(c => c.IsDeleted == true);

            if (company.LogoImg != null)
            {
                string logoFolder = "images/logo/";
                logoFolder += Guid.NewGuid().ToString() + company.LogoImg.FileName;
                company.CompanyLogo = logoFolder;

                string serverFolder = Path.Combine(_webHost.WebRootPath, logoFolder);



                company.LogoImg.CopyTo(new FileStream(serverFolder, FileMode.Create));
            }

            //IFormFile uniqueFileName = GetUploadedLogo(company);
            //company.LogoImg = uniqueFileName;
                
            _context.Add(company);
            _context.SaveChanges();
            return RedirectToAction("List");
        }

        #endregion

        //private IFormFile GetUploadedLogo(Company company)
        //{
        //    IFormFile uniqueFileName = null;

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
                .Include(e => e.Employees)
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

        #region  -- Edit --

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Company company = _context.Companies
                .Include(e => e.Employees)
                .Where(c => c.CompanyID == id)
                .FirstOrDefault();
            return View(company);
        }

        [HttpPost]
        public IActionResult Edit(Company company)
        {
            List<Employee> employees = _context.Employees
                .Where(c => c.CompanyID==company.CompanyID).ToList();
            _context.Employees.RemoveRange(employees);
            _context.SaveChanges();
            

            company.Employees.RemoveAll(e => e.IsDeleted == true);

            //if(company.CompanyLogo != null)
            //{
            //    //string uniqueFileName = GetLogo(company);
            //    //company.LogoImg = uniqueFileName;

            //}

            _context.Attach(company);
            _context.Entry(company).State = EntityState.Modified;
            _context.Employees.AddRange(company.Employees);
            _context.SaveChanges();
            return RedirectToAction("List");
        }



        #endregion

    }
}
