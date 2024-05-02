using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoe_Store_DB.Classes
{
    class Customer
    {
        int id;
        string name;
        long phoneNumber;
        string email;
        long discountCardId;
        double discountCardAccumulation;

        public Customer(int id, string name, long phoneNumber, string email, long discountCardId, double discountCardAccumulation)
        {
            Id = id;
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
            DiscountCardId = discountCardId;
            DiscountCardAccumulation = discountCardAccumulation;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public long PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string Email { get => email; set => email = value; }
        public long DiscountCardId { get => discountCardId; set => discountCardId = value; }
        public double DiscountCardAccumulation { get => discountCardAccumulation; set => discountCardAccumulation = value; }
    }
}
