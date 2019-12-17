using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTracking.Models.Interfaces;
using TimeTracking.Models.Exceptions;

namespace TimeTracking.Models
{
    public class Reporter : IReporter
    {
        public List<Project> SortProjects(DateTime dateFrom, DateTime dateTo, List<Project> projects)
        {
            List<Project> sortedProjects = new List<Project>();
            foreach (Project project in projects)
            {
                if (project.DateCreated >= dateFrom && project.DateCreated <= dateTo)
                {
                    sortedProjects.Add(project);
                }
            }         
            return sortedProjects;
        }

        public List<Project> SortProjectsFrom(DateTime dateFrom, List<Project> projects)
        {
            List<Project> sortedProjects = new List<Project>();
            foreach (Project project in projects)
            {
                if (project.DateCreated >= dateFrom)
                {
                    sortedProjects.Add(project);
                }
            }       
            return sortedProjects;
        }
        public List<Project> SortProjectsTo(DateTime dateTo, List<Project> projects)
        {
            List<Project> sortedProjects = new List<Project>();
            foreach (Project project in projects)
            {
                if (project.DateCreated <= dateTo)
                {
                    sortedProjects.Add(project);
                }
            }
            return sortedProjects;
        }


        public Report GetProjectDetails(Project project, Employee employee)
        {
            if(!CanEmployeeAccessReports(employee))
            {
                throw new CantAccessReportsException();
            }

            Report report = new Report(
                project, project.Name, project.Description,
                project.DateCreated, project.Employees, project.Commits
            );

            return report;
        }

        public bool CanEmployeeAccessReports(Employee employee)
        {
            if (employee.Role == 2)
                return true;
            else 
                return false;
        }
    }
}
