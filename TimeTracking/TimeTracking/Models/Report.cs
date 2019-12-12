using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracking.Models
{
    public class Report
    {
        public Project Project { get; set; }
        public string ProjectName { get; set; }        
        public string ProjectDescription { get; set; }
        public DateTime DateCreated { get; set; }
        public List<Employee> Employee { get; set; } // project empl
        public List<Commit> Commits { get; set; }

        public Report(Project project, string projectName, string projectDescription, 
                        DateTime dateCreated, List<Employee> employee, List<Commit> commits)
        {
            Project = project;
            ProjectName = projectName;
            ProjectDescription = projectDescription;
            DateCreated = dateCreated;
            Employee = employee;
            Commits = commits;
        }
    }
}
