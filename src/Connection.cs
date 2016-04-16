using MySql.Data.MySqlClient;
//using System.Data.SqlClient;
using System.Data;
using System;

// Sistema de conexiones que recibe la IP del servidor y el puerto de conexión

namespace RPU5
{
    class Connection
    {
        public Connection(string IP, int port) {

            string connectionString = "Server=" + IP + ";Port=" + port + ";Database=testing;" +
                                      "Uid=picking; Pwd=FSN5hrUD8UFVAU8z;";
            string sql_petition = "INSERT INTO tabla_1 (nombre, edad, sexo) VALUES ('Juanito', '23', 'M')";
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql_petition;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex) {
                Console.Write("CONNECTION FAILED");
            }
        }

        public bool status() {
            return true;
        }
    }
}
