using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracking.Models
{
    public class Commit
    {
        public string Description { get; set; }
        public DateTime CommitDate { get; set; }
        public Employee Employee { get; set; }
        public TimeSpan HoursWorked { get; set; }

        public Commit(string description, Employee employee, TimeSpan hoursWorked)
        {
            Description = description;
            CommitDate = DateTime.Now;
            Employee = employee;
            HoursWorked = hoursWorked;
        }

    }
}
