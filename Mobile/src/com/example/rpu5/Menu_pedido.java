package com.example.rpu5;


import java.util.HashMap;
import java.util.Map;

import android.app.Activity;
import android.content.Context;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;


public class Menu_pedido extends Activity {
	Button Regreso,Pedido;
	String ipServidorMySQL, contrasenaMySQL, usuarioMySQL, puertoMySQL;
	TextView Dinosaurio,Carro,Avion; 
	Map<String, String> Directorio = new HashMap<String,String>();
	int x = -1,EnPed = 0;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.menu_pedido);
		
		Regreso = (Button)findViewById(R.id.button_cP);     
		Pedido  = (Button)findViewById(R.id.button);
		Dinosaurio = (TextView)findViewById(R.id.editText);
		Carro = (TextView)findViewById(R.id.editText2);
		Avion = (TextView)findViewById(R.id.editText3);
		
		
		Pedido.setOnClickListener(new View.OnClickListener(){
			public void onClick(View V){
				if(EnPed == 0){
					Toast.makeText(getApplicationContext(),"Generando pedido",Toast.LENGTH_SHORT).show();
					EnPed = 1;
					Directorio.put("Dinosaurio", Dinosaurio.getText().toString());
					Directorio.put("Carro", Carro.getText().toString());
					Directorio.put("Avion", Avion.getText().toString());
					new PushtoDB().execute();
				}else{
					Toast.makeText(getApplicationContext(),"Pedido en proceso",Toast.LENGTH_SHORT).show();
				}
			}
		});
		
		Regreso.setOnClickListener(new View.OnClickListener() {
			public void onClick(View v) {
				finish();
			}
		});
	}
    private class PushtoDB extends AsyncTask<Void, Void, Void>{
    	@Override
    	protected Void doInBackground(Void... arg0){
    		try{
    			cargarConfiguracion();
    			Conexion_Base MySQLOj = new Conexion_Base();
    			MySQLOj.ConectionC(ipServidorMySQL,puertoMySQL );
    			MySQLOj.Open("Mobile", usuarioMySQL, contrasenaMySQL);
    			x = MySQLOj.status();
    			if(x == 1){
    				MySQLOj.push("pedidos_movil",Directorio);
    			}
    			MySQLOj.Close(); 	
    			EnPed = 0;
    		}
    		catch(Exception e)
    		{
    			x = -1;
    		}
    		return null;
    	}
    	
    	@Override
    	protected void onPostExecute(Void results){
    		if(x == 1){
    			Toast.makeText(getApplicationContext(),"Pedido Completado",Toast.LENGTH_SHORT).show();
    		}else{
    			Toast.makeText(getApplicationContext(),"Error",Toast.LENGTH_SHORT).show();
    		}
    		super.onPostExecute(results);
    		super.onPostExecute(results);
    	}
    }
    
    
    public void cargarConfiguracion()
    {
    	//leemos los valores de conexión al servidor
    	//MySQL desde SharedPreferences
    	SharedPreferences prefs = 
        getSharedPreferences("AjpdSoftMySQL", Context.MODE_PRIVATE);

        ipServidorMySQL = prefs.getString("Conexión", "10.20.21.106");
        contrasenaMySQL = prefs.getString("Contraseña", "kenshinn1");        
        puertoMySQL =  Integer.toString(prefs.getInt("Puerto", 3306)); 
        usuarioMySQL = prefs.getString("Usuario", "Mobile");        
    }    

}
