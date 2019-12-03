using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracking.Models.Exceptions
{
    public class ExceededProjectBudgetException : Exception
    {
        public ExceededProjectBudgetException(double amount)
            : base(String.Format("Projekto biudžetas buvo viršytas {0:0} €", Math.Abs(amount)))
        {
        }
    }
}
