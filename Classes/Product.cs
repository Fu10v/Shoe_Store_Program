using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoe_Store_DB.Classes
{
    internal class Product
    {
        int id;
        string name;
        string gender;
        string type;
        string brand;
        string material;
        string season;
        string colors;
        string sizes;
        int price;
        int quantity;

        public Product() { }

        public Product(int id, string name, string gender, string type, string brand, string material, string season, string colors, string sizes, int price, int quantity)
        {
            Id = id;
            Name = name;
            Gender = gender;
            Type = type;
            Brand = brand;
            Material = material;
            Season = season;
            Colors = colors;
            Sizes = sizes;
            Price = price;
            Quantity = quantity;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Gender { get => gender; set => gender = value; }
        public string Type { get => type; set => type = value; }
        public string Brand { get => brand; set => brand = value; }
        public string Material { get => material; set => material = value; }
        public string Season { get => season; set => season = value; }
        public string Colors { get => colors; set => colors = value; }
        public string Sizes { get => sizes; set => sizes = value; }
        public int Price { get => price; set => price = value; }
        public int Quantity { get => quantity; set => quantity = value; }
    }
}
