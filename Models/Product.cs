using System;

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
