using CompanyDB.Data;
using CompanyDB.Data.Entity;
using CompanyDB.Models;
using CompanyDB.ViewModel;

namespace CompanyDB.Repository
{
    public class CompanyRepository
    {
        private readonly AppDbContext _context = null;
        public CompanyRepository(AppDbContext contex)
        {
            _context = contex;
        }

        public int AddCompany(CompanyViewModel model)
        {
            var newCompany = new Company()
            { 
                CompanyName = model.CompanyName,
                CompanyEmail = model.CompanyEmail,
                CompanyLogo = model.CompanyLogo,
                CompanyWebsite = model.CompanyWebsite,
               Employees = model.Employees,

            };

            _context.Companies.Add(newCompany);
            _context.SaveChanges();
            return newCompany.CompanyID;
        }
        public List<CompanyViewModel> GetCompanies()
        {
            return DataSource();
        }
        public List<EmployeeViewModel> GetEmployees()
        {
            return EmployeeData();
        }

        public CompanyViewModel GetCompanyById(int id)
        {
            return DataSource().Where(c => c.CompanyID == id).FirstOrDefault();
        }
        public List<CompanyViewModel> SearchCompany(string name)
        {
            return DataSource().Where(c => c.CompanyName.Contains(name)).ToList();
        }

        private List<CompanyViewModel> DataSource()
        {
            return new List<CompanyViewModel>()
            {
                new CompanyViewModel(){CompanyID=1, CompanyName="Liz Ltd", CompanyLogo="hellologo",CompanyEmail="test@test.com",
                CompanyWebsite="www.liz.com"},
                new CompanyViewModel() {CompanyID=2, CompanyName="lizzy company", CompanyLogo="hiLogo", CompanyEmail="liz@test.com",
                CompanyWebsite="www.test.com" },
                new CompanyViewModel{CompanyID=3,CompanyName="John IT Ltd", CompanyLogo="Microsoft", CompanyEmail="john@test.com",
                CompanyWebsite="www.john.com" }

            };
        }

        private List<EmployeeViewModel> EmployeeData() {
            return new List<EmployeeViewModel>()
            {
                new EmployeeViewModel(){EmployeeID=1, FirstName="LizBeth", LastName="Testing", Email="liz@test.com", Phone="01256894" },
                new EmployeeViewModel(){EmployeeID=2, FirstName="John", LastName="Doe", Email="john@test.com", Phone="025698745" },
                new EmployeeViewModel() {EmployeeID=3, FirstName="Iron", LastName="Man", Email="iron@test.com", Phone="01259873" },
                new EmployeeViewModel{EmployeeID=4,FirstName="Fiona", LastName="Shrek", Email="fiona@test.com", Phone="014578963" }
            };
        }
    }
}
