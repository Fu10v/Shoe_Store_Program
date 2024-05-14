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
        int productQuantityId;
        string product;
        string size;
        string color;
        int quantity;
        double price;
        double total;

        public SalesList(int id, int salesId, int productQuantityId, string productName, string size, string color, int quantity, double price, double total)
        {
            Id = id;
            SalesId = salesId;
            Product = productName;
            Quantity = quantity;
            Total = total;
            Price = price;
            Size = size;
            Color = color;
            ProductQuantityId = productQuantityId;
        }

        public int Id { get => id; set => id = value; }
        public int SalesId { get => salesId; set => salesId = value; }
        public string Product { get => product; set => product = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public double Total { get => total; set => total = value; }
        public double Price { get => price; set => price = value; }
        public string Size { get => size; set => size = value; }
        public string Color { get => color; set => color = value; }
        public int ProductQuantityId { get => productQuantityId; set => productQuantityId = value; }
    }
}
