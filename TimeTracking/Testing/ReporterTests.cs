using NUnit.Framework;
using System;
using System.Collections.Generic;
using TimeTracking.Models;
using TimeTracking.Models.Interfaces;
using TimeTracking.Models.Exceptions;
using NSubstitute;

namespace Testing
{
    class ReporterTests
    {
        Employee employee;
        Employee employee2;
        
        Project project;
        Project project2;
        List<Project> projects;
        Reporter reporter;
        IReporter mock;

        [SetUp]
        public void Setup()
        {
            employee = new Employee("Mantas M", 160, 1);
            employee2 = new Employee("Jonas J", 160, 2);

            List<Employee> employees = new List<Employee>();
            employees.Add(employee);
            employees.Add(employee2);

            mock = Substitute.For<IReporter>();
            reporter = new Reporter();

            project = new Project("Test", "no desc", 5000, employees);
            project2 = new Project("Test2", "no desc", 50, employees);

            projects = new List<Project>();
            projects.Add(project);
            projects.Add(project2);

        }

        [Test]
        public void TestIfEmployeeCantAccessReports()
        {
            Assert.Throws<CantAccessReportsException>(
                () => reporter.GetProjectDetails(project, employee));
        }
        [Test]
        public void TestIfEmployeeCanAccessReports()
        {
            mock.CanEmployeeAccessReports(employee).Returns(true);
            mock.GetProjectDetails(project, employee);

            Assert.AreEqual(true, mock.CanEmployeeAccessReports(employee));
            mock.Received().GetProjectDetails(project, employee); 
        }

        [Test]
        public void TestIfCanSortProjectsFromTo()
        {
            var dateFrom = new DateTime(2019, 12, 16);
            var dateTo = new DateTime(2019, 12, 17);

            mock.SortProjects(dateFrom, dateTo, projects);

            mock.Received().SortProjects(dateFrom, dateTo, projects);

        }

        [Test]
        public void TestIfCanSortProjectsFrom()
        {
            var dateFrom = new DateTime(2019, 12, 16);

            mock.SortProjects(projects, dateFrom);

            mock.Received().SortProjects(projects, dateFrom);
        }
        [Test]
        public void TestIfCanSortProjectsTo()
        {
            var dateTo = new DateTime(2019, 12, 17);

            mock.SortProjects(dateTo, projects);

            mock.Received().SortProjects(dateTo, projects);
        }
        [Test]
        public void TestIfCanSortProjectsByEmployee()
        {
            mock.SortProjects(employee, projects);

            mock.Received().SortProjects(employee, projects);
        }
        [Test]
        public void TestIfCanSortProjectsByProjectName()
        {
            mock.SortProjects(project, projects);

            mock.Received().SortProjects(project, projects);
        }

    }
}
