using EmployeeApi.Entities.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Core.Employee.Interface
{
    public interface IEmployeeCore
    {
        public Task<List<EmployeeModel>> GetEmployees();
        public Task<EmployeeResponse> SetEmployee(EmployeeModel employee);
        public Task<EmployeeModel> FindEmployeeByRfc(string Rfc);
    }
}
