package com.example.rpu5;

import android.app.Activity;
import android.content.Context;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

public class Menu_configuracion extends Activity {
	TextView textIP, textPuerto, textContrasena, textUsuario;
	String conexionMySQLURL;
	private Button buttonProbarConexion,Regreso,Guardar,Cargar; 
	int x = -1;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.menu_configuracion);
		
		textIP               = (TextView)findViewById(R.id.txtIP);      
        textPuerto           = (TextView)findViewById(R.id.txtPuerto);
        textContrasena       = (TextView)findViewById(R.id.txtContrasena);
        textUsuario          = (TextView)findViewById(R.id.txtUsuario);
        buttonProbarConexion = (Button)findViewById(R.id.btProbarConexion);
		Regreso              = (Button)findViewById(R.id.button_cC);  
		Cargar               = (Button)findViewById(R.id.BCargar);
		Guardar              = (Button)findViewById(R.id.BGuardar);
		
		
		buttonProbarConexion.setOnClickListener(new View.OnClickListener() 
        {
          public void onClick(View v) 
          {
        	  Toast.makeText(getApplicationContext(),"Conectando con Base",Toast.LENGTH_SHORT).show();
        	  new MyTask().execute();
          }
        });    
		
		Cargar.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				cargarConfiguracion();
				
			}
		});
		
		Guardar.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				guardarConfiguracion();
				
			}
		});
		
		Regreso.setOnClickListener(new View.OnClickListener() {
			public void onClick(View v) {
				finish();
			}
		});
	}
		
    private class MyTask extends AsyncTask<Void, Void, Void>{
    	@Override
    	protected Void doInBackground(Void... arg0){
    		try{
    			Conexion_Base MySQLOj = new Conexion_Base();
    			MySQLOj.ConectionC(textIP.getText().toString(), textPuerto.getText().toString());
    			MySQLOj.Open("Mobile", textUsuario.getText().toString(), textContrasena.getText().toString());
    			//MySQLOj.ConectionC("10.20.21.106", "3306");
    			//MySQLOj.Open("Mobile", "Mobile", "kenshinn1");
    			x = MySQLOj.status();
    			MySQLOj.Close();
    		}catch(Exception e){
    			x = -1;
    		}
    		return null;
    	}
    	
    	@Override
    	protected void onPostExecute(Void results){
    		if(x == 1){
    			Toast.makeText(getApplicationContext(),"Conectado",Toast.LENGTH_SHORT).show();
    		}else{
    			Toast.makeText(getApplicationContext(),"No conectado",Toast.LENGTH_SHORT).show();
    		}
    		Toast.makeText(getApplicationContext(),"Fin",Toast.LENGTH_SHORT).show();
    		super.onPostExecute(results);
    	}
    }
    
    public void guardarConfiguracion()
    {
    	try
    	{
    		SharedPreferences prefs = 
    			getSharedPreferences("AjpdSoftMySQL", Context.MODE_PRIVATE);
    		SharedPreferences.Editor editor = prefs.edit();
    		editor.putString("Conexión", textIP.getText().toString());
    		editor.putString("Contraseña", textContrasena.getText().toString());
    		int puerto = 3306;
  			puerto = Integer.valueOf(textPuerto.getText().toString());    			
    		editor.putInt("Puerto", puerto);
    		editor.putString("Usuario", textUsuario.getText().toString());      
    		editor.commit();
    		if(x == 1){
    			Toast.makeText(getApplicationContext(),"Conectado",Toast.LENGTH_SHORT).show();
    		}else{
    			Toast.makeText(getApplicationContext(),"No conectado",Toast.LENGTH_SHORT).show();
    		}
    	}
        catch (Exception e)
        {  
      	  Toast.makeText(getApplicationContext(),
                  "Error: " + e.getMessage(),
                  Toast.LENGTH_LONG).show();
        }    	
    }
    
    public void cargarConfiguracion()
    {
    	try
    	{    	
    		SharedPreferences prefs = 
        		getSharedPreferences("AjpdSoftMySQL", Context.MODE_PRIVATE);
        	textIP.setText(prefs.getString("Conexión", "10.20.21.79"));
        	textContrasena.setText(prefs.getString("Contraseña", "kenshinn1"));
    		int puerto = 3306;
   			puerto = prefs.getInt("Puerto", 3306);
        	textPuerto.setText(Integer.toString(puerto));        
        	textUsuario.setText(prefs.getString("Usuario", "Mobile"));    
    	}
        catch (Exception e) 
        {  
      	  Toast.makeText(getApplicationContext(),
                  "Error: " + e.getMessage(),
                  Toast.LENGTH_LONG).show();
        }    
    }
    
    public void onDestroy()
    {
      super.onDestroy();
      guardarConfiguracion();
    }
    
    @Override
    protected void onStart() 
    {
      super.onStart();
      cargarConfiguracion();
    }   
}