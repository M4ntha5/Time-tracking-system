using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracking.Models.Interfaces
{
    public interface IReporter
    {
        List<Project> SortProjects(DateTime from, DateTime to, List<Project> projects);
        List<Project> SortProjectsTo(DateTime dateTo, List<Project> projects);
        List<Project> SortProjectsFrom(DateTime dateFrom, List<Project> projects);
        Report GetProjectDetails(Project project, Employee employee);
        bool CanEmployeeAccessReports(Employee employee);
    }
}
