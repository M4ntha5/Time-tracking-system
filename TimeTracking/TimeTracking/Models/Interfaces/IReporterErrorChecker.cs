using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracking.Models.Interfaces
{
    public interface IReporterErrorChecker
    {
        bool EmployeeIsManager(Employee employee);
    }
}
