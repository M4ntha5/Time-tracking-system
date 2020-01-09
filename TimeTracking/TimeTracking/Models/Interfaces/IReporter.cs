using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracking.Models.Interfaces
{
    public interface IReporter
    {
        List<Project> SortProjects(DateTime from, DateTime to, List<Project> projects);
        List<Project> SortProjects(DateTime dateTo, List<Project> projects);
        List<Project> SortProjects(List<Project> projects, DateTime dateFrom);
        List<Project> SortProjects(Employee employee, List<Project> projects);
        List<Project> SortProjects(Project project, List<Project> projects);
        Project GetProjectDetails(Project project, Employee employee);
        bool CanEmployeeAccessReports(Employee employee);
    }
}
