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

    }
}
