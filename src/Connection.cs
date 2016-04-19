using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System;

// Sistema de conexiones que recibe la IP del servidor y el puerto de conexión

namespace RPU5
{
    class Connection
    {
        MySqlConnection conn;
        public Connection(string IP, int port) {

            string connectionString = "Server=" + IP + ";Port=" + port + ";Database=rpu5;" +
                                      "Uid=picking; Pwd=PiCkInG;";
            

            //try
           // {
                conn = new MySqlConnection(connectionString);
                conn.Open();
                Dictionary<string, string> temp = new Dictionary<string, string> ();
                temp.Add("nombre", "Shinobu");
                temp.Add("edad", "51");
                temp.Add("sexo", "F");
                //push("test", temp);
                pull("test", "nombre='simancarts'");
                //update("test", "nombre='Shinobu'", "nombre='German'");
                conn.Close();
           // }
            //catch (Exception ex) {
                //Console.Write("CONNECTION FAILED");
            //}
        }

        public void push(string table, Dictionary<string, string> info) {

            // Construir string de peticion
            string petition = "INSERT INTO " + table + " ";
            List<string> keys = new List<string>(info.Keys);
            foreach(string key in keys)
            {
                info[key] = "'" + info[key] + "'"; 
            }
            string fields = string.Join(",", info.Keys);
            string values = string.Join(",", info.Values);

            petition += "(" + fields + ")" + " VALUES " + "(" + values + ")";
            // Ejecutar comando
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = petition;
            cmd.ExecuteNonQuery();
        }

        public void pull(string table, string where_condition) {
            string petition = "SELECT * FROM " + table + " WHERE " + where_condition;

            try {
                MySqlCommand cmd = new MySqlCommand(petition, this.conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                List<string> data = new List<string>(); int i = 0;
                while (reader.Read()) {
                    data.Add(reader.GetString(i));
                    ++i;
                }
                Console.Write(data[0] + " " +  data[1]);
            }
            catch (Exception ex) {
                Console.Write("ELEMENT NOT FOUND");
            }

            //return null;
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
