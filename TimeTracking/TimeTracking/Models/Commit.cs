using System;
using CodeMash.Client;
using CodeMash.Models;
using CodeMash.Repository;
using MongoDB.Driver;
using MongoDB.Bson;
using CodeMash.Project.Services;
using Isidos.CodeMash.ServiceContracts;
using TimeTracking.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TimeTracking.Models
{
    [CollectionName("Commits")]
    public class Commit : Entity
    {
        public string Description { get; set; }
        public DateTime CommitDate { get; set; }
        public string Employee_id { get; set; }
        public double TimeWorked { get; set; }
        public DatabaseFindOneResponse<Employee> Employee { get; set; }


        public Commit(string description, string employee_id, double timeWorked)
        {
            Description = description;
            CommitDate = DateTime.Now;
            Employee_id = employee_id;
            TimeWorked = timeWorked;
            Employee = new DatabaseFindOneResponse<Employee>();
        }
        public Commit()
        { }

        private async void setEmployee()
        {
            Database db = new Database();

            var client = db.Connect();
            // 3. Create a service object
            var service = new CodeMashRepository<Employee>(client);

            // 4. Call an API method
            /*this.Employee = await service.FindOneByIdAsync(
                this.Employee_id,
                new DatabaseFindOneOptions()
            );*/
        }

    }
}
