using CompanyDB.Data;
using CompanyDB.Data.Entity;
using CompanyDB.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyDB.Repository
{
    public class CompanyRepository
    {

        private readonly AppDbContext _context = null;

        public CompanyRepository(AppDbContext context)
        {
            _context = context;
        }

        //public async Task<CompanyModel> AddCompany(CompanyModel company)
        public async Task<int> AddCompany(CompanyModel company)
        {
            var newCompany = new Company()
            {
                CompanyName = company.CompanyName,
                CompanyEmail = company.CompanyEmail,
                CompanyLogo = company.CompanyLogo,
                CompanyWebsite = company.CompanyWebsite
            };
            await _context.Companies.AddAsync(newCompany);
            await _context.SaveChangesAsync();
            return newCompany.CompanyID; 
        }

        public async Task<List<CompanyModel>>? GetCompanies()
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


        public async Task<CompanyModel> GetCompanyById(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            //_context.Companies.Where(c => c.CompanyID == id).FirstOrDefaultAsync();
            if (company != null)
            {
                var companyDetails = new CompanyModel()
                {
                    CompanyName = company.CompanyName,
                    CompanyEmail = company.CompanyEmail,
                    CompanyLogo = company.CompanyLogo,
                    CompanyWebsite = company.CompanyWebsite
                };
                return companyDetails;
            }
            return null;
        }
        public async Task<List<CompanyModel>> SearchCompany(string name)
        {
            return null;
        }

    }
}
