using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApi.Entities.Employee
{
    public class EmployeeModel
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "LastName is required.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Rfc is required.")]
        public string Rfc { get; set; }
        [Required(ErrorMessage = "BornDate is required.")]
        public DateTime BornDate { get; set; }
        [Required(ErrorMessage = "Status is required."),  Range(0,2)]
        public EmployeeStatus Status { get; set; }
    }
}
