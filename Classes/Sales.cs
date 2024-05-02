using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoe_Store_DB.Classes
{
    internal class Sales
    {
        int id;
        string employee;
        string customer;
        DateTime timeOfSale;
        string soldItems;

        public Sales(int id, string employee, string customer, DateTime time_of_sale, string soldItems)
        {
            Id = id;
            Employee = employee;
            Customer = customer;
            TimeOfSale = time_of_sale;
            SoldItems = soldItems;
        }

        public int Id { get => id; set => id = value; }
        public string Employee { get => employee; set => employee = value; }
        public string Customer { get => customer; set => customer = value; }
        public DateTime TimeOfSale { get => timeOfSale; set => timeOfSale = value; }
        public string SoldItems { get => soldItems; set => soldItems = value; }
    }
}
