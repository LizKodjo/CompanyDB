using CompanyDB.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CompanyDB.Components
{
    public class VariousCompaniesViewComponent : ViewComponent
    {
        private readonly CompanyRepository _companyRepository;
        public VariousCompaniesViewComponent(CompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var companies = await _companyRepository.GetCompaniesAsync();
            return View(companies);
        }
    }
}
