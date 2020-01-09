using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTracking.Models.Interfaces;
using TimeTracking.Models.Exceptions;
using CodeMash.Client;
using CodeMash.Models;
using CodeMash.Repository;
using MongoDB.Driver;
using MongoDB.Bson;
using CodeMash.Project.Services;
using Isidos.CodeMash.ServiceContracts;
using TimeTracking.Models;
using Microsoft.Extensions.Configuration;


namespace TimeTracking.Models
{
    public class TimeTracker : ITracker
    {
        private INotifier notifier;
        Database db = new Database();
        public TimeTracker(INotifier notifier)
        {
            this.notifier = notifier;
        }

        //start - stop method
        public void LogHours(DatabaseFindOneResponse<Employee> employee,
                             DatabaseFindOneResponse<Project> project, 
                             TimeSpan time, string description)
        {
            
            if (!CheckIfEmployeeCanWorkOnTheProject(employee, project))
            {
                throw new WorkingOnNotAssignProjectException();
            }       
            else
            {
                //creating commit for a project
                Commit commit = new Commit(description, employee.Result.Id, Math.Round(time.TotalHours,2));
                //adding commit to db
                db.InsertCommit(commit);
                //cia neatsinaujina commit.id tad neina sito commito iterpt prie projekto
                project.Result.Commits_ids.Add(commit.Id);
                //cia reikia update project daryt
                
                //adding time employee worked on current project
                employee.Result.TimeWorked = employee.Result.TimeWorked + Math.Round(time.TotalHours, 2);    
                
                if(notifier.CheckForEmployeeOvertime(employee))
                {
                    var message = "Jus dirbote viršvalandžius";
                }
                
            }
        }
        //multiple projects - multiple hours method
        public void LogHours(List<Project> projects, List<Commit> commits)
        {
            //checking for empty lists
            if (projects == null || commits == null)
            {
                throw new ArgumentNullException();
            }
            if(projects.Count != commits.Count)
            {
                throw new ProjectsAndCommitsQuantityDoNotMatchException();
            }
            for (int i = 0; i < projects.Count; i++)
            {
                //pagalvot kaip cia su situ daryt
                if (!CheckIfEmployeeCanWorkOnTheProject(commits[i].Employee, projects[i]))
                {
                    throw new WorkingOnNotAssignProjectException();
                }

                else if (CheckIfEmployeeWorkedMoreThenPossible(commits))
                {
                    throw new WorkedMoreThenPossibleDuringWorkdayException();
                }
                else
                {
                    //inserting every commit to db
                    db.InsertCommit(commits[i]);
                    //adding commit to a project
                    projects[i].Commits.Add(commits[i]);


                    // total time employee worked during month
                    commits[i].Employee.Result.TimeWorked = 
                        commits[i].Employee.Result.TimeWorked + Math.Round(commits[i].TimeWorked);
                }
            }
        }

        public bool CheckIfEmployeeWorkedMoreThenPossible(List<Commit> commits)
        {
            //checking if employee worked more then 16h
            TimeSpan totalTime = TimeSpan.FromHours(CalculateCommitsTime(commits));

            if (totalTime > TimeSpan.FromHours(16))
            {
                return true;
            }
            
            return false;
        }
        /// <summary>
        /// checking if employee worked overtime
        /// </summary>
        /// <param name="employee">current employee</param>
        /// <returns>whether employee worked overtime</returns>
        public bool CheckForEmployeeOvertime(DatabaseFindOneResponse<Employee> employee)
        {
            //calculating total work time of all employee commits
            var totaTtime = employee.Result.TimeWorked;

            // if employee exceeded monthly hours 
            if (employee.Result.Budget < totaTtime)
            {
                return true;
            }
            
            return false;
        }
        /// <summary>
        /// checking whether employee can work on a project
        /// </summary>
        /// <param name="employee">employee trying to work</param>
        /// <param name="project">project, employee trying to work </param>
        /// <returns>whether employee can work on a project</returns>
        public bool CheckIfEmployeeCanWorkOnTheProject(
                                    DatabaseFindOneResponse<Employee> employee,
                                    DatabaseFindOneResponse<Project> project)
        {
            foreach (var emp_id in project.Result.Employees_ids)
            {
                if (emp_id == employee.Result.Id)
                {
                    return true;
                }
            }
            
            return false;
        }
        public bool CheckIfEmployeeCanWorkOnTheProject(Employee employee, Project project)
        {
            foreach (var emp_id in project.Employees_ids)
            {
                if (emp_id == employee.Id)
                {
                    return true;
                }
            }

            return false;
        }
        public bool CheckIfProjectBudgetExceeded(Project project)
        {
            //calculating total time of all project commits
            var totalTime = CalculateCommitsTime(project.Commits);

            //project budget exceeded or equals to zero
            if (project.Budget <= totalTime)
                return true;
                
            return false;
        }
        private double CalculateCommitsTime(List<Commit> commits)
        {
            double totaTtime = 0;
            foreach (var commit in commits)
            {
                totaTtime = totaTtime + commit.TimeWorked;
            }
            return totaTtime;
        }
    }
}
