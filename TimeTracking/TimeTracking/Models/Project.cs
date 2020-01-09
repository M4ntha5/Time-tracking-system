using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeMash.Client;
using CodeMash.Models;
using CodeMash.Repository;
using MongoDB.Driver;
using CodeMash.Project.Services;
using Isidos.CodeMash.ServiceContracts;

namespace TimeTracking.Models
{
    [CollectionName("Projects")]
    public class Project : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Budget { get; set; }            //project budget in hours
        public DateTime DateCreated { get; set; }
        public List<Employee> Employees { get; set; }
        public List<string> Employees_ids { get; set; }
        public List<Commit> Commits { get; set; }       //all project commits
        public List<string> Commits_ids { get; set; }


        public Project(string name, string description, double budget, List<string> employees)
        {
            Name = name;
            Description = description;
            Budget = budget;
            Commits = new List<Commit>();
            Employees_ids = employees;
        }
    }
}
