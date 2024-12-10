using Avalonia.Controls.Documents;
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
    public interface IEditProductService
    {
        bool UpdateProduct(int id, string name, string image, bool isBestSeller, int categoryId, SqlMoney price);
    }

    public class EditProductService : DatabaseConnection, IEditProductService
    {
        public bool UpdateProduct(int id, string name, string image, bool isBestSeller, int categoryId, SqlMoney price)
        {
            using(var conn = SqlConn())
            {
                conn.Open();
                string query = "update products set name=@name, is_best_seller=@isBestSeller, categories_id=categoryId, price=@price, updated_at=current_timestamp where id=@id;";
                using(var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
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
