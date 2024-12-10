using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamilots_POS.Models
{
    public class TransactionProduct
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }
}
  