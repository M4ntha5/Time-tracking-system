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
        public TimeSpan Budget { get; set; }            //valandom tarkim 40 
        public DateTime DateCreated { get; set; }
        public List<Employee> Employees { get; set; }   //projektas gali turet daug emp
        public List<Commit> Commits { get; set; }       //cia bus laikomi visi commitai siam projektui


        public Project(string name, string description, double budget, List<Employee> employees)
        {
            Name = name;
            Description = description;
            Budget = TimeSpan.FromHours(budget);
            DateCreated = DateTime.Now;
            Employees = employees;
            Commits = new List<Commit>();
        }
    }
}
