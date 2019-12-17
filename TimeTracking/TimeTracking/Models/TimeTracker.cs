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
        //start - stop method
        public void LogHours(Employee employee, Project project, 
                             TimeSpan time, string description)
        {        
            if (!CheckIfEmployeeCanWorkOnTheProject(employee, project))
            {
                throw new WorkingOnNotAssignProjectException();
                //return "Jūs negalite dirbti prie jums nepriskirto projekto";
            }       
            else
            {
                //creating commit for a project
                Commit commit = new Commit(description, employee, time);
                project.Commits.Add(commit);

                //added time employee worked on current project
                employee.TimeWorked = employee.TimeWorked.Add(time);              
            }   
        }

        public void LogHours(List<Project> projects, List<Commit> commits)
        {
            //checking for empty lists
            if (projects == null || commits == null || projects.Count != commits.Count)
            {
                throw new ArgumentNullException();
            }
            for (int i = 0; i < projects.Count; i++)
            {
                if (!CheckIfEmployeeCanWorkOnTheProject(commits[i].Employee, projects[i]))
                {
                    throw new WorkingOnNotAssignProjectException();
                    //"Jūs negalite dirbti prie jums nepriskirto projekto";
                }

                else if (CheckIfEmployeeWorkedMoreThenPossible(commits))
                {
                    throw new WorkedMoreThenPossibleDuringWorkdayException();
                    //"Jūs negalite per darbo dieną dirbti daugiau negu 16 val.!"
                }
                else
                {
                    //adding commit to a project
                    projects[i].Commits.Add(commits[i]);

                    // total time employee worked during month
                    //galbut reik padaryt kas menesi sito nunulinima
                    commits[i].Employee.TimeWorked = commits[i].Employee.TimeWorked.Add(commits[i].HoursWorked);
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
        /// <param name="commits">all commits</param>
        /// <returns>whether employee worked overtime</returns>
        public bool CheckForEmployeeOvertime(Employee employee, List<Commit> commits)
        {
            var currentMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var monthCommits = GetAllCommitsBetweenDates(
                                currentMonth, DateTime.Now, commits);

            //sorting all desired employee commits
            var sortedCommits = new List<Commit>();
            foreach (var commit in monthCommits)
            {
                if (commit.Employee.FullName == employee.FullName)
                {
                    sortedCommits.Add(commit);
                }
            }
            //calculating total work time of all commits
            TimeSpan totaTtime = CalculateCommitsTime(sortedCommits);

            // if employee exceeded monthly hours 
            if (employee.Budget < totaTtime)
            {
                return true;
            }
            else
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
                if (emp.FullName == employee.FullName)
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
            else
                return false;
        }

        private TimeSpan CalculateCommitsTime(List<Commit> commits)
        {
            TimeSpan totaTtime = TimeSpan.Zero;
            foreach (Commit commit in commits)
            {
                totaTtime = totaTtime.Add(commit.HoursWorked);
            }
            return totaTtime;
        }

        private List<Commit> GetAllCommitsBetweenDates(DateTime from, DateTime to, List<Commit> commits)
        {
            var sortedCommits = new List<Commit>();
            foreach (var commit in commits)
            {
                if (commit.CommitDate >= from && commit.CommitDate <= to)
                {
                    sortedCommits.Add(commit);
                }
            }
            return sortedCommits;
        }
    }
}
