using NUnit.Framework;
using System;
using System.Collections.Generic;
using TimeTracking.Models;
using TimeTracking.Models.Interfaces;
using TimeTracking.Models.Exceptions;
using NSubstitute;

namespace Testing
{
    public class Tests
    {
        Employee employee;
        Employee employee2;
        List<Employee> employees;
        IErrorChecker errorMock;
        Project project;
        Tracker tracker;

        [SetUp]
        public void Setup()
        {         
            employee = new Employee("Mantas M", 1, 5, 1);
            employee2 = new Employee("Jonas J", 1, 5, 1);
            employees = new List<Employee>();
            employees.Add(employee);
            employees.Add(employee2);

            errorMock = Substitute.For<IErrorChecker>();
            tracker = new Tracker(errorMock);
            project = new Project("Test", "no desc", 5000, employees);
        }

        [Test]
        public void TestIfemployeeCanSuccessfullyStartWorkingOnTheProject()
        {
            errorMock.CheckIfEmployeeCanWorkOnTheProject(employee, project).Returns(true);
            errorMock.CheckIfEmployeeIsStillWorkingOnTheProject(employee).Returns(true);

            var actual = tracker.StartWorkingOnProject(employee, project);

            Assert.AreEqual("Success", actual);

        }
        [Test]
        public void TestIfEmployeeCantStartWorkingOnTheProject()
        {
            errorMock.CheckIfEmployeeCanWorkOnTheProject(employee, project).Returns(false);
            errorMock.CheckIfEmployeeIsStillWorkingOnTheProject(employee).Returns(true);

            var actual = tracker.StartWorkingOnProject(employee, project);

            Assert.AreEqual("Jūs negalite dirbti prie jums nepriskirto projekto", actual);

        }
        [Test]
        public void TestIfEmployeeIsAlreadyWorkingOnTheProject()
        {
            errorMock.CheckIfEmployeeCanWorkOnTheProject(employee, project).Returns(true);
            errorMock.CheckIfEmployeeIsStillWorkingOnTheProject(employee).Returns(false);

            //var actual = tracker.StartWorkingOnProject(employee, project);
            //Assert.AreEqual("Jūs negalite dirbti prie kelių projektų vienu metu!", actual);

            Assert.Throws<WorkingOnMultipleProjectsException>(() => tracker.StartWorkingOnProject(employee, project));

        }

        [Test]
        public void TestIfEmployeeCanStopWorkingOnTheProjectAndNoOvertime()
        {
            errorMock.CheckIfEmployeeCanWorkOnTheProject(employee, project).Returns(true);
            errorMock.CheckIfEmployeeIsStillWorkingOnTheProject(employee).Returns(true);
            tracker.StartWorkingOnProject(employee, project);

            errorMock.CheckIfEmployeeStartedToWorkOnTheProject(employee).Returns(true);

            var actual = tracker.EndWorkingOnProject(employee, project);

            Assert.AreEqual("Laikas sėkmingai sustabdytas. Viršvalandžių nedirbta", actual);

        }

        [Test]
        public void TestIfEmployeeCanStopWorkingOnTheProjectWithSomeOvertime()
        {
            errorMock.CheckIfEmployeeCanWorkOnTheProject(employee, project).Returns(true);
            errorMock.CheckIfEmployeeIsStillWorkingOnTheProject(employee).Returns(true);
            tracker.StartWorkingOnProject(employee, project);

            //rankiniu budu atimu 10 h kad zinociau jog buvo dirbama 2h virsvalandziu
            employee.StartTime = employee.StartTime.AddHours(-10);

            errorMock.CheckIfEmployeeStartedToWorkOnTheProject(employee).Returns(true);

            //var actual = tracker.EndWorkingOnProject(employee, project);
            //Assert.AreEqual("Jūs dirbote 2 h viršvalandžių", actual);

            Assert.Throws<WorkingOvertimeException>(() => tracker.EndWorkingOnProject(employee, project));
        }

        [Test]
        public void TestIfBudgetExceeded()
        {
            errorMock.CheckIfEmployeeCanWorkOnTheProject(employee, project).Returns(true);
            errorMock.CheckIfEmployeeIsStillWorkingOnTheProject(employee).Returns(true);
            tracker.StartWorkingOnProject(employee, project);

            //sumazinu projekto biudzeta kad bendra suma virsytu projekto biudzeta
            project.Budget = 5;

            //rankiniu budu atimu 10 h kad zinociau jog buvo dirbama 2h virsvalandziu
            employee.StartTime = employee.StartTime.AddHours(-10);

            errorMock.CheckIfEmployeeStartedToWorkOnTheProject(employee).Returns(true);
            errorMock.CheckIfBudgetExceeded(project).Returns(true);

            //var actual = tracker.EndWorkingOnProject(employee, project);
            //Assert.AreEqual("Projekto biudžetas buvo viršytas 45 €", actual);

            Assert.Throws<ExceededProjectBudgetException>(() => tracker.EndWorkingOnProject(employee, project));

        }
    }
}