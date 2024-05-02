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
        int price;
        int quantity;

        public SupplyList(int id, int invoiceId, string product, int price, int quantity)
        {
            Id = id;
            InvoiceId = invoiceId;
            Product = product;
            Price = price;
            Quantity = quantity;
        }

        public int Id { get => id; set => id = value; }
        public int InvoiceId { get => invoiceId; set => invoiceId = value; }
        public string Product { get => product; set => product = value; }
        public int Price { get => price; set => price = value; }
        public int Quantity { get => quantity; set => quantity = value; }
    }
}
