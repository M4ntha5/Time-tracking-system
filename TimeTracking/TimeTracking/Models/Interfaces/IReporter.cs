using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracking.Models.Interfaces
{
    public interface IReporter
    {
        List<Project> SortProjects(DateTime from, DateTime to, List<Project> projects);
        //Report MakeReport(Project project);
    }
}
