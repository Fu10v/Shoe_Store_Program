﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoe_Store_DB.Classes
{
    internal class Color
    {
        private int id;
        private string name;

        public Color()
        {

        }
        public Color(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
    }
}