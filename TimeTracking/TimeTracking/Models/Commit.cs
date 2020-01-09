using CodeMash.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracking.Models
{
    [CollectionName("Commits")]
    public class Commit : Entity
    {
        public string Description { get; set; }
        public DateTime CommitDate { get; set; }
        public Employee Employee { get; set; }
        public string Employee_id { get; set; }
        public int TimeWorked { get; set; }
        

        public Commit(string description, string employee_id, int timeWorked)
        {
            Description = description;
            CommitDate = DateTime.Now;
            Employee_id = employee_id;
            TimeWorked = timeWorked;
        }

    }
}
