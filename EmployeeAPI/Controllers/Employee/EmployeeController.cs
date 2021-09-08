using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeApi.Entities.Employee;
using EmployeeAPI.Core.Employee.Interface;
using EmployeeApi.Entities;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeAPI.Controllers.Employee
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeCore employeeCore;
        public EmployeeController(IEmployeeCore _employeeCore)
        {
            employeeCore = _employeeCore;
        }
        // GET: api/<EmployeeController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await employeeCore.GetEmployees().ConfigureAwait(false);

            return Ok(response);
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{rfc}")]
        public async Task<IActionResult> Get(string rfc)
        {
            rfc.Trim().ToUpper();
            var emp = await employeeCore.FindEmployeeByRfc(rfc).ConfigureAwait(false);
            if (emp.Rfc == null)
            {
                var response = new EmployeeResponse();
                response.Success = false;
                response.Error = new Error
                {
                    Code = 500,
                    Message = "Employee not Foud"
                };
                return Ok(response);
            }
            else
            {
                return Ok(emp);
            }
        }


        // POST api/<EmployeeController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmployeeModel model)
        {
 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var response = await employeeCore.SetEmployee(model).ConfigureAwait(false);
          
            return Ok(response);
        }
        // POST api/<EmployeeController>
        [HttpPost, Route("Form")]
        public async Task<IActionResult> PostEmployee([Required]string name, [Required]string lastName, [Required]string rfc, [Required]DateTime bornDate, [Required, Range(0, 2)]int status)
        {
            EmployeeStatus employeeStatus= Enum.IsDefined(typeof(EmployeeStatus), status) ? (EmployeeStatus)status : 0;


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var model = new EmployeeModel()
            {
                Name = name,
                LastName = lastName,
                Rfc = rfc,
                BornDate = bornDate,
                Status = employeeStatus
            };

            var response = await employeeCore.SetEmployee(model).ConfigureAwait(false);

            return Ok(response);
        }

    }
}
