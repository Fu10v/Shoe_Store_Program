using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoe_Store_DB.Classes
{
    internal class ProductReturn
    {
        int id;
        int sale_id;
        string returnReason;
        string products;

        public ProductReturn(int id, int sale_id, string returnReason, string returnedProducts)
        {
            Id = id;
            Sale_id = sale_id;
            ReturnReason = returnReason;
            Products = returnedProducts;
        }

        public int Id { get => id; set => id = value; }
        public int Sale_id { get => sale_id; set => sale_id = value; }
        public string ReturnReason { get => returnReason; set => returnReason = value; }
        public string Products { get => products; set => products = value; }
    }
}
