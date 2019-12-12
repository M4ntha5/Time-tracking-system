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
        Project project;
        Project project2;
        TimeTracker tracker;
        ITracker mock;
        List<TimeSpan> TimeList;
        List<TimeSpan> TimeListException;
        List<Project> projects;
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

            TimeList = new List<TimeSpan>();
            TimeList.Add(new TimeSpan(3, 5, 15));
            TimeList.Add(new TimeSpan(8, 6, 3));

            TimeListException = new List<TimeSpan>();
            TimeListException.Add(new TimeSpan(3, 5, 15));
            TimeListException.Add(new TimeSpan(8, 6, 3));
            TimeListException.Add(new TimeSpan(8, 6, 3));


            projects = new List<Project>();
            projects.Add(project);
            projects.Add(project2);
        }

        [Test]
        public void TestIfEmployeeCanLogHoursStartStop()
        {  
            mock = Substitute.For<ITracker>();

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
                         () => tracker.LogHours(emp, project, time, "heh"));

        }

        [Test]
        public void TestWhenNullListsInLogHoursMethod()
        {
            Assert.Throws<ArgumentNullException>(
                          () => tracker.LogHours(employee, null, null, "lala")); 
        }

        [Test]
        public void TestIfEmployeeWorkedMoreThenPossible()
        {
        Assert.Throws<WorkedMoreThenPossibleDuringWorkdayException>(
                        () => tracker.LogHours(employee, projects, TimeListException, "meh"));
        }

        [Test]
        public void TestIfEmployeeCanLogMultipleHours()
        {
            mock = Substitute.For<ITracker>();

            mock.CheckIfEmployeeCanWorkOnTheProject(employee, project).Returns(true);
            mock.CheckIfEmployeeWorkedMoreThenPossible(TimeList).Returns(false);

            mock.LogHours(employee, projects, TimeList, "lala");

            mock.Received().LogHours(employee, projects, TimeList, "lala");
            Assert.AreEqual(true, mock.CheckIfEmployeeCanWorkOnTheProject(employee, project));
            Assert.AreEqual(false, mock.CheckIfEmployeeWorkedMoreThenPossible(TimeList));
        }

    }
}