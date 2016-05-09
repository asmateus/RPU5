package com.example.rpu5;

import java.sql.Connection;
import java.sql.DriverManager;
//import java.sql.SQLException;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;

import android.text.TextUtils;

public class Conexion_Base {
	Connection conn;
    private String connection_string;
    private String database;
    private String user;
    private String pwd;
    private int state = -1;
    
	public void ConectionC(String IP, String port){
		this.state = 0;
        this.connection_string = "jdbc:mysql://" + IP + ":" + port;
	}
	
    public void Refactor(String database, String user, String pwd)
    {
        this.database = database;
        this.pwd = pwd;
        this.user = user;
    }
    
    public void Open(String database, String user, String pwd)
    {
        Refactor(database, user, pwd);
        Open();
    }
    
    public void Open()
    {
    	//System.out.println("CONNECTING...");
        try {
        	Class.forName("com.mysql.jdbc.Driver"); 
			this.conn =	DriverManager.getConnection(this.connection_string + "/" +  this.database,this.user, this.pwd);	
            this.state = 1;
            //System.out.println("CONNECTION OPEN");
        }
        catch (Exception ex)
        {
            //System.out.print("\n CONNECTION FAILED\n ");
            //System.out.println(ex.getMessage());
            //Close();
            this.state = -1;
        }
    }
    
    public void Close() 
    {
        try {
        	this.conn.close();
		} catch (SQLException e) {
			this.state = -1;
		}
    }
    
    public void push(String table, Map<String, String> info) 
    {

        String petition = "INSERT INTO " + table + " ";
        String values = "";
        int i = 0;
        List<String> keys = new ArrayList<String>(info.keySet());
        for(String key : keys)
        {
            info.put("'" +info.get(key)+ "'", key);
        }
        String fields = TextUtils.join(",",keys);
        for(String key : keys)
        {
        	if(i == 0){
        		values = "'"+info.get(key)+"'";
        	}else{
        		values = values + "," +"'"+info.get(key)+"'";
        	}
            i++;
        }
        petition += "(" + fields + ")" + " VALUES " + "(" + values + ")";
        System.out.println(petition);
        
        // Ejecutar comando
        try{
        	Statement st = conn.createStatement();
			//st.executeQuery(petition); 
			st.execute(petition);
        }
        catch(Exception e)
        {
        	 System.out.print("PUT FAILED ");
             System.out.println(e.getMessage());
        	this.state = -1;
        }
        finally
        {
        	keys.clear();
        	info.clear();
        	petition = "";
        }

    }
    
    public List<String> pull(String table, String where_condition)
    {
        String petition = "SELECT * FROM " + table + " WHERE " + where_condition;
        List<String> data = new ArrayList<String>();
        try {

            Statement st = conn.createStatement();
    		ResultSet reader = st.executeQuery(petition); 
        	reader.last();
            // Convertir resultado en datos legibles para el usuario
            while (reader.next())
            {
                for (int i = 0; i < reader.getMetaData().getColumnCount(); ++i)
                {
                    data.add(reader.getString(i));
                    System.out.println(reader.getString(i));
                }
            }
            reader.close();
        }
        catch (Exception ex) {
            //System.out.println("CONNECTION FAILED");
            //System.out.println(ex.getMessage());
            this.state = -1;
        }
        return data;
    }
    
//////////////////// Funciona ////////////////////////////////
    public List<String> pullRow(String table, String row)
    {
        List<String> row_list = new ArrayList<String>();
        String petition = "SELECT * FROM " + table;
        List<String> full_list = new ArrayList<String>();
        try
        {
            Statement st = conn.createStatement();
    		ResultSet reader = st.executeQuery(petition); 

            // Convertir resultado en datos legibles para el usuario
            // Identify row number
            int row_num = 0,col_num = reader.getMetaData().getColumnCount(); boolean ishere = false;
            for (int i = 1; i <= col_num; ++i) {
                if (reader.getMetaData().getColumnName(i).equals(row)) {
                    row_num = i;
                    ishere = true;
                    System.out.println(reader.getMetaData().getColumnName(i));
                }
            }
            if (ishere == false)
            {
                return null;
            }
            else
            {
                while (reader.next())
                {
                    for (int i = 1; i <= reader.getMetaData().getColumnCount(); ++i)
                    {
                        full_list.add(reader.getString(i));
                        
                    }
                }
                int count = reader.getMetaData().getColumnCount();
                reader.close();
                for (int i = row_num-1; i < full_list.size(); i += count){
                    row_list.add(full_list.get(i));
                }
            }
        }
        catch (Exception ex)
        {
            //System.out.println("CONNECTION FAILED");
            //System.out.println(ex.getMessage());
            this.state = -1;
        }
        return row_list;
    }
//////////////////// Funciona ////////////////////////////////    
    
    public void update(String table, String replace_condition, String field)
    {
    	try {
    		String petition = "UPDATE " + table + " SET " + field + " WHERE " + replace_condition;
    		Statement st;
			st = conn.createStatement();
			st.executeQuery(petition);
		} catch (SQLException e) {
			e.printStackTrace();
		} 
    }

    public int status()
    {
        return this.state;
    }
//////////////////////////////////////////////////////////////
}
