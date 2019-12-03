using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracking.Models
{
    public class Employee
    {
        public string FullName { get; set; }
        public int Rating { get; set; }
        public double HourlyRate { get; set; } //darbuotojo verte/ kaina bruto
        public int Role { get; set; } //1-developer 2-team lead 3- manager
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan TotalTime { get; set; }
       // public Project Project { get; set; } //emp turi tik viena projekta
        public bool HasProject { get; set; } // gal uztektu tik patikrint ar neuzbaigtas laikas
        public double Overtime { get; set; }

        public Employee(string fullName, int rating, double hourlyRate, int role)
        {
            FullName = fullName;
            Rating = rating;
            HourlyRate = hourlyRate;
            Role = role;
            StartTime = default(DateTime);
            EndTime = default(DateTime);
            TotalTime = TimeSpan.Zero;
           // Project = project;
            HasProject = false;
            Overtime = 0;
        }
    }
}
