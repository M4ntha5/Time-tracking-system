using NUnit.Framework;
using System;
using System.Collections.Generic;
using TimeTracking.Models;
using TimeTracking.Models.Interfaces;
using TimeTracking.Models.Exceptions;
using NSubstitute;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using CodeMash.Client;
using CodeMash.Models;
using CodeMash.Repository;
using MongoDB.Driver;
using MongoDB.Bson;
using CodeMash.Project.Services;


namespace Testing
{
    public class TrackerTests
    {
        /*
        Employee employee;
        Employee employee2;
        List<Employee> employees;

        List<Commit> commits;

        Project project;
        Project project2;
        List<Project> projects;
        */
        TimeTracker tracker;
        ITracker mock;
        INotifier notifierMock;
        TimeSpan time;
        

        Database db;
        DatabaseFindOneResponse<Employee> Employee;
        DatabaseFindOneResponse<Project> Project;


        [SetUp]
        public void Setup()
        {
            Environment.SetEnvironmentVariable("projectId", "f5e102ab-db85-41ad-8f7e-c4d414948ce6");
            Environment.SetEnvironmentVariable("apiKey", "RbJEZdTWLUE5EPkfWXJ5sUp1uM5IJYAd");
            
            db = new Database();
            mock = Substitute.For<ITracker>();
            notifierMock = Substitute.For<INotifier>();
            tracker = new TimeTracker(notifierMock);

            Employee = db.GetEmployeeById("5e1715b52ae6c800013e8c19");
            Project = db.GetProjectById("5e171b2e2ae6c800013ed766");



        }
        [Test]
        public void InsertProjectTest()
        { 
             //here works only without async
             //when doing same in startup it works also with async
            List<string> emp_ids = new List<string>();
            emp_ids.Add("5e1715952ae6c800013df834");
            emp_ids.Add("5e1715b52ae6c800013e8c19");

            Project project = new Project("nauajs", "dddd", 200, emp_ids);
            db.InsertProject(project);
        }


        [Test]
        public void InsertCommitTest()
        {
            //here works only without async
            //when doing same in startup it works also with async   
            Commit commit = new Commit("asd", "5e1715b52ae6c800013e8c19", 5);
            db.InsertCommit(commit);
        }

        [Test]
        public void InsertEmployeeTest()
        {
            //here works only without async
            //when doing same in startup it works also with async
            Employee emp = new Employee("Mantas", 160, 1);
            db.InsertEmployee(emp);
        }

        [Test]
        public void TestIfEmployeeCanLogHoursStartStop()
        {
           /* mock.CheckIfEmployeeCanWorkOnTheProject(employee, project).Returns(true);
            mock.LogHours(employee, project, time, "desc");

            Assert.AreEqual(true, mock.CheckIfEmployeeCanWorkOnTheProject(employee, project));
            mock.Received().LogHours(employee, project, time, "desc");*/

            tracker.LogHours(Employee, Project, new TimeSpan(3,2,1), "desc");

        }




        /*
                [SetUp]
                public void Setup()
                {



                    employee = new Employee("Mantas M", 160, 1);
                    employee2 = new Employee("Jonas J", 160, 2);
                    employees = new List<Employee>();
                    employees.Add(employee);
                    employees.Add(employee2);



                    mock = Substitute.For<ITracker>();
                    notifierMock = Substitute.For<INotifier>();
                    tracker = new TimeTracker(notifierMock);

                    project = new Project("Test", "no desc", 40, emp);
                    project2 = new Project("tes2", "d", 50, emp);
                    time = TimeSpan.FromHours(5);

                    projects = new List<Project>();
                    projects.Add(project);
                    projects.Add(project2);

                    commits = new List<Commit>();
                    commits.Add(new Commit("desc", employee, new TimeSpan(3, 5, 15)));
                    commits.Add(new Commit("desc", employee2, new TimeSpan(5, 5, 15)));
                }



                [Test]
                public void TestIfEmployeeCanLogHoursStartStop()
                {
                     mock.CheckIfEmployeeCanWorkOnTheProject(employee, project).Returns(true);
                     mock.LogHours(employee, project, time, "desc");

                     Assert.AreEqual(true, mock.CheckIfEmployeeCanWorkOnTheProject(employee, project));
                     mock.Received().LogHours(employee, project, time, "desc");

                     tracker.CheckIfEmployeeCanWorkOnTheProject(employee.Id, project);


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
                public void TestWhenCommitsAndProjectsListsCountDoNotMatch()
                {
                    commits.Add(new Commit("desc", employee, new TimeSpan(1, 5, 15)));
                    Assert.Throws<ProjectsAndCommitsQuantityDoNotMatchException>(
                                  () => tracker.LogHours(projects, commits));
                }

                [Test]
                public void TestIfEmployeeWorkedMoreThanPossible()
                {
                    projects.Add(new Project("tes3", "d", 50, employees));
                    commits.Add(new Commit("desc", employee2, new TimeSpan(12, 5, 15)));
                    Assert.Throws<WorkedMoreThenPossibleDuringWorkdayException>(
                                () => tracker.LogHours(projects, commits));
                }
                [Test]
                public void TestIfEmployeeDidntWorkedMoreThanPossible()
                {
                    Assert.AreEqual(false, tracker.CheckIfEmployeeWorkedMoreThenPossible(commits));

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
                    notifierMock.CheckForEmployeeOvertime(employee).Returns(false);

                    //changing to lower employee budget 
                    employee.Budget = new TimeSpan(3, 0, 0);

                    mock.LogHours(projects, commits);

                    Assert.AreEqual(false, notifierMock.CheckForEmployeeOvertime(employee));
                    mock.Received().LogHours(projects, commits);
                }

                [Test]
                public void TestIfEmployeeWorkedOvertime()
                {
                    notifierMock.CheckForEmployeeOvertime(employee).Returns(true);

                    //changing to lower employee budget 
                    employee.Budget = new TimeSpan(3, 0, 0);

                    mock.LogHours(projects, commits);

                    Assert.AreEqual(true, notifierMock.CheckForEmployeeOvertime(employee));
                    mock.Received().LogHours(projects, commits);
                }

                [Test]
                public void TestIfProjectBudgetExceeded()
                {
                    //lowering project budget
                    project.Budget = new TimeSpan(3, 0, 0);

                    project.Commits.Add(new Commit("de4c", employee, new TimeSpan(3, 5, 15) ));
                    project.Commits.Add(new Commit("de4c", employee, new TimeSpan(3, 5, 15) ));

                    var actual = tracker.CheckIfProjectBudgetExceeded(project);

                    Assert.AreEqual(true, actual);
                }

                [Test]
                public void TestIfProjectBudgetDidntExceeded()
                {
                    project.Commits.Add(new Commit("de4c", employee, new TimeSpan(3, 5, 15) ));
                    project.Commits.Add(new Commit("de4c", employee, new TimeSpan(3, 5, 15)));

                    var actual = tracker.CheckIfProjectBudgetExceeded(project);

                    Assert.AreEqual(false, actual);
                }
        */
    }
}