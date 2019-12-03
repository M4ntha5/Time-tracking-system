using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracking.Models
{
    public class Project
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Budget { get; set; }
        public List<Employee> Employees { get; set; } //projektas turi daug emp

        public Project(string name, string description, double budget, List<Employee> employees)
        {
            Name = name;
            Description = description;
            Budget = budget;
            Employees = employees;
        }
    }
}
