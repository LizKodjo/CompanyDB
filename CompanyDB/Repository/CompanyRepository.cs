using CompanyDB.Models;

namespace CompanyDB.Repository
{
    public class CompanyRepository
    {
        public List<Company> GetCompanies()
        {
            return DataSource();
        }

        public Company GetCompanyById(int id)
        {
            return DataSource().Where(c => c.CompanyID == id).FirstOrDefault();
        }
        public List<Company> SearchCompany(string name)
        {
            return DataSource().Where(c => c.CompanyName.Contains(name)).ToList();
        }

        private List<Company> DataSource()
        {
            return new List<Company>()
            {
                new Company(){CompanyID=1, CompanyName="Liz Ltd", CompanyLogo="hellologo",CompanyEmail="test@test.com",
                CompanyWebsite="www.liz.com"},
                new Company() {CompanyID=2, CompanyName="lizzy company", CompanyLogo="hiLogo", CompanyEmail="liz@test.com",
                CompanyWebsite="www.test.com" },
                new Company{CompanyID=3,CompanyName="John IT Ltd", CompanyLogo="Microsoft", CompanyEmail="john@test.com",
                CompanyWebsite="www.john.com" }

            };
        }
    }
}
