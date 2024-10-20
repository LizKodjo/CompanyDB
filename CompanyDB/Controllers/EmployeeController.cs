using CompanyDB.Data;
using CompanyDB.Models;
using CompanyDB.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CompanyDB.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly EmployeeRepository _employeeRepository;
        private readonly CompanyRepository _companyRepository;
        public EmployeeController(AppDbContext context, EmployeeRepository employeeRepository, 
            CompanyRepository companyRepository)
        {
            _context = context;
            _employeeRepository = employeeRepository;
            _companyRepository = companyRepository;
        }
        public ActionResult GetEmployees()
        {
            //var employees = _companyRepository.GetEmployees();
            return View();

        }

        [HttpGet]
        public async Task<ActionResult> AddEmployee(bool isSuccess = false, int employeeId = 0)
        {
            var companies = await _companyRepository.GetCompanies();
            ViewBag.IsSuccess = isSuccess;
            ViewBag.EmployeeId = employeeId;
            return View();  
        }

        [HttpPost]
        public async Task<ActionResult> AddEmployee(EmployeeModel employee)
        {
            if (ModelState.IsValid)
            {
                int id = _employeeRepository.AddEmployee(employee);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddEmployee), new {isSuccess = true, employeeId = id});
                }
            }
            ViewBag.Companies = await _companyRepository.GetCompanies(); 
            return View();
        }

    }
}
