using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracking.Models.Exceptions
{
    public class WorkingOnNotAssignProjectException :Exception
    {
        public WorkingOnNotAssignProjectException()
            : base("Jūs negalite dirbti prie jums nepriskirto projekto")
        { }
    }
}
