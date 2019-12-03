using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTracking.Models.Interfaces;
using TimeTracking.Models.Exceptions;

namespace TimeTracking.Models
{
    public class Tracker : ITracker
    {
        IErrorChecker errorChecker;

        public Tracker(IErrorChecker errorChecker)
        {
            this.errorChecker = errorChecker;
        }

        public string StartWorkingOnProject(Employee employee, Project project)
        {
            if (!errorChecker.CheckIfEmployeeCanWorkOnTheProject(employee, project))
            {
                return "Jūs negalite dirbti prie jums nepriskirto projekto";
            }
            else if (!errorChecker.CheckIfEmployeeIsStillWorkingOnTheProject(employee))
            {
                throw new WorkingOnMultipleProjectsException();
                //return "Jūs negalite dirbti prie kelių projektų vienu metu!";
            }
            else
            { 
                employee.StartTime = DateTime.Now; //data kada pradejo darba prie projekto
                employee.HasProject = true;        //siuo metu dirba
                return "Success";
            }   
        }

        public string EndWorkingOnProject(Employee employee, Project project)
        {
            if(!errorChecker.CheckIfEmployeeStartedToWorkOnTheProject(employee))
            {
                return "Jūs dar nepradėjote dirbti prie šio projekto!";
            }
            employee.EndTime = DateTime.Now;    //laikas kada baige darba prie projekto
            employee.HasProject = false;        //flagas ar dirba prie vieno is projektu false=nedirba

            //laikas kiek pradirbo prie projekto
            TimeSpan timeWorked = employee.EndTime - employee.StartTime;

            //sumuojamas laikas prie bendro darbuotojo isdirbto laiko prie projekto
            employee.TotalTime = employee.TotalTime.Add(timeWorked);

            //is projekto biudzeto nuskaiciuojama pradirbta suma
            var empPrice = employee.HourlyRate * timeWorked.TotalHours;
            project.Budget -= empPrice;

            if(errorChecker.CheckIfBudgetExceeded(project))
            {
                throw new ExceededProjectBudgetException(project.Budget);
                //return String.Format("Projekto biudžetas buvo viršytas {0:0} €", Math.Abs(project.Budget));
            }

            //ar buvo dirbami virsvalandziai
            return CheckForOvertime(employee);
        }

        public string CheckForOvertime(Employee employee)
        {
            //laikas pradirbtas prie projekto
            TimeSpan timeWorked = employee.EndTime - employee.StartTime;

            //ar buvo dirbama daugiau nei 8h
            if (timeWorked.TotalHours > 8)
            {
                var overtimeSize = timeWorked.TotalHours - 8;
                throw new WorkingOvertimeException(overtimeSize);
                //return String.Format("Jūs dirbote {0:0} h viršvalandžių", overtimeSize);
            }
            else
                return "Laikas sėkmingai sustabdytas. Viršvalandžių nedirbta";
        }


    }
}
