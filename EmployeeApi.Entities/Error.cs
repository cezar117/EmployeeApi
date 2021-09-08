using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApi.Entities
{
    public class Error
    {
        /// <summary>
        /// Define Error Code
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Define Error Message
        /// </summary>
        public string Message { get; set; }
    }
}
