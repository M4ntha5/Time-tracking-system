using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracking.Models.Interfaces
{
    public interface ITracker
    {
        string StartWorkingOnProject(Employee employee, Project project);
        string EndWorkingOnProject(Employee employee, Project project);
        string CheckForOvertime(Employee employee);

    }
}
