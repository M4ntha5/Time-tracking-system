using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeMash.Repository;

namespace TimeTracking.Models.Interfaces
{
    public interface INotifier
    {
        bool CheckForEmployeeOvertime(DatabaseFindOneResponse<Employee> employee);
    }
}
