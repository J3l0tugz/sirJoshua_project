using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamilots_POS.Models
{
    public class Log
    {
        public int Id { get; set; }
        public SqlMoney TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
