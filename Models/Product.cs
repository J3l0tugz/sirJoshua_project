
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamilots_POS.Models
{
    public class Product : ObservableObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image {  get; set; }
        public bool IsBestSeller { get; set; }
        public int CategoryId { get; set; }
        public SqlMoney Price { get; set; }
        public SqlDateTime? CreatedAt { get; set; }
        public SqlDateTime? UpdatedAt { get; set; }
    }
}
