package com.example.rpu5;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;

public class Main_RPU5 extends Activity {
	Button Menu_pedidos,Menu_estaciones,Menu_configuracion;
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main__rpu5);
		Menu_pedidos              = (Button)     findViewById(R.id.button_pedido);
		Menu_estaciones           = (Button)     findViewById(R.id.button_estaciones);
		Menu_configuracion           = (Button)     findViewById(R.id.button_configuracion);
		
		Menu_pedidos.setOnClickListener(new View.OnClickListener() {
			public void onClick(View v) {
				Intent Pedido_in = new Intent("android.intent.action.MENU_PEDIDO");
				startActivity(Pedido_in);
			}
		});
		
		Menu_estaciones.setOnClickListener(new View.OnClickListener() {
			public void onClick(View v) {
				Intent Pedido_in = new Intent("android.intent.action.MENU_ESTACIONES");
				startActivity(Pedido_in);
			}
		});
		
		Menu_configuracion.setOnClickListener(new View.OnClickListener() {
			public void onClick(View v) {
				Intent Pedido_in = new Intent("android.intent.action.MENU_CONFIGURACION");
				startActivity(Pedido_in);
			}
		});
		
	}
}
