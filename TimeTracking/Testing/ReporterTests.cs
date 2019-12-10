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
       /* Employee employee;
        Employee employee2;
        List<Employee> employees;
        IReporterErrorChecker mock;
        Project project;
        Reporter reporter;

        [SetUp]
        public void Setup()
        {
            employee = new Employee("Mantas M", 1, 5, 1);
            employee2 = new Employee("Jonas J", 2, 5, 1);
            employees = new List<Employee>();
            employees.Add(employee);
            employees.Add(employee2);

            mock = Substitute.For<IReporterErrorChecker>();
            reporter = new Reporter(mock);
            var projectDate = new DateTime(2019, 12, 1);
            project = new Project("Test", "no desc", 5000, projectDate, employees);
        }

        [Test]
        public void TestIfDeveloperCantAccessReports()
        {
            mock.EmployeeIsManager(employee).Returns(false);
            Assert.Throws<CantAccessReportsException>(() => reporter.GetReport(project, employee));

        }
        [Test]
        public void TestIfManagerCanAccessReports()
        {
            mock.EmployeeIsManager(employee2).Returns(true);

            var expected = new Report(project, employees, TimeSpan.Zero);

            var actual = reporter.GetReport(project, employee2);

            Assert.AreEqual(expected, actual);

        }
        */
    }
}
