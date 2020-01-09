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
    [CollectionName("Employees")]
    public class Employee : Entity
    {
        public string FullName { get; set; }
        public int Role { get; set; } //1-developer 2- manager
        /*
        * !!!!!!!This atribute must be set to 0 every 1st day of the month!!!!!!!!!!
        */
        public TimeSpan TimeWorked { get; set; } //employee actual work time during month
        public int Budget { get; set; } //monthly budget in hours (160h when full time)   


        public Employee(string fullName, int budget, int role)
        {
            FullName = fullName;
            Budget = budget;
            Role = role;
        }
        


    }
}
