using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoe_Store_DB.Classes
{
    internal class Supplier
    {
        int id;
        string name;
        long edrpou_Code;
        long firstPhoneNumber;
        long? secondPhoneNumber;
        string email;
        string address;
        long currentAccount;

        public Supplier(int id, string name, long edrpou_Code, long firstPhoneNumber, long? secondPhoneNumber, string email, string address, long currentAccount)
        {
            Id = id;
            Name = name;
            Edrpou_Code = edrpou_Code;
            FirstPhoneNumber = firstPhoneNumber;
            SecondPhoneNumber = secondPhoneNumber;
            Email = email;
            Address = address;
            CurrentAccount = currentAccount;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public long Edrpou_Code { get => edrpou_Code; set => edrpou_Code = value; }
        public long FirstPhoneNumber { get => firstPhoneNumber; set => firstPhoneNumber = value; }
        public long? SecondPhoneNumber { get => secondPhoneNumber; set => secondPhoneNumber = value; }
        public string Email { get => email; set => email = value; }
        public string Address { get => address; set => address = value; }
        public long CurrentAccount { get => currentAccount; set => currentAccount = value; }
    }
}
