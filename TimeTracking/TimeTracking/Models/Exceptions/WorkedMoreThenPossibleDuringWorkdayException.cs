using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracking.Models.Exceptions
{
    public class WorkedMoreThenPossibleDuringWorkdayException : Exception
    {
        public WorkedMoreThenPossibleDuringWorkdayException()
            : base("Jūs negalite per darbo dieną dirbti daugiau negu 16 val.!")
        { }
    }
}
