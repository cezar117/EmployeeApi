using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApi.Entities
{
    public abstract class ResponseBase<T>
    {
        /// <summary>
        /// Define Success Response
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Define InfoMessage for additional information
        /// </summary>
        public bool InfoMessage { get; set; }

        /// <summary>
        /// Define Error in Response
        /// </summary>
        public Error Error { get; set; }

        /// <summary>
        /// Data value
        /// </summary>
        public T Data { get; set; }
    }
}
