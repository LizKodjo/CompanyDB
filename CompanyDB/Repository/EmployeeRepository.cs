using CompanyDB.Data;
using CompanyDB.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace CompanyDB.Repository
{
    public class EmployeeRepository
    {
        private readonly AppDbContext _context = null;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeeModel>> GetEmployees()
        {
            return await _context.Employees.Select(e => new EmployeeModel()
                {
                EmployeeID = e.EmployeeID,
                FirstName = e.FirstName,
                LastName = e.LastName,
                CompanyID = e.CompanyID,
                Email = e.Email,
                Phone = e.Phone

            }).ToListAsync();
        }
    }
}
