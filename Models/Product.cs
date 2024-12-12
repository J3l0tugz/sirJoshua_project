
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamilots_POS.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image {  get; set; }
        public bool IsBestSeller { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public SqlMoney Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
