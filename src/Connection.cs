﻿using MySql.Data.MySqlClient;
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

        public void Open(string database, string user, string pwd)
        {
            Refactor(database, user, pwd);

            Open();
        }

        public void Refactor(string database, string user, string pwd)
        {
            this.database = database;
            this.pwd = pwd;
            this.user = user;
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
            foreach(string key in keys)
            {
                info[key] = "'" + info[key] + "'"; 
            }
            string fields = string.Join(",", info.Keys);
            string values = string.Join(",", info.Values);

            petition += "(" + fields + ")" + " VALUES " + "(" + values + ")";
            // Ejecutar comando
            MySqlCommand cmd = this.conn.CreateCommand();
            cmd.CommandText = petition;
            cmd.ExecuteNonQuery();
        }

        public void pull(string table, string where_condition)
        {
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
                Console.Write("ELEMENT NOT FOUND\n" + ex.Message);
                this.state = -1;
            }
        }

        public void update(string table, string field, string replace_condition)
        {
            string petition = "UPDATE " + table + " SET " + field + " WHERE " + replace_condition;
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = petition;
            cmd.ExecuteNonQuery();
        }

        public int status()
        {
            return this.state;
        }
    }
}
