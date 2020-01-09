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

namespace TimeTracking.Models
{
    public class Database
    {
        private CodeMashClient connect()
        {
            // 1. Get your Project ID and Secret Key
            var projectId = Guid.Parse(Environment.GetEnvironmentVariable("projectId"));
            var apiKey = Environment.GetEnvironmentVariable("apiKey");

            // 2. Create a general client for API calls
            var client = new CodeMashClient(apiKey, projectId);

            return client;
        }
        public async void GetAllEmployees()
        {
            var client = connect();
            // 3. Create a service object
            var service = new CodeMashRepository<Employee>(client);

            // 4. Call an API method
            var employees = await service.FindAsync(
                x => true,
                new DatabaseFindOptions()
            );        
        }

        public async void GetAllProjects()
        {
            var client = connect();
            // 3. Create a service object
            var service = new CodeMashRepository<Project>(client);

            // 4. Call an API method
            var projects = await service.FindAsync(
                x => true,
                new DatabaseFindOptions()
            );
        }

        public async void GetAllCommits()
        {
            var client = connect();
            // 3. Create a service object
            var service = new CodeMashRepository<Commit>(client);

            // 4. Call an API method
            var commits = await service.FindAsync(
                x => true,
                new DatabaseFindOptions()
            );
        }

        public async void GetEmployeeById(string id)
        {
            var client = connect();
            // 3. Create a service object
            var service = new CodeMashRepository<Employee>(client);

            // 4. Call an API method
            var employee = await service.FindOneByIdAsync(
                id,
                new DatabaseFindOneOptions()
            );
        }

        public async void GetCommitById(string id)
        {
            var client = connect();
            // 3. Create a service object
            var service = new CodeMashRepository<Commit>(client);

            // 4. Call an API method
            var commit = await service.FindOneByIdAsync(
                id,
                new DatabaseFindOneOptions()
            );
        }

        public async void DeleteCommitById(string id)
        {
            var client = connect();
            // 3. Create a service object
            var service = new CodeMashRepository<Commit>(client);

            // 4. Call an API method
            await service.DeleteOneAsync(
                x => x.Id == id
            );
        }

        public async void DeleteEmployeeById(string id)
        {
            var client = connect();
            // 3. Create a service object
            var service = new CodeMashRepository<emp>(client);

            // 4. Call an API method
            await service.DeleteOneAsync(
                x => x.Id == id
            );
        }

        public async void DeleteProjectById(string id)
        {
            var client = connect();
            // 3. Create a service object
            var service = new CodeMashRepository<Project>(client);

            // 4. Call an API method
            await service.DeleteOneAsync(
                x => x.Id == id
            );
        }
        public async void InsertCommit(Commit commit)
        {
            var client = connect();
            // 3. Create a service object
            var service = new CodeMashRepository<Commit>(client);

            //var commit = new Commit("desc", "5e1488a732bc640001bbb25e", 2);

            // 4. Call an API method

            await service.InsertOneAsync(commit, new DatabaseInsertOneOptions());
        }


    }
    
}
