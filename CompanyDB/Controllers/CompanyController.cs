using CompanyDB.Data;
using CompanyDB.Models;
using CompanyDB.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CompanyDB.Controllers
{
    public class CompanyController : Controller
    {
        
        private readonly AppDbContext _context;

        private readonly EmployeeRepository _employeeRepository;
        private readonly CompanyRepository _companyRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
   
        public CompanyController(AppDbContext context,EmployeeRepository employeeRepository, IWebHostEnvironment webHostEnvironment,
            CompanyRepository companyRepository)
        {
            _context = context;
            _employeeRepository = employeeRepository;
            _webHostEnvironment = webHostEnvironment;
            _companyRepository = companyRepository;
           
            
        }

        public async Task<ViewResult> GetCompanies()
        {
            var companies = await _companyRepository.GetCompanies();
            return View(companies);
        }

        [Route("company-details/{id}", Name ="companyDetailsRoute")]
        public async Task<ViewResult> GetCompany(int id)
        {
            var company = await _companyRepository.GetCompanyById(id);
            return View(company);
        }

        public async Task<List<CompanyModel>> SearchCompany(string name)
        {
            return await _companyRepository.SearchCompany(name);
        }

        [HttpGet]
        public async Task<ActionResult> AddCompany(bool isSuccess = false, int companyId = 0)
        {
            //var employees = await _employeeRepository.GetEmployees();

            ViewBag.IsSuccess = isSuccess;
            ViewBag.CompanyId = companyId;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddCompany(CompanyModel company)
        {
            if (ModelState.IsValid)
            {
                if (company.LogoImg != null)
                {
                    string storedLogos = "images/logo/";
                    storedLogos += Guid.NewGuid().ToString() + "_" + company.LogoImg.FileName;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, storedLogos);
                    company.CompanyLogo = storedLogos;
                    await company.LogoImg.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                }

                int id = await _companyRepository.AddCompany(company);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddCompany), new { isSuccess = true, companyId = id });
                }
            }

            //ViewBag.Employees = await _employeeRepository.GetEmployees();
            return View();
        }
    }
}
