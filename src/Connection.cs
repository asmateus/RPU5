using System.Data.SqlClient;
using System.Data;
using System;

// Sistema de conexiones que recibe la IP del servidor y el puerto de conexión

namespace RPU5
{
    class Connection
    {
        public Connection(string IP, int port) {

            string connectionString = "Data Source=" + IP + ",3306" + ";Initial Catalog=testing;" +
                                      "User ID=picking; Password=FSN5hrUD8UFVAU8z";
            string sql_petition = "INSERT INTO tabla_1 Data (nombre, edad, sexo) VALUES (@nombre, @edad, @sexo)";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql_petition);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = connection;
            cmd.Parameters.AddWithValue("@nombre", "Osamu");
            cmd.Parameters.AddWithValue("@edad", "23");
            cmd.Parameters.AddWithValue("@sexo", "M");
            try
            {
                Console.Write("\n TRY CONNECTION\n");
                connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { Console.Write(ex + "\n CONNECTION FAILED.\n"); }
        }

        public bool status() {
            return true;
        }
    }
}
