using CompanyDB.Data;
using CompanyDB.Data.Entity;
using CompanyDB.Models;
using CompanyDB.ViewModel;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CompanyDB.Repository
{
    public class CompanyRepository
    {
        private readonly AppDbContext _context;
        public CompanyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int>AddCompany(CompanyModel model)
        {
            var newCompany = new CompanyModel()
            { 
                CompanyName = model.CompanyName,
                CompanyEmail = model.CompanyEmail,
                CompanyLogo = model.CompanyLogo,
                CompanyWebsite = model.CompanyWebsite,
               //Employees = model.Employees,

            };

            await _context.Companies.AddAsync(newCompany);
            await _context.SaveChangesAsync();
            return newCompany.CompanyID;
        }
        public async Task<List<CompanyModel>> GetCompanies()
        {
            var companies = new List<CompanyModel>();
            var allcompanies = await _context.Companies.ToListAsync();
            if (allcompanies?.Any() == true)
            {
                foreach (var company in allcompanies)
                {
                    companies.Add(new CompanyModel()
                    {
                        CompanyName = company.CompanyName,
                        CompanyEmail = company.CompanyEmail,
                        CompanyLogo = company.CompanyLogo,
                        CompanyWebsite = company.CompanyWebsite
                    });
                }
            }
            return companies;
        }
        public List<EmployeeViewModel> GetEmployees()
        {
            return null;
        }

        public async Task<CompanyModel> GetCompanyById(int id)
        {
            return await _context.Companies.Where(c => c.CompanyID == id)
                .Select(company => new CompanyModel()
                {
                    CompanyName = company.CompanyName,
                    CompanyEmail = company.CompanyEmail,
                    CompanyLogo = company.CompanyLogo,
                    CompanyWebsite = company.CompanyWebsite
                }).FirstOrDefaultAsync();
            
        }

        public List<CompanyViewModel> SearchCompany(string name)
        {
            return null;
        }    

        
    }
}
