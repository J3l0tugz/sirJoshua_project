using Mamilots_POS.Models;
using Microsoft.Data.SqlClient;
using project_open;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Mamilots_POS.Repositories
{
    public interface ITransactionRepository
    {
        IAsyncEnumerable<Log> GetTransactionsAsync();
    }

    public class TransactionRepository : DatabaseConnection, ITransactionRepository
    {
        public async IAsyncEnumerable<Log> GetTransactionsAsync()
        {
            using (var conn = SqlConn())
            {
                conn.Open();
                string query = "select * from transactions ordey by created_at desc;";
                using(var cmd = new SqlCommand(query, conn))
                {
                    using(var reader = await cmd.ExecuteReaderAsync(System.Data.CommandBehavior.CloseConnection))
                    {
                        while(await reader.ReadAsync())
                        {
                            yield return new Log
                            {
                                Id = reader.GetInt32(0),
                                TotalPrice = reader.GetSqlMoney(1),
                                CreatedAt = reader.GetDateTime(2),
                            };
                        }
                    }
                }
            }
        }
    }
}
