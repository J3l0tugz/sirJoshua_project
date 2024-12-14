using HarfBuzzSharp;
using Mamilots_POS.Models;
using Microsoft.Data.SqlClient;
using project_open;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
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
        int GetHighestId();
        Product GetProduct(int Id);

    }

    public class ProductRepository : DatabaseConnection, IProductRepository
    {
        public async IAsyncEnumerable<Product> GetProductsAsync()
        {
            using (var conn = SqlConn())
            {
                conn.Open();
                string query = "select p.name,p.is_best_seller,p.categories_id,p.price, p.id from products AS p where p.is_deleted=0;";
                using (var cmd = new SqlCommand(query, conn))
                {
                    using (var reader = await cmd.ExecuteReaderAsync(System.Data.CommandBehavior.CloseConnection))
                    {
                        while (await reader.ReadAsync())
                        {
                            yield return new Product()
                            {
                                Name = reader.GetString(0),
                                IsBestSeller = (bool)reader.GetBoolean(1),
                                CategoryId = reader.GetInt32(2),
                                Price = reader.GetSqlMoney(3),
                                Id = reader.GetInt32(4),
                            };
                        }
                    }
                }
            }
        }


        public int GetHighestId()
        {
            using (var conn = SqlConn())
            {
                conn.Open();
                string query = "select max(id) as Id from products;";
                using (var cmd = new SqlCommand(query,conn))
                {
                    using (var reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        if(reader.Read())
                        {
                            return reader.GetInt32(0);
                        }
                        return -1;
                    }
                }

            }
        }

        public Product GetProduct(int Id)
        {
            using (var conn = SqlConn())
            {
                conn.Open();
                string query = "select p.name,p.is_best_seller,p.categories_id,p.price, p.id from products AS p where p.id = @id;";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("id", Id);
                    using (var reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        if(reader.Read())
                        {
                            return new Product()
                            {
                                Name = reader.GetString(0),
                                IsBestSeller = (bool)reader.GetBoolean(1),
                                CategoryId = reader.GetInt32(2),
                                Price = reader.GetSqlMoney(3),
                                Id = reader.GetInt32(4),
                            };
                        }
                        return null;
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
