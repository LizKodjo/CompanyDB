using CompanyDB.Data;
using CompanyDB.Models;
using CompanyDB.Repository;
using CompanyDB.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyDB.Controllers
{
    public class CompanyController : Controller
    {
        
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHost;
        private readonly CompanyRepository _companyRepository = null;
        public CompanyController(AppDbContext context, IWebHostEnvironment webHost, CompanyRepository companyRepository)
        {
            _context = context;
            _webHost = webHost;
            _companyRepository = companyRepository;
        }

        public ViewResult GetCompanies()
        {
            var companies = _companyRepository.GetCompanies();
            return View(companies);
        }

        public ViewResult GetEmployees()
        {
            var employees = _companyRepository.GetEmployees();
            return View(employees);
            
        }

        public ViewResult GetCompany(int id)
        {
            var company = _companyRepository.GetCompanyById(id);
            return View(company);
        }

        public List<CompanyViewModel> SearchCompany(string name)
        {
            return _companyRepository.SearchCompany(name);
        }

        public ViewResult AddCompany()
        {
            return View();
        }

        [HttpPost]
        public ViewResult AddCompany(CompanyViewModel company)
        {
            int id = _companyRepository.AddCompany(company);
            return View(company);
        }

















        //public async  Task<IActionResult> List()
        //{
        //    List<CompanyModel> companies;
        //    companies = await _context.Companies.ToListAsync();
        //    return View(companies);
        //}

        #region -- Create Company --

        //[HttpGet]
        //public IActionResult Create()
        //{
        //    CompanyModel company = new CompanyModel() { CompanyName = "" };
        //    company.Employees.Add(new EmployeeModel() { FirstName = "", LastName = "" });              
        
        //    return View(company);
        //}

        [HttpPost]
        public IActionResult Create(CompanyViewModel company)
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
        //public IActionResult Details(int id)
        //{
        //    CompanyModel company = _context.Companies
        //        .Include(e => e.Employees)
        //        .Where(c => c.CompanyID == id)
        //        .FirstOrDefault();
        //    return View(company);
        //}
        #endregion


        #region -- Delete --
        [HttpGet]
        //public IActionResult Delete(int id)
        //{
        //    CompanyModel company = _context.Companies
        //        .Include(e => e.Employees)
        //        .Where(c => c.CompanyID == id).FirstOrDefault();
        //    return View(company);
        //}

        [HttpPost]
        public IActionResult Delete(CompanyViewModel company)
        {
            _context.Attach(company);
            _context.Entry(company).State = EntityState.Deleted;
            _context.SaveChanges();
            return RedirectToAction("List");
        }

        #endregion

        #region  -- Edit --

        //[HttpGet]
        //public IActionResult Edit(int id)
        //{
        //    CompanyModel company = _context.Companies
        //        .Include(e => e.Employees)
        //        .Where(c => c.CompanyID == id)
        //        .FirstOrDefault();
        //    return View(company);
        //}

        //[HttpPost]
        //public IActionResult Edit(CompanyModel company)
        //{
        //    List<EmployeeModel> employees = _context.Employees
        //        .Where(c => c.CompanyID==company.CompanyID).ToList();
        //    _context.Employees.RemoveRange(employees);
        //    _context.SaveChanges();
            

        //    company.Employees.RemoveAll(e => e.IsDeleted == true);

        //    //if(company.CompanyLogo != null)
        //    //{
        //    //    //string uniqueFileName = GetLogo(company);
        //    //    //company.LogoImg = uniqueFileName;

        //    //}

        //    _context.Attach(company);
        //    _context.Entry(company).State = EntityState.Modified;
        //    _context.Employees.AddRange(company.Employees);
        //    _context.SaveChanges();
        //    return RedirectToAction("List");
        //}



        #endregion

    }
}
