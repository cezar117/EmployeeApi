using EmployeeApi.DataAccess.Employee.Interfaces;
using EmployeeApi.Entities;
using EmployeeApi.Entities.Employee;
using EmployeeAPI.Core.Employee.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeAPI.Core.Employee.Business
{
    public class EmployeeCore : IEmployeeCore
    {
        private readonly IEmployeeRepository employeeRepository;
        public EmployeeCore(IEmployeeRepository _employeeRepository) { employeeRepository = _employeeRepository; }
        public  Task<EmployeeModel> FindEmployeeByRfc(string Rfc)
        {
            var emp =  employeeRepository.FindEmployeeByRfc(Rfc);
            return emp;

        }

        public  async Task<List<EmployeeModel>> GetEmployees()
        {
            return await employeeRepository.GetEmployees().ConfigureAwait(false); 
           
        }

        public async Task<EmployeeResponse> SetEmployee(EmployeeModel employee)
        {
            var response = new EmployeeResponse();
            var rfc = new ValidateRFC();
           var rs =  rfc.ValidateRfc(employee.Rfc);
            if (!rs)
            {
                response.Success = false;
                response.Error = new Error
                {
                    Code = 500,
                    Message = "El RFC No es Valido"
                };
                return response;
            }
            employee.Rfc = employee.Rfc.Trim().ToUpper();
            var emp = await FindEmployeeByRfc(employee.Rfc).ConfigureAwait(false);
            if (emp.Status != EmployeeStatus.NotSet || emp.Rfc == employee.Rfc)
            {
                response.Success = false;
                response.Error = new Error
                {
                    Code = 500,
                    Message = "Ya Existe un Empleado Registrado con el mismo RFC"
                };
               
            }
            else
            {
                int result = await employeeRepository.SetEmployee(employee).ConfigureAwait(false);
                if (result > 0)
                {

                    response.Data = "Employee Created";
                    response.Success = true;
                }
                else
                {

                    response.Success = false;
                    response.Error = new Error
                    {
                        Code = 500,
                        Message = "Employee Error"
                    };
                }
            }

            return response;
        }
    }
}
