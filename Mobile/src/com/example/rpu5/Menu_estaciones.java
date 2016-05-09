package com.example.rpu5;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

import android.content.Context;
import android.content.SharedPreferences;

import android.app.Activity;
import android.os.AsyncTask;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemSelectedListener;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.Spinner;
import android.widget.TextView;


public class Menu_estaciones extends Activity implements OnItemSelectedListener {
	int x = -1,Pos;
	Button Regreso;
	Spinner Lista;
	TextView Operario,Codigo,Autorizado,textIP, textPuerto, textContrasena, textUsuario;
	String ipServidorMySQL, contrasenaMySQL, usuarioMySQL, puertoMySQL;
    String[] Estaciones = {"Estacion 1","Estacion 2","Estacion 3","Estacion 4","Estacion 5","Estacion 6","Estacion 7"};
    List<String> Operarios1   = new ArrayList<String>(Arrays.asList("NULL",",",",",",",",",",",",",","));
    List<String> Codigos      = new ArrayList<String>(Arrays.asList("NULL",",",",",",",",",",",",",","));
    List<String> Autorizacion = new ArrayList<String>(Arrays.asList("NULL",",",",",",",",",",",",",","));
	String[] Operarios = {"1","2","3","4","5","6","7","8"};
	Menu_configuracion config = new Menu_configuracion();
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		
		super.onCreate(savedInstanceState);
		setContentView(R.layout.menu_estaciones);
		
		textIP         = (TextView)findViewById(R.id.txtIP        );      
        textPuerto     = (TextView)findViewById(R.id.txtPuerto    );
        textContrasena = (TextView)findViewById(R.id.txtContrasena);
        textUsuario    = (TextView)findViewById(R.id.txtUsuario   );
		Operario       = (TextView)findViewById(R.id.TxOperario   );
		Codigo         = (TextView)findViewById(R.id.TxCodigo     );
		Autorizado     = (TextView)findViewById(R.id.TxAutorizado );
		Regreso        = (Button  )findViewById(R.id.button_cE    ); 
		Lista          = (Spinner )findViewById(R.id.Lista        );
		
		ArrayAdapter<String> adaptador = new ArrayAdapter<String>(Menu_estaciones.this,android.R.layout.simple_spinner_item,Estaciones);
        Lista.setAdapter(adaptador);
        Lista.setOnItemSelectedListener(this);
        new Coneccion().execute();
        
		Regreso.setOnClickListener(new View.OnClickListener() {
			public void onClick(View v) {
				finish();
			}
		});
	}


    private class Coneccion extends AsyncTask<Void, Void, Void>{
    	@Override
    	protected Void doInBackground(Void... arg0){
    		try{
    			cargarConfiguracion();
    			Conexion_Base MySQLOj = new Conexion_Base();
    			MySQLOj.ConectionC(ipServidorMySQL,puertoMySQL );
    			MySQLOj.Open("RPU5", usuarioMySQL, contrasenaMySQL);
    			x = MySQLOj.status();
    			if(x == 1){
    				Operarios1   = MySQLOj.pullRow("operarios", "Nombre");
    				Codigos      = MySQLOj.pullRow("operarios", "Codigo");
    				Autorizacion = MySQLOj.pullRow("operarios", "Autorizado");
    			}
    			MySQLOj.Close(); 	
    		}
    		catch(Exception e)
    		{
    			x = -1;
    		}
    		return null;
    	}
    	
    	@Override
    	protected void onPostExecute(Void results){
    		Pos = Lista.getSelectedItemPosition(); 
    		Operario.setText(Operarios1.get(Pos));
    		Codigo.setText(Codigos.get(Pos));
    		Autorizado.setText(Autorizacion.get(Pos));
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
	
	@Override
	public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
		Pos = Lista.getSelectedItemPosition(); 
		switch(Pos){
			case 0:Operario.setText(Operarios1.get(0));Codigo.setText(Codigos.get(0));Autorizado.setText(Autorizacion.get(0));break;
			case 1:Operario.setText(Operarios1.get(1));Codigo.setText(Codigos.get(1));Autorizado.setText(Autorizacion.get(1));break;
			case 2:Operario.setText(Operarios1.get(2));Codigo.setText(Codigos.get(2));Autorizado.setText(Autorizacion.get(2));break;
			case 3:Operario.setText(Operarios1.get(3));Codigo.setText(Codigos.get(3));Autorizado.setText(Autorizacion.get(3));break;
			case 4:Operario.setText(Operarios1.get(4));Codigo.setText(Codigos.get(4));Autorizado.setText(Autorizacion.get(4));break;
			case 5:Operario.setText(Operarios1.get(5));Codigo.setText(Codigos.get(5));Autorizado.setText(Autorizacion.get(5));break;
			case 6:Operario.setText(Operarios1.get(6));Codigo.setText(Codigos.get(6));Autorizado.setText(Autorizacion.get(6));break;
		}
	}
	
	
	@Override
	public void onNothingSelected(AdapterView<?> parent) {
		Pos = Lista.getSelectedItemPosition(); 
		Operario.setText(Operarios1.get(Pos));
		Codigo.setText(Codigos.get(Pos));
		Autorizado.setText(Autorizacion.get(Pos));
	}
}
