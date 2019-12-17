using NUnit.Framework;
using System;
using System.Collections.Generic;
using TimeTracking.Models;
using TimeTracking.Models.Interfaces;
using TimeTracking.Models.Exceptions;
using NSubstitute;

namespace Testing
{
    public class TrackerTests
    {
        Employee employee;
        Employee employee2;
        List<Employee> employees;

        List<Commit> commits;

        Project project;
        Project project2;
        List<Project> projects;

        TimeTracker tracker;
        ITracker mock;  
        TimeSpan time;

        [SetUp]
        public void Setup()
        {         
            employee = new Employee("Mantas M", 160, 1);
            employee2 = new Employee("Jonas J", 160, 2);
            employees = new List<Employee>();
            employees.Add(employee);
            employees.Add(employee2);

            mock = Substitute.For<ITracker>();
            tracker = new TimeTracker();
            
            project = new Project("Test", "no desc", 40, employees);
            project2 = new Project("tes2", "d", 50, employees);
            time = TimeSpan.FromHours(5);

            projects = new List<Project>();
            projects.Add(project);
            projects.Add(project2);

            commits = new List<Commit>();
            commits.Add(new Commit("desc", employee, new TimeSpan(3, 5, 15)));
            commits.Add(new Commit("desc", employee2, new TimeSpan(15, 5, 15)));
        }

        [Test]
        public void TestIfEmployeeCanLogHoursStartStop()
        {  
            mock.CheckIfEmployeeCanWorkOnTheProject(employee, project).Returns(true);
            mock.LogHours(employee, project, time, "desc");

            Assert.AreEqual(true, mock.CheckIfEmployeeCanWorkOnTheProject(employee, project));
            mock.Received().LogHours(employee, project, time, "desc");
        }
        
        [Test]
        public void TestIfEmployeeCantStartWorkingOnTheProjectStartStop()
        {
            var emp = new Employee("petras", 160, 1);

            Assert.Throws<WorkingOnNotAssignProjectException>(
                         () =>  tracker.LogHours(emp, project, time, "heh"));

        }

        [Test]
        public void TestWhenNullCommitsAndProjectsListsInLogHoursMethod()
        {
            Assert.Throws<ArgumentNullException>(
                          () => tracker.LogHours(null, null)); 
        }
        [Test]
        public void TestWhenCommitsAndProjectsListsDoNotMatch()
        {
            commits.Add(new Commit("desc", employee, new TimeSpan(1, 5, 15)));
            Assert.Throws<ArgumentNullException>(
                          () => tracker.LogHours(projects, commits));
        }

        [Test]
        public void TestIfEmployeeWorkedMoreThanPossible()
        {
            Assert.Throws<WorkedMoreThenPossibleDuringWorkdayException>(
                        () => tracker.LogHours(projects, commits));
        }

        [Test]
        public void TestIfEmployeeCanLogMultipleHours()
        {
            mock.CheckIfEmployeeCanWorkOnTheProject(employee, project).Returns(true);
            mock.CheckIfEmployeeWorkedMoreThenPossible(commits).Returns(false);

            mock.LogHours(projects, commits);

            mock.Received().LogHours(projects, commits);
            Assert.AreEqual(true, mock.CheckIfEmployeeCanWorkOnTheProject(employee, project));
            Assert.AreEqual(false, mock.CheckIfEmployeeWorkedMoreThenPossible(commits));
        }

        [Test]
        public void TestIfEmployeeDidntWorkedOvertime()
        {
            //adding more commits for sorting purpose
            commits.Add(new Commit("de4c", employee, new TimeSpan(3, 5, 15)));
            commits.Add(new Commit("des45c", employee2, new TimeSpan(15, 5, 15)));
            commits.Add(new Commit("des4c", employee, new TimeSpan(3, 5, 15)));
            commits.Add(new Commit("d22esc", employee2, new TimeSpan(15, 5, 15)));

            Assert.AreEqual(false, tracker.CheckForEmployeeOvertime(employee, commits));        
        }

        [Test]
        public void TestIfEmployeeWorkedOvertime()
        {
            //adding more commits for sorting purpose
            commits.Add(new Commit("de4c", employee, new TimeSpan(3, 5, 15)));
            commits.Add(new Commit("des45c", employee2, new TimeSpan(15, 5, 15)));
            commits.Add(new Commit("des4c", employee, new TimeSpan(3, 5, 15)));
            commits.Add(new Commit("d22esc", employee, new TimeSpan(15, 5, 15)));

            //changing to lower employee budget 
            employee.Budget = new TimeSpan(15, 0, 0);

            var actual = tracker.CheckForEmployeeOvertime(employee, commits);

            Assert.AreEqual(true, actual);
        }

        [Test]
        public void TestIfProjectBudgetExceeded()
        {
            //lowering project budget
            project.Budget = new TimeSpan(3, 0, 0);

            project.Commits.Add(new Commit("de4c", employee, new TimeSpan(3, 5, 15)));
            project.Commits.Add(new Commit("de4c", employee, new TimeSpan(3, 5, 15)));

            var actual = tracker.CheckIfProjectBudgetExceeded(project);

            Assert.AreEqual(true, actual);
        }

        [Test]
        public void TestIfProjectBudgetDidntExceeded()
        {
            project.Commits.Add(new Commit("de4c", employee, new TimeSpan(3, 5, 15)));
            project.Commits.Add(new Commit("de4c", employee, new TimeSpan(3, 5, 15)));

            var actual = tracker.CheckIfProjectBudgetExceeded(project);

            Assert.AreEqual(false, actual);
        }

    }
}