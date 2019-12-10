using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracking.Models.Interfaces
{
    public interface ITracker
    {
        void LogHours(Employee employee, Project project, TimeSpan hours, string description);
        void LogHours(Employee employee, List<Project> projects, List<TimeSpan> hours, string description);
        bool CheckForEmployeeOvertime(Employee employee);
        bool CheckIfEmployeeCanWorkOnTheProject(Employee employee, Project project);
        bool CheckIfProjectBudgetExceeded(Project project);
    }
}
