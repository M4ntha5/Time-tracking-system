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
        TimeTracker tracker;
        ITracker mock;

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
        }

        [Test]
        public void TestIfEmployeeCanLogHoursStartStop()
        {
            TimeSpan time = TimeSpan.FromHours(5);
            mock = Substitute.For<ITracker>();

            mock.LogHours(employee, project, time, "desc");       

            mock.Received().LogHours(employee, project, time, "desc");

        }
        
        [Test]
        public void TestIfEmployeeCantStartWorkingOnTheProjectStartStop()
        {
            TimeSpan time = TimeSpan.FromHours(5);

            var emp = new Employee("petras", 160, 1);

            Assert.Throws<WorkingOnNotAssignProjectException>(
                         () => tracker.LogHours(emp, project, time, "heh"));

        }
        [Test]
        public void TestIfEmployeeCanLogHoursList()
        {
            mock = Substitute.For<ITracker>();

            List<Project> projects = new List<Project>();
            Project pro2 = new Project("tes2", "d", 50, employees);
            projects.Add(project);
            projects.Add(pro2);

            List<TimeSpan> times = new List<TimeSpan>();
            times.Add(new TimeSpan(3, 5, 15));
            times.Add(new TimeSpan(8, 6, 3));

            mock.LogHours(employee, projects, times, "lala");

            mock.Received().LogHours(employee, projects, times, "lala");
        }

        [Test]
        public void TestIfNullListsInLogHoursMethod()
        {
            Assert.Throws<ArgumentNullException>(
                          () => tracker.LogHours(employee, null, null, "lala")); 
        }

         [Test]
         public void TestIfEmployeeWorkedMoreThenPossible()
         {
            mock = Substitute.For<ITracker>();

            List<Project> projects = new List<Project>();
            Project pro2 = new Project("tes2", "d", 50, employees);
            projects.Add(project);
            projects.Add(pro2);

            List<TimeSpan> times = new List<TimeSpan>();
            times.Add(new TimeSpan(15, 5, 15));
            times.Add(new TimeSpan(8, 6, 3));


            Assert.Throws<WorkedMoreThenPossibleDuringWorkdayException>(
                          () => tracker.LogHours(employee, projects, times, "meh"));
         }

        [Test]
        public void TestIfEmployeeCanLogMultipleHours()
        {
            mock = Substitute.For<ITracker>();

            List<Project> projects = new List<Project>();
            Project pro2 = new Project("tes2", "d", 50, employees);
            projects.Add(project);
            projects.Add(pro2);

            List<TimeSpan> times = new List<TimeSpan>();
            times.Add(new TimeSpan(2, 5, 15));
            times.Add(new TimeSpan(8, 6, 3));


            mock.LogHours(employee, projects, times, "meh");

            mock.Received().LogHours(employee, projects, times, "meh");
        }

    }
}