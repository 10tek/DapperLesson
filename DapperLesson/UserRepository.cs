using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Dapper;
using System.Data.Common;

namespace DapperLesson
{
    public class UserRepository
    {
        public void Add(User user)
        {
            var sql = "Insert into Users(@Id, @CreationDate, @Login, @AttemptCount);";
            
            using (var connection = new SqlConnection())
            {
                // обязательно нужна строка подключения
                var transaction = connection.BeginTransaction();
                var rowAffected = connection.Execute(sql, user, transaction);

                if (rowAffected != 1)
                {
                    throw new Exception("БАЛЯ");
                }
            }
        }

        public User GetUserById(Guid id)
        {
            var sql = "Select Top(1) from Users where Id=@Id;";

            using (var connection = new SqlConnection())
            {
                // обязательно нужна строка подключения
                return connection.QuerySingleOrDefault<User>(sql, new { Id = id });
            }
        }

    }
}
