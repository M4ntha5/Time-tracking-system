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
                            TimeSpan hours, string description)
        {        
            if (!CheckIfEmployeeCanWorkOnTheProject(employee, project))
            {
                throw new WorkingOnNotAssignProjectException();
                //return "Jūs negalite dirbti prie jums nepriskirto projekto";
            }       
            else
            {
                //sukuriams commitas projektui 
                Commit commit = new Commit(description, employee, hours);
                project.Commits.Add(commit);

                //laikas kiek isdirbta prie projekto
                employee.TimeWorked = hours;     

                //is projekto biudzeto nuskaiciuojama isdirbtos valandos
                project.Budget -= hours;

                //is darbuotojo biudzeto nuskaiciuojamos isdirbtos valandos
                employee.Budget -= hours;
                
            }   
        }
        // kai galima kelis projektus vienu metu suvest
        public void LogHours(Employee employee, List<Project> projects, 
                            List<TimeSpan> hours, string description)
        {
            //ar project su timespan listai nera tusti
            if(projects == null && hours == null)
            {
                throw new ArgumentNullException();
            }
            for (int i =0;i< projects.Count;i++)
            {
                if (!CheckIfEmployeeCanWorkOnTheProject(employee, projects[i]))
                {
                    throw new WorkingOnNotAssignProjectException();
                    //"Jūs negalite dirbti prie jums nepriskirto projekto";
                }
                else if(CheckIfEmployeeWorkedMoreThenPossible(hours))
                {
                    throw new WorkedMoreThenPossibleDuringWorkdayException();
                    //"Jūs negalite per darbo dieną dirbti daugiau negu 16 val.!"
                }
                else
                {
                    //sukuriams commitas projektui 
                    Commit commit = new Commit(description, employee, hours[i]);
                    projects[i].Commits.Add(commit);

                    //laikas kiek buvo dirbta per visa darbo diena prie skirtingu projektu
                    var totalTime = new TimeSpan(hours.Sum(r => r.Ticks));

                    //is projekto biudzeto nuskaiciuojama isdirbtos valandos
                    projects[i].Budget -= hours[i];

                    //is darbuotojo biudzeto nuskaiciuojamos isdirbtos valandos
                    employee.Budget -= totalTime;

                }
            }
        }

        public bool CheckIfEmployeeWorkedMoreThenPossible(List<TimeSpan> hours)
        {
            var totalTime = new TimeSpan(hours.Sum(r => r.Ticks));
            if(totalTime > TimeSpan.FromHours(16))
            {
                return true;
            }
            return false;
        }

        public bool CheckForEmployeeOvertime(Employee employee)
        {
            //ar darbuotojas virsijo menesini numatyta dirbti valandu kieki
            if (employee.Budget < TimeSpan.Zero)
            {
                return true;
            }
            else
                return false;
        }

        public bool CheckIfEmployeeCanWorkOnTheProject(Employee employee, Project project)
        {
            //patikrinama ar darbuotojas yra zmoniu priskirtu dirbti prie projekto sarase
            foreach(var emp in project.Employees)
            {
                if(emp.FullName == employee.FullName)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckIfProjectBudgetExceeded(Project project)
        {
            //projekto biudzetas virsytas arba pilnai isnaudotas
            if (project.Budget <= TimeSpan.Zero)
                return true;
            else
                return false;
        }
    }
}
