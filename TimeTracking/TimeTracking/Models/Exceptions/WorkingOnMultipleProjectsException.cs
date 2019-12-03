using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracking.Models.Exceptions
{
    public class WorkingOnMultipleProjectsException :Exception
    {
        public WorkingOnMultipleProjectsException()
            : base(String.Format("Jūs negalite dirbti prie kelių projektų vienu metu!"))
        { }
    }
}
