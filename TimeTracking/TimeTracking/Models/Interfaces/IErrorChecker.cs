using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracking.Models.Interfaces
{
    public interface IErrorChecker
    {
        bool CheckIfEmployeeCanWorkOnTheProject(Employee employee, Project project);
        bool CheckIfEmployeeIsStillWorkingOnTheProject(Employee employee);
        bool CheckIfEmployeeStartedToWorkOnTheProject(Employee employee);
        bool CheckIfBudgetExceeded(Project project);
    }

}
