using EmployeeApi.Entities.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApi.DataAccess.Employee.Interfaces
{
    public interface IEmployeeRepository
    {
        public Task<List<EmployeeModel>> GetEmployees();
        public Task<int> SetEmployee(EmployeeModel employee);
        public Task<EmployeeModel> FindEmployeeByRfc(string Rfc);
    }
}
