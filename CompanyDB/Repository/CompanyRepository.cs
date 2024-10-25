using CompanyDB.Data;
using CompanyDB.Data.Entity;
using CompanyDB.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyDB.Repository
{
    public class CompanyRepository
    {

        private readonly AppDbContext _context;

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

        public async Task<List<CompanyModel>> GetCompanies()
        {
            return await _context.Companies
                .Select(company => new CompanyModel()
                { 
                    CompanyName = company.CompanyName, 
                    CompanyEmail = company.CompanyEmail, 
                    CompanyLogo = company.CompanyLogo, 
                    CompanyWebsite = company.CompanyWebsite 
                }).ToListAsync();

        }



        public async Task<List<CompanyModel>> GetCompaniesAsync()
        {
            return await _context.Companies
                .Select(company => new CompanyModel()
                {
                    CompanyName = company.CompanyName,
                    CompanyEmail = company.CompanyEmail,
                    CompanyLogo = company.CompanyLogo,
                    CompanyWebsite = company.CompanyWebsite
                }).Take(5).ToListAsync();

        }


        public async Task<CompanyModel> GetCompanyById(int id)
        {
            return await _context.Companies.Where(c => c.CompanyID == id)
                .Select(company => new CompanyModel()
                {
                    CompanyName = company.CompanyName,
                    CompanyEmail = company.CompanyEmail,
                    CompanyLogo = company.CompanyLogo,
                    CompanyWebsite = company.CompanyWebsite,
                }).FirstOrDefaultAsync();
        }
        public async Task<List<CompanyModel>> SearchCompany(string name)
        {
            return null;
        }

    }
}
