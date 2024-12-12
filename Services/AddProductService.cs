using Microsoft.Data.SqlClient;
using project_open;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamilots_POS.Services
{
    public interface IAddProductService
    {
        bool AddProduct(string name, string image, bool isBestSeller, int categoryId, SqlMoney price);
    }

    public class AddProductService : DatabaseConnection, IAddProductService
    {
        public bool AddProduct(string name, string image, bool isBestSeller, int categoryId, SqlMoney price)
        {
            using (var conn = SqlConn())
            {
                conn.Open();
                string query = "insert into products(name, is_best_seller, categories_id, price)" +
                    "\n values(@name, @isBestSeller, @categoryId, @price);";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("name", name);
                    cmd.Parameters.AddWithValue("isBestSeller", isBestSeller);
                    cmd.Parameters.AddWithValue("categoryId", categoryId);
                    cmd.Parameters.AddWithValue("price", price);

                    return cmd.ExecuteNonQuery() == 1;
                }
            }
        }
    }
}
