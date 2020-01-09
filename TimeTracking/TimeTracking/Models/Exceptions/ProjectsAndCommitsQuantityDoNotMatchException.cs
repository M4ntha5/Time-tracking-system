using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracking.Models.Exceptions
{
    public class ProjectsAndCommitsQuantityDoNotMatchException : Exception
    {
        public ProjectsAndCommitsQuantityDoNotMatchException() 
            : base("Registruojamų projaktų ir valandų kieks turi sutapti!")
        {

        }
    }
}
