using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoe_Store_DB.Classes
{
    internal class ProductCart : Product
    {
        int prductQuantityId;
        double total;

        public ProductCart(int id, string name, string gender, string type, string brand, string material, string season, string colors, string sizes, int price, int quantity, int prductQuantityId) : base(id, name, gender, type, brand, material, season, colors, sizes, price, quantity)
        {
            PrductQuantityId = prductQuantityId;
            Total = price * quantity;
        }

        public int PrductQuantityId { get => prductQuantityId; set => prductQuantityId = value; }
        public double Total { get => total; set => total = value; }
    }
}
