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
        private string connection_string;
        private string database;
        private string user;
        private string pwd;
        private int state;

        public Connection(string IP, int port)
        {
            this.state = 0;
            this.connection_string = "Server=" + IP + ";Port=" + port + ";";
        }

        public void Refactor(string database, string user, string pwd)
                {
                    this.database = database;
                    this.pwd = pwd;
                    this.user = user;
                }

        public void Open(string database, string user, string pwd)
        {
            Refactor(database, user, pwd);

            Open();
        }

        public void Open()
        {
            try {
                this.conn = new MySqlConnection(
                                           this.connection_string + "Database=" + this.database + ";Uid=" +
                                           this.user + ";Pwd=" + this.pwd + ";"
                                           );
                this.conn.Open();
                this.state = 1;
            }
            catch (Exception ex)
            {
                Console.Write("\n CONNECTION FAILED\n " + ex.Message);
                this.state = -1;
            }
        }

        public void Close()
        {
            this.conn.Close();
        }

        public void push(string table, Dictionary<string, string> info)
        {

            // Construir string de peticion
            string petition = "INSERT INTO " + table + " ";
            List<string> keys = new List<string>(info.Keys);
            List<string> vals = new List<string>();
            foreach (string key in keys)
            {
                vals.Add("'" + info[key] + "'");
            }
            string fields = string.Join(",", info.Keys);
            string values = string.Join(",", vals);

            petition += "(" + fields + ")" + " VALUES " + "(" + values + ")";
            // Ejecutar comando
            MySqlCommand cmd = this.conn.CreateCommand();
            cmd.CommandText = petition;
            cmd.ExecuteNonQuery();
        }

        public List<string> pull(string table, string where_condition)
        {
            string petition = "SELECT * FROM " + table + " WHERE " + where_condition;
            List<string> data = new List<string>();
            try {
                MySqlCommand cmd = new MySqlCommand(petition, this.conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                // Convertir resultado en datos legibles para el usuario
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; ++i)
                    {
                        data.Add(reader.GetString(i));
                    }
                }
                reader.Close();
            }
            catch (Exception ex) {
                Console.Write("ELEMENT NOT FOUND\n" + ex.Message);
                this.state = -1;
            }
            return data;
        }

        public List<string> pullRow(string table, string row)
        {
            List<string> row_list = new List<string>();
            string petition = "SELECT * FROM " + table;
            List<string> full_list = new List<string>();
            try
            {
                MySqlCommand cmd = new MySqlCommand(petition, this.conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                // Convertir resultado en datos legibles para el usuario
                // Identify row number
                int row_num = 0; bool ishere = false;
                for (int i = 0; i < reader.FieldCount; ++i) {
                    if (row == reader.GetName(i)) {
                        row_num = i;
                        ishere = true;
                    }
                }
                if (ishere == false)
                {
                    return null;
                }
                else
                {
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; ++i)
                        {
                            full_list.Add(reader.GetString(i));
                        }
                    }
                    int count = reader.FieldCount;
                    reader.Close();
                    for (int i = row_num; i < full_list.Count; i += count)
                        row_list.Add(full_list[i]);
                }
            }
            catch (Exception ex)
            {
                Console.Write("ELEMENT NOT FOUND\n" + ex.Message);
                this.state = -1;
            }
            return row_list;
        }

        public void update(string table, string replace_condition, string field)
        {
            string petition = "UPDATE " + table + " SET " + field + " WHERE " + replace_condition;
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = petition;
            cmd.ExecuteNonQuery();
        }

        public void truncate(string table)
        {

            string petition = "TRUNCATE TABLE " + table;
            MySqlCommand cmd = conn.CreateCommand();
            try
            {
                cmd.CommandText = petition;
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }

        public List<string> pullse(string table, string columna1, string where_condition)
        {
            string petition = "SELECT " + columna1 + " FROM " + table + " WHERE " + where_condition;
            List<string> data = new List<string>();
            try
            {
                MySqlCommand cmd = new MySqlCommand(petition, this.conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                // Convertir resultado en datos legibles para el usuario
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; ++i)
                    {
                        data.Add(reader.GetString(i));
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.Write("ELEMENT NOT FOUND\n" + ex.Message);
                this.state = -1;
            }
            return data;
        }

        public void delete(string table, string condition)
        {
            string petition = "DELETE FROM " + table + " WHERE " + condition;
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = petition;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.Write("ELEMENT NOT FOUND\n" + ex.Message);
                this.state = -1;
            }
        }

        public List<string> pullAll(string table)
        {
            string petition = "SELECT * FROM " + table;
            List<string> data = new List<string>();
            try
            {
                MySqlCommand cmd = new MySqlCommand(petition, this.conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                // Convertir resultado en datos legibles para el usuario
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; ++i)
                    {
                        data.Add(reader.GetString(i));
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.Write("ELEMENT NOT FOUND\n" + ex.Message);
                this.state = -1;
            }
            return data;
        }

        public int status()
        {
            return this.state;
        }
    }
}
