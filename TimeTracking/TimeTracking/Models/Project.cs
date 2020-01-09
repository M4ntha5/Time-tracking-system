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
using Newtonsoft.Json;

namespace TimeTracking.Models
{
    [CollectionName("Projects")]
    public class Project : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Budget { get; set; }            //project budget in hours
        public DateTime DateCreated { get; set; }
        //kartais veikia kai uzkomentuoti, kartais kai atkomentuoti???
        public List<DatabaseFindOneResponse<Employee>> Employees { get; set; }
        public List<string> Employees_ids { get; set; }
        public List<Commit> Commits { get; set; }       //all project commits
        public List<string> Commits_ids { get; set; }


        public Project(string name, string description, double budget, List<string> employees)
        {
            Name = name;
            Description = description;
            Budget = budget;
            Employees_ids = employees;
            DateCreated = DateTime.Now;
            Commits_ids = new List<string>();
            Employees = new List<DatabaseFindOneResponse<Employee>>();
            Commits = new List<Commit>();
        }

      /* public async void setEmployees()
        {
            Database db = new Database();

            var client = db.Connect();

            // 3. Create a service object
            var service = new CodeMashRepository<Employee>(client);
           /* foreach(string emp_id in Employees_ids)
            {
                this.Employees.Add(await service.FindOneByIdAsync(
                    emp_id,
                    new DatabaseFindOneOptions()
                ));
            }
        }

        public async void setCommits()
        {
            Database db = new Database();

            var client = db.Connect();

            // 3. Create a service object
            var service = new CodeMashRepository<Commit>(client);
            foreach (string commit_id in Commits_ids)
            {
               /* this.Commits.Add(await service.FindOneByIdAsync(
                    commit_id,
                    new DatabaseFindOneOptions()
                ));
            }
        }*/
    
    }
}
