using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracking.Models
{
    public class Report
    {
        //public List<Employee> Employees { get; set; } // gal but nereik nes project turi lista darbuotoju
        public List<Project> Projects { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public Report(List<Project> projects, string name, string description, DateTime date)
        {
            Projects = projects;
            Name = name;
            Description = description;
            Date = date;
        }

        public void GetReport(DateTime from, DateTime to)
        {
            List<Report> reports = new List<Report>();

            if(this.Date >= from && this.Date <= to)
            {
                
            }
        }
    }
}
