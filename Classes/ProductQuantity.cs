using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoe_Store_DB.Classes
{
    internal class ProductQuantity
    {
        int id;
        int productId;
        string size;
        string color;
        int quantity;

        public ProductQuantity(int id, int productId, string size, string color, int quantity)
        {
            Id = id;
            ProductId = productId;
            Size = size;
            Color = color;
            Quantity = quantity;
        }

        public int Id { get => id; set => id = value; }
        public int ProductId { get => productId; set => productId = value; }
        public string Size { get => size; set => size = value; }
        public string Color { get => color; set => color = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        
    }
}
