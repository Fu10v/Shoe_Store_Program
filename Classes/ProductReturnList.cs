using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoe_Store_DB.Classes
{
    internal class ProductReturnList
    {
        int id;
        int productReturnId;
        int salesListId;
        string product;
        string size;
        string color;
        int quantity;

        public ProductReturnList(int id, int productReturnId, int salesListId, string product, string size, string color, int quantity)
        {
            Id = id;
            ProductReturnId = productReturnId;
            SalesListId = salesListId;
            Product = product;
            Size = size;
            Color = color;
            Quantity = quantity;
        }

        public int Id { get => id; set => id = value; }
        public int ProductReturnId { get => productReturnId; set => productReturnId = value; }
        public int SalesListId { get => salesListId; set => salesListId = value; }
        public string Product { get => product; set => product = value; }
        public string Size { get => size; set => size = value; }
        public string Color { get => color; set => color = value; }
        public int Quantity { get => quantity; set => quantity = value; }
    }
}
