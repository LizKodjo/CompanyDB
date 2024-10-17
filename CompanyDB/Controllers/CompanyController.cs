using CompanyDB.Data;
using CompanyDB.Models;
using CompanyDB.Repository;
using CompanyDB.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace CompanyDB.Controllers
{
    public class CompanyController : Controller
    {
        
        private readonly AppDbContext _context;

        //private readonly EmployeeRepository _employeeRepository;
        private readonly CompanyRepository _companyRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
   
        public CompanyController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
           
            //_employeeRepository = employeeRepository;
        }

        public async Task<ViewResult> GetCompanies()
        {
            var companies = await _companyRepository.GetCompanies();
            return View(companies);
        }

        public ViewResult GetEmployees()
        {
            var employees = _companyRepository.GetEmployees();
            return View(employees);
            
        }

        [Route("company-details/{id}", Name ="companyDetailsRoute")]
        public async Task<ViewResult> GetCompany(int id)
        {
            var company = await _companyRepository.GetCompanyById(id);
            return View(company);
        }

        public List<CompanyViewModel> SearchCompany(string name)
        {
            return _companyRepository.SearchCompany(name);
        }

        public ViewResult AddCompany(bool isSuccess = false, int companyId = 0)
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
            return View();
        }
    }
}
