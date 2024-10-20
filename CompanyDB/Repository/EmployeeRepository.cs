using CompanyDB.Data;
using CompanyDB.Data.Entity;
using CompanyDB.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyDB.Repository
{
    public class EmployeeRepository
    {
        private readonly AppDbContext _context = null;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public int AddEmployee(EmployeeModel employee)
        {
            var newEmployee = new Employee()
            {
                FirstName = employee.Firstname,
                LastName = employee.Lastname,
                CompanyID = employee.CompanyID,
                Email = employee.Email,
                Phone = employee.Phone,
            };
            _context.Employees.Add(newEmployee);
            _context.SaveChanges();
            return newEmployee.EmployeeID;
        }

        public async Task<List<EmployeeModel>> GetEmployees()
        {
            return await _context.Employees.Select(e => new EmployeeModel()
            {
                Firstname = e.FirstName,
                Lastname = e.LastName,
                Email = e.Email,
                Phone = e.Phone,
            }).ToListAsync();
        }

        public EmployeeModel GetEmployee(int id)
        {
            return null;
        }
        public List<EmployeeModel> SeachEmployee(string name)
        {
            return null;
        }
        public async Task<EmployeeModel> AddEmployee()
        {
            return null;
        }
    }
}
