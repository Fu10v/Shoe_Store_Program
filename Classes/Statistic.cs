using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoe_Store_DB.Classes
{
    internal class Statistic
    {
        DateTime dateTime;
        double total;

        public Statistic(DateTime dateTime, double total)
        {
            DateTime = dateTime;
            Total = total;
        }

        public DateTime DateTime { get => dateTime; set => dateTime = value; }
        public double Total { get => total; set => total = value; }
    }
}
