using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;

namespace PingWebApi.Model
{
    public static class DatabaseCommand
    {
        private static string _connectionString = "Server=tcp:pinggame.database.windows.net,1433;Initial Catalog=ping;Persist Security Info=False;User ID=ping;Password=Database123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public static int ExecuteQuery(string queryString)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                try
                {
                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        int status = command.ExecuteNonQuery();

                        if (status == 0)
                        {
                            return 0;
                        }

                        return 1;     
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return 0;
                }
            }
        }

        public static List<Users> ReadUsers(string queryString)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
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
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataReader dataReader = command.ExecuteReader();

                List<UserScore> list = new List<UserScore>();

                foreach (var unused in dataReader)
                {
                    UserScore score = new UserScore((string)dataReader.GetValue(0), (int)dataReader.GetValue(1),(string)dataReader.GetValue(2));
                    list.Add(score);
                }
                return list;
            }
        }
    }
}