using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracking.Models.Exceptions
{
    public class WorkingOvertimeException : Exception
    {
        public WorkingOvertimeException()
        { }

        public WorkingOvertimeException(double overtimeSize)
            :base(String.Format("Jūs dirbote {0:0} h viršvalandžių", overtimeSize))
        {

        }
    }
}
