using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracking.Models
{
    public class Report
    {
        public List<Project> Project { get; set; }
        public string Name { get; set; }        
        public string Description { get; set; } 

        public Report(List<Project> project, string name, string description)
        {
            Project = project;
            Name = name;
            Description = description;
        }
    }
}
