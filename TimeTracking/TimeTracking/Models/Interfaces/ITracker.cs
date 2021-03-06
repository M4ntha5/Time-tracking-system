﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeMash.Client;
using CodeMash.Models;
using CodeMash.Repository;
using MongoDB.Driver;
using CodeMash.Project.Services;
using Isidos.CodeMash.ServiceContracts;

namespace TimeTracking.Models.Interfaces
{
    public interface ITracker
    {
        public void LogHours(DatabaseFindOneResponse<Employee> employee,
                             DatabaseFindOneResponse<Project> project,
                             TimeSpan time, string description)
        void LogHours(List<Project> projects, List<Commit> commits);
        //bool CheckForEmployeeOvertime(Employee employee, List<Commit> commits);
        bool CheckIfEmployeeCanWorkOnTheProject(Employee employee, Project project);
        bool CheckIfProjectBudgetExceeded(Project project);
        bool CheckIfEmployeeWorkedMoreThenPossible(List<Commit> commits);
    }
}
