using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoe_Store_DB.Classes
{
    internal class SalesList
    {
        int id;
        int salesId;
        string product;
        int quantity;

        public SalesList(int id, int salesId, string productName, int quantity)
        {
            Id = id;
            SalesId = salesId;
            Product = productName;
            Quantity = quantity;
        }

        public int Id { get => id; set => id = value; }
        public int SalesId { get => salesId; set => salesId = value; }
        public string Product { get => product; set => product = value; }
        public int Quantity { get => quantity; set => quantity = value; }
    }
}
