using MySql.Data.MySqlClient;
using System.Collection.Generic;
using System.Data;
using System;

// Sistema de conexiones que recibe la IP del servidor y el puerto de conexión

namespace RPU5
{
    class Connection
    {
        public Connection(string IP) {

            string connectionString = "Server=" + IP + ";Port=" + "3306" + ";Database=testing;" +
                                      "Uid=picking; Pwd=FSN5hrUD8UFVAU8z;";

            try
            {
                conn.Open();
                conn.Close();
            }
            catch (Exception ex) {
                Console.Write("CONNECTION FAILED");
            }
        }

        public void push(string table, Dictionary<string, string> info) {

            // Construir string de peticion
            string petition = "INSERT INTO " + table + " ";

            string fields = string.Join(",", info.Keys);
            string values = string.Join(",", info.Values);

            petition += "(" + fields + ")" + " VALUES " + "(" + values + ")";

            // Ejecutar comando
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = petition;
            cmd.ExecuteNonQuery();
        }

        public DataRow pull(string table, string where_condition) {
            string petition = "SELECT * FROM " + table + " WHERE " + where_condition;
            try {
                MySqlDataAdapter da = new MySqlDataAdapter(petition, this.conn);
                DataSet ds = new DataSet();
                da.Fill(ds, table);
                DataTable dt = ds.Tables[table];
                return dt.Rows[0];
            }
            catch (Exception ex) {
                Console.Write("ELEMENT NOT FOUND");
            }
            return null;
        }

        public void update(string table, string field, string replace_condition) {
            string petition = "UPDATE " + table + " SET " + field + " WHERE " + replace_condition;
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = petition;
            cmd.ExecuteNonQuery();
        }

        public bool status() {
            return true;
        }
    }
}
