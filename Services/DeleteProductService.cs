using Microsoft.Data.SqlClient;
using project_open;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamilots_POS.Services
{
    public interface IDeleteProductService
    {
        bool DeleteProduct(int id);
    }

    public class DeleteProductService : DatabaseConnection, IDeleteProductService
    {
        public bool DeleteProduct(int id)
        {
            using (var conn = SqlConn())
            {
                conn.Open();
                string query = "update product set is_deleted=1 where id=@id";
                using(var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("id", id);

                    return cmd.ExecuteNonQuery() == 1;
                }
            }
        }
    }
}
