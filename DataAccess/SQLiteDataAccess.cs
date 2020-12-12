using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace DataAccess
{
    public class SQLiteDataAccess
    {
        SqliteConnectionStringBuilder ConnStringBuilder = new SqliteConnectionStringBuilder();

        public SQLiteDataAccess()
        {
            ConnStringBuilder.DataSource = "./SQLite_CRUD.db";
        }


        public void InsertUser()
        {
            using (var connection = new SqliteConnection(ConnStringBuilder.ConnectionString))
            {

                connection.Open();

                var delTableCmd = connection.CreateCommand();
                delTableCmd.CommandText = "DROP TABLE IF EXISTS user";
                delTableCmd.ExecuteNonQuery();

                var createTable = connection.CreateCommand();
                createTable.CommandText = "CREATE TABLE user (ID INT, FirstName VARCHAR(100), LastName VARCHAR(100), UserType VARCHAR(100))";
                createTable.ExecuteNonQuery();

                //seed some data
                using (var transaction = connection.BeginTransaction())
                {
                    var insertCmd = connection.CreateCommand();

                    insertCmd.CommandText = "INSERT INTO user VALUES(1, 'RAM', 'Kumar', 'User')";
                    insertCmd.ExecuteNonQuery();

                    insertCmd.CommandText = "INSERT INTO user VALUES(1, 'Shyam', 'Sharma', 'User')";
                    insertCmd.ExecuteNonQuery();

                    insertCmd.CommandText = "INSERT INTO user VALUES(1, 'Jay', 'Vermar', 'Driver')";
                    insertCmd.ExecuteNonQuery();

                    transaction.Commit();

                }
            }
        }

        public List<User> GetUser()
        {
            List<User> users = new List<User>();
            using (var connection = new SqliteConnection(ConnStringBuilder.ConnectionString))
            {
                connection.Open();

                var selectCmd = connection.CreateCommand();
                selectCmd.CommandText = "Select Id, FirstName, LastName, UserType FROM user";

                using (var reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User()
                        {
                            ID = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            UserType = reader.GetString(3)

                        });
                    }
                }
            }
            return users;

        }
    }
}
