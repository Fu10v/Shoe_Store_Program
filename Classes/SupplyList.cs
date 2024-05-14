using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoe_Store_DB.Classes
{
    internal class SupplyList
    {
        int id;
        int invoiceId;
        string product;
        string size;
        string color;
        double price;
        int quantity;

        public SupplyList(int id, int invoiceId, string product, string size, string color, double price, int quantity)
        {
            Id = id;
            InvoiceId = invoiceId;
            Product = product;
            Price = price;
            Quantity = quantity;
            Size = size;
            Color = color;
        }

        public int Id { get => id; set => id = value; }
        public int InvoiceId { get => invoiceId; set => invoiceId = value; }
        public string Product { get => product; set => product = value; }
        public double Price { get => price; set => price = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public string Size { get => size; set => size = value; }
        public string Color { get => color; set => color = value; }
    }
}
