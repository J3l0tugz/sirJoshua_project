using Mamilots_POS.Models;
using Microsoft.Data.SqlClient;
using project_open;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Mamilots_POS.Repositories
{
    public interface IProductRepository
    {
        IAsyncEnumerable<Product> GetProductsAsync();
        IAsyncEnumerable<TransactionProduct> GetTransactionProductsAsync(int TransactionId);

    }

    public class ProductRepository : DatabaseConnection, IProductRepository
    {
        public async IAsyncEnumerable<Product> GetProductsAsync()
        {
            using (var conn = SqlConn())
            {
                conn.Open();
                string query = "select * from products join categories As c on c.id = products.categories_id where is_deleted=0;";
                using (var cmd = new SqlCommand(query, conn))
                {
                    using (var reader = await cmd.ExecuteReaderAsync(System.Data.CommandBehavior.CloseConnection))
                    {
                        while (await reader.ReadAsync())
                        {
                            yield return new Product
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Image = reader.GetString(2),
                                IsBestSeller = (bool)reader.GetBoolean(3),
                                Category = new Category()
                                {
                                    Id = reader.GetInt32(4),
                                    Name = reader.GetString(11),
                                },
                                Price = reader.GetInt32(5),
                                CreatedAt = reader.GetDateTime(6),
                                UpdatedAt = reader.IsDBNull(7) ? null : reader.GetDateTime(7),
                            };
                        }
                    }
                }
            }
        }

        public Product GetProduct(int Id)
        {
            using (var conn = SqlConn())
            {
                conn.Open();
                string query = "Select * from products join where id = @Id and is_delete=0";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("id", Id);
                    using (var reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        return new Product
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Image = reader.GetString(2),
                            IsBestSeller = (bool)reader.GetBoolean(3),
                            Category = new Category
                            {
                                Id = reader.GetInt32(4),
                                Name = reader.GetString(11),
                            },
                            Price = reader.GetInt32(5),
                            CreatedAt = reader.GetDateTime(6),
                        };
                    }
                }
            }
        }

        public async IAsyncEnumerable<TransactionProduct> GetTransactionProductsAsync(int TransactionId)
        {
            using (var conn = SqlConn())
            {
                conn.Open();
                string query = "select name,price,quantity from transaction_products as TP" +
                    "\n join products as P on P.id = TP.product_id" +
                    "\n where log_id = @TransactionId;";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("TransactioId", TransactionId);

                    using (var reader = await cmd.ExecuteReaderAsync(System.Data.CommandBehavior.CloseConnection))
                    {
                        while (await reader.ReadAsync())
                        {
                            yield return new TransactionProduct
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Quantity = reader.GetInt32(2),
                            };
                        }
                    }
                }
            }
        }
    }
}
