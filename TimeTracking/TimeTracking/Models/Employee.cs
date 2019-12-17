using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracking.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Role { get; set; } //1-developer 2- manager
        public TimeSpan TimeWorked { get; set; }    //laikas 
        public DateTime Date { get; set; }          //date created  ((optional))
        public TimeSpan Budget { get; set; } // menesinis biudzetas valandom 160 default (kaip overtime)   


        public Employee(string fullName, double budget, int role)
        {
            FullName = fullName;
            Budget = TimeSpan.FromHours(budget);
            Role = role;
            TimeWorked = TimeSpan.Zero;
            Date = DateTime.Now;
        }
    }
}
