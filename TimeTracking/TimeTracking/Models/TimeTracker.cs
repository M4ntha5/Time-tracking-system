using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTracking.Models.Interfaces;
using TimeTracking.Models.Exceptions;

namespace TimeTracking.Models
{
    public class TimeTracker : ITracker
    {
        private INotifier notifier;
        public TimeTracker(INotifier notifier)
        {
            this.notifier = notifier;
        }

        //start - stop method
        public void LogHours(Employee employee, Project project, 
                             TimeSpan time, string description)
        {        
            if (!CheckIfEmployeeCanWorkOnTheProject(employee, project))
            {
                throw new WorkingOnNotAssignProjectException();
            }       
            else
            {
                //creating commit for a project
                Commit commit = new Commit(description, employee, time);

                project.Commits.Add(commit);

                //added time employee worked on current project
                employee.TimeWorked = employee.TimeWorked.Add(time);    
                
                if(notifier.CheckForEmployeeOvertime(employee))
                {
                    var message = "Jus dirbote virsvalandzius";
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
                    //adding commit to a project
                    projects[i].Commits.Add(commits[i]);

                    var timeWorked = TimeSpan.FromHours(commits[i].TimeWorked);

                    // total time employee worked during month
                    commits[i].Employee.TimeWorked = 
                        commits[i].Employee.TimeWorked.Add(timeWorked);
                }
            }
        }

        public bool CheckIfEmployeeWorkedMoreThenPossible(List<Commit> commits)
        {
            //checking if employee worked more then 16h
            TimeSpan totalTime = CalculateCommitsTime(commits);

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
        public bool CheckForEmployeeOvertime(Employee employee)
        {
            //calculating total work time of all employee commits
            var totaTtime = employee.TimeWorked;

            //converting double type to timespan
            var employeeBudget = TimeSpan.FromHours(employee.Budget);

            // if employee exceeded monthly hours 
            if (employeeBudget < totaTtime)
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
        public bool CheckIfEmployeeCanWorkOnTheProject(Employee employee, Project project)
        {
            foreach (var emp in project.Employees)
            {
                if (emp.Id == employee.Id)
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
            //converting double type to timespan
            var projectBudget = TimeSpan.FromHours(project.Budget);

            //project budget exceeded or equals to zero
            if (projectBudget <= totalTime)
                return true;

            return false;
        }
        private TimeSpan CalculateCommitsTime(List<Commit> commits)
        {
            TimeSpan totaTtime = TimeSpan.Zero;
            foreach (Commit commit in commits)
            {
                //converting double type to timespan
                var timeWorked = TimeSpan.FromHours(commit.TimeWorked);
                totaTtime = totaTtime.Add(timeWorked);
            }
            return totaTtime;
        }
    }
}
