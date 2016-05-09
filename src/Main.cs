﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace RPU5
{
    public partial class Main : Form
    {
		public static String ruta_recursos;
		public static Control[,] GUI_estacion = new Control[6, 8];
        public static Control[] GUI_picking = new Control[8];

        private Connection connection;
        private Status status_thread;
        private Graficador graficar;
		private Estacion manejador_estaciones;

        public Main()
        {
            InitializeComponent();

            // Asignar ruta a recursos, DEBE MODIFICARSE por computador
            ruta_recursos = @"A:\Andres\Git\RPU5\resources";

            // Abrir conexión con el servidor
            connection = new Connection("192.168.1.7", 3306);
            connection.Open("RPU5", "rpu5", "Andre210229");

            // Crea los paquetes de controles
            packet_of_controls_es();
            packet_of_controls_pk();

            // Iniciar las estaciones
            manejador_estaciones = new Estacion(connection);
            manejador_estaciones.InitializeOpe();

            // Iniciar graficador
            graficar = new Graficador(ruta_recursos, GUI_estacion, GUI_picking, manejador_estaciones, connection); 

			// Inicia el hilo que verifica el estado de las conexiones y los operarios
			status_thread = new Status(connection, manejador_estaciones, graficar, this.Controls);
			status_thread.Start();
        }

        private void packet_of_controls_es()
        {
            List<string> control_types = new List<string> () { "label", "Frame", "estacion", "usuario", "conexion" };

            for (int j = 0; j < control_types.Count; ++j)
                for (int i = 1; i < 9; ++i)
                    GUI_estacion[j, i - 1] = Controls[control_types[j] + i];

        }

        private void packet_of_controls_pk()
        {
            List<string> control_types = new List<string>() { "label", "Frame", "conexion", "usuario", "Picking", "Picking", "Picking" };
            for (int i = 0; i < 7; ++i) {
                string temp;
                if (i < 4)
                    temp = "8";
                else
                    temp = Convert.ToString(i - 3);
                GUI_picking[i] = Controls[control_types + temp];
            }
        }

        // FUNCIONES DE ELEMENTOS DE LA UI

        private void verLaAyudaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graficar.ayuda();
        }

        private void acerdaDeRPU5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graficar.acercade();
        }

        private void operariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
        //    graficar.operarios();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }
    }
}
