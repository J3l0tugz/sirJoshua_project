﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamilots_POS.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ushort IsBestSeller { get; set; }
        public int CategoryId { get; set; }
        public float Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; private set; }
    }
}
