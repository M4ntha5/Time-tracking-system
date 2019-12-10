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
        IReporterErrorChecker ErrorChecker;

        public Reporter(IReporterErrorChecker errorChecker)
        {
            ErrorChecker = errorChecker;
        }

        public List<Project> SortProjects(DateTime dateFrom, DateTime dateTo, List<Project> projects)
        {
            List<Project> sortedProjects = new List<Project>();
            if (dateFrom == null)
            {
                foreach (Project project in projects)
                {
                    if (project.DateCreated <= dateTo)
                    {
                        sortedProjects.Add(project);
                    }
                }
            }
            else if (dateTo == null)
            {
                foreach (Project project in projects)
                {
                    if (project.DateCreated >= dateFrom)
                    {
                        sortedProjects.Add(project);
                    }
                }
            }
            else
            {
                foreach (Project project in projects)
                {
                    if (project.DateCreated >= dateFrom && project.DateCreated <= dateTo)
                    {
                        sortedProjects.Add(project);
                    }
                }
            }
            return sortedProjects;
        }


       /* public Report MakeReport(Project project)
        {
            //total time all employees worked on this project
            TimeSpan totalEmpWorkTime = TimeSpan.Zero;

            List<Employee> employees = project.Employees;

            foreach (Employee employee in employees)
            {
                totalEmpWorkTime.Add(employee.TotalTime);
            }

            Report report = new Report()
            return report;
        }*/

        //employee - darbuotojas kuris dabar bando pasiekti reportus
        /*public Report GetReport(Project project, Employee employee)
        {
            if(!ErrorChecker.EmployeeIsManager(employee))
            {
                throw new CantAccessReportsException();
            }
            Report report = MakeReport(project);

            return report;
        }*/

      
    }
}
