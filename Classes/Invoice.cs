using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoe_Store_DB.Classes
{
    internal class Invoice
    {
        int id;
        string employee;
        string customer;
        DateTime supplyTime;
        string deliveredItems;

        public Invoice(int id, string employee, string customer, DateTime supplyTime, string deliveredItems)
        {
            Id = id;
            Employee = employee;
            Customer = customer;
            SupplyTime = supplyTime;
            DeliveredItems = deliveredItems;
        }

        public int Id { get => id; set => id = value; }
        public string Employee { get => employee; set => employee = value; }
        public string Customer { get => customer; set => customer = value; }
        public DateTime SupplyTime { get => supplyTime; set => supplyTime = value; }
        public string DeliveredItems { get => deliveredItems; set => deliveredItems = value; }
    }
}
