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
        /// <summary>
        /// Sort all projects between selected dates
        /// </summary>
        /// <param name="dateFrom">Date from</param>
        /// <param name="dateTo">Date to</param>
        /// <param name="projects">All projects</param>
        /// <returns>Sorted projects between selected dates</returns>
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
        /// <summary>
        /// Sort projects from selected date
        /// </summary>
        /// <param name="dateFrom">Date from</param>
        /// <param name="projects">All projects</param>
        /// <returns>Sorted projects from selected date</returns>
        public List<Project> SortProjects(DateTime dateFrom, List<Project> projects)
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
        /// <summary>
        /// Sort projects to selected date
        /// </summary>
        /// <param name="projects">All projects</param>
        /// <param name="dateTo">Date to</param>
        /// <returns>Sorted projects to selected date</returns>
        public List<Project> SortProjects(List<Project> projects, DateTime dateTo)
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
        /// <summary>
        /// Sort project by selected employee
        /// </summary>
        /// <param name="employee">Selected employee</param>
        /// <param name="projects">All projects</param>
        /// <returns>All projects which includes selected employee</returns>
        public List<Project> SortProjects(Employee employee, List<Project> projects)
        {
            List<Project> sortedProjects = new List<Project>();
            foreach (Project project in projects)
            {
                foreach(var emp in project.Employees)
                {
                    if (emp.FullName == employee.FullName)
                    {
                        sortedProjects.Add(project);
                    }
                }       
            }
            return sortedProjects;
        }
        /// <summary>
        /// Sort all projects by given project name ((optional))
        /// </summary>
        /// <param name="project">Given project details</param>
        /// <param name="projects">All projects</param>
        /// <returns>Sorted projects by given project details</returns>
        public List<Project> SortProjects(Project project, List<Project> projects)
        {
            List<Project> sortedProjects = new List<Project>();
            foreach (Project pro in projects)
            {
                if (pro.Name == project.Name)
                {
                    sortedProjects.Add(project);
                }
            }
            return sortedProjects;
        }

        /*
         * optional????
         * ar uztenka tik project,
         *  nes reportas gaunasi kaip ir tas pats kaip projektas
         */
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
