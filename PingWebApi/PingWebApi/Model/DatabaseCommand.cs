using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PingWebApi.Model
{
    public static class DatabaseCommand
    {
        private static string ConnectionString = "Server=tcp:pinggame.database.windows.net,1433;Initial Catalog=ping;Persist Security Info=False;User ID=ping;Password=Database123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public static void ExecuteQuery(string queryString)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                try
                {
                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        public static List<Users> ReadUsers(string queryString)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataReader dataReader = command.ExecuteReader();

                List<Users> list = new List<Users>();

                foreach (var unused in dataReader)
                {
                    Users user = new Users((string)dataReader.GetValue(0), (string)dataReader.GetValue(1));
                    list.Add(user);
                }
                return list;
            }
        }

        public static List<UserScore> ReadScore(string queryString)
         {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataReader dataReader = command.ExecuteReader();

                List<UserScore> list = new List<UserScore>();

                foreach (var unused in dataReader)
                {
                    UserScore score = new UserScore((string)dataReader.GetValue(0), (int)dataReader.GetValue(1));
                    list.Add(score);
                }
                return list;
            }
        }
    }
}