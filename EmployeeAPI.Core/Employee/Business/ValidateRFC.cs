using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmployeeAPI.Core.Employee.Business
{
    public class ValidateRFC
    {

        public bool ValidateRfc(string rfc)
        {
            var rfcUpper = rfc.Trim().ToUpper();
            //string pattern = @"^([A-ZÑ&]{3,4})(?:- )?(\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01]))(?:- )?([A-Z\d]{2})([A\d])$";
            string pattern = @"^([A-ZÑ&]{3,4})?(?:- ?)?(\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01]))?(?:- ?)?([A-Z\d]{2})([A\d])$";
            Regex rg = new(pattern);
            var isValid = rg.IsMatch(rfcUpper);

            if (!isValid)
            {
                return false;
            }
            else
            {
                var valid = rg.Match(rfc);
                var val = valid.Value;
                return true;
            }
        }
    }
}
