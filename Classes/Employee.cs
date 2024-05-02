using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoe_Store_DB.Classes
{
    class Employee
    {
        int id;
        string name;
        string position;
        long phoneNumber;
        string email;
        string address;
        string gender;
        DateOnly dateOfBirth;

        public Employee(int id, string name, string position, long phoneNumber, string email, string address, string gender, DateOnly dateOfBirth)
        {
            Id = id;
            Name = name;
            Position = position;
            PhoneNumber = phoneNumber;
            Email = email;
            Address = address;
            Gender = gender;
            DateOfBirth = dateOfBirth;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Position { get => position; set => position = value; }
        public long PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string Email { get => email; set => email = value; }
        public string Address { get => address; set => address = value; }
        public string Gender { get => gender; set => gender = value; }
        public DateOnly DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }
    }
}
