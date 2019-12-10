using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracking.Models.Exceptions
{
    public class CantAccessReportsException :Exception
    {
        public CantAccessReportsException()
            : base("Jūs neturite teisių peržiūrėtiprojektų ataskaitas")
        {

        }
    }
}
