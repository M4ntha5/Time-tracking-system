using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracking.Models.Interfaces
{
    public interface ITracker
    {
        void LogHours(Employee employee, Project project, TimeSpan hours, string description);
        void LogHours(List<Project> projects, List<Commit> commits);
        bool CheckForEmployeeOvertime(Employee employee, List<Commit> commits);
        bool CheckIfEmployeeCanWorkOnTheProject(Employee employee, Project project);
        bool CheckIfProjectBudgetExceeded(Project project);
        bool CheckIfEmployeeWorkedMoreThenPossible(List<Commit> commits);
    }
}
