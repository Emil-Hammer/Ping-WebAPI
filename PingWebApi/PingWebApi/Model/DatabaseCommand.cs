using System;
using System.Collections;
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

        public static List<string> ReadQuery(string queryString)
        {
            string output = "";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataReader dataReader = command.ExecuteReader();

                List<string> list = new List<string>();

                while (dataReader.Read())
                {
                    list.Add(output + dataReader.GetValue(0) + " - " + dataReader.GetValue(1) + "\n");
                }

                return list;
            }
        }
    }
}