using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

/*
*
* Esta clase verifica todas las conexiones del programa y genera las acciones adecuadas
* dependiendo del estado de las mismas. Para ello debe recibir la conexión inicial, esto
* es, la clase, ella misma no puede iniciar un vínculo de conexión. 
* 
*/

namespace RPU5
{
    class Status : BaseThread
    {
        private static int SLEEP_TIME = 2000;
        private Connection conn;

		private System.Windows.Forms.PictureBox imagenestaciones;
		private string[] OrdenEst = { "Waiting", "Waiting", "Waiting", "Waiting", "Waiting", "Waiting", "Waiting" };
		private string[] prev_OrdenEst = { "Carro", "Waiting", "Waiting", "Waiting", "Waiting", "Waiting", "Waiting" };
		private string[] Auto = { "NA", "NA", "NA", "NA", "NA", "NA", "NA" };
		private string[] prev_Auto = { "Si", "NA", "NA", "NA", "NA", "NA", "NA" };
		private string[] state = { "No", "Si", "No", "No", "No", "No", "No" };
		private string[] prev_state = { "No", "No", "No", "No", "No", "No", "No" };
		private string[] marco = { "frame_gray", "frame_green", "frame_yellow", "frame_red" };
		private int[] WarningOp = { 0, 0, 0, 0, 0, 0, 0 };
		private bool graphicate_once = false;


		private Estacion manejador_estaciones;
		private graficador graficar;
		private Control.ControlCollection main_copy;


		public Status(Connection conn, Estacion manejador_estaciones, graficador graficar, Control.ControlCollection main_copy) {
            this.conn = conn;
			this.manejador_estaciones = manejador_estaciones;
			this.graficar = graficar;
			this.main_copy = main_copy;
        } 
        public override void RunThread()
        {
            FeedBack mess = new FeedBack();
            while (true)
            {
                // Verificar conexiones
                switch (this.conn.status()) { 
                    case 0:
                        mess.SendSignal("CONNECTION_IDLE");
                        break;
                    case -1:
                        mess.SendSignal("CONNECTION_ERROR");
                        this.conn.Open();
                        break;
                    case 1:
                        mess.SendSignal("CONNECTION_OK");
                        break;
                }
				verify_connection_stations ();

                Thread.Sleep(SLEEP_TIME);
            }
        }
		private void verify_connection_stations()
		{
			checkestaciones ();
			if (graphicate_once == false) {
				graphicate_once = true;
				update_form ();
			}
			bool eq = true;
			for (int i = 0; i < state.Length; ++i) {
				if (state [i] != prev_state [i]) {
					eq = false;
				}
				prev_state [i] = state [i];
			}
			for (int i = 0; i < Auto.Length; ++i) {
				if (Auto [i] != prev_Auto [i]) {
					eq = false;
				}
				prev_Auto [i] = Auto [i];
			}
			for (int i = 0; i < OrdenEst.Length; ++i) {
				if (OrdenEst [i] != prev_OrdenEst [i]) {
					eq = false;
				}
				prev_OrdenEst [i] = OrdenEst [i];
			}
			if (eq == false) {
				update_form ();
			} 
		}

		public void checkestaciones()
		{
			for (int i = 1; i < 8; i++)
			{
				string station = "" + i;
				state[i-1] = manejador_estaciones.getOperario(station, "Estado");
				WarningOp[i - 1] = manejador_estaciones.OperarioWarning(station);
				Auto[i-1] = manejador_estaciones.getOperario(station, "Autorizado");
				OrdenEst = manejador_estaciones.OrdenEstacion(station);

			}
		}

		private void update_form()
		{
			// Vamos a hacer esto paso a paso
			for (int j = 1; j < 8; ++j) {
				graficar_marco (j);
				graficar_conexion (j);
				graficar_autorizacion (j);
				graficar_producto (j);
			}
		}

		private void graficar_marco(int j)
		{
			string imagen;
			if (state [j - 1] == "No") {
				imagen = marco [0];
			} 
			else {
				if (Auto [j - 1] == "No") {
					imagen = marco [2];
				} 
				else {
					imagen = marco [1];
				}
			}
			imagenestaciones = (PictureBox)main_copy["Frame" + j];
			graficar.imagen(imagen, imagenestaciones);
		}

		private void graficar_conexion (int i)
		{
			if (state[i - 1] == "Si")
			{
				imagenestaciones = (PictureBox)main_copy["conexion" + i];
				graficar.imagen("estacion_activa", imagenestaciones);
			}
			else
			{
				imagenestaciones = (PictureBox)main_copy["conexion" + i];
				graficar.imagen("estacion_inactiva", imagenestaciones);
			}
		}

		private void graficar_autorizacion (int i)
		{
			if (Auto[i - 1] == "Si" && WarningOp[i - 1] == 1)
			{
				imagenestaciones = (PictureBox)main_copy["usuario" + i];
				graficar.imagen("usuario_activo_warning", imagenestaciones);

			}
			else if (Auto[i - 1] == "Si" && WarningOp[i - 1] == 0)
			{
				imagenestaciones = (PictureBox)main_copy["usuario" + i];
				graficar.imagen("usuario_activo", imagenestaciones);
			}
			else if (Auto[i - 1] == "NA")
			{
				imagenestaciones = (PictureBox)main_copy["usuario" + i];
				graficar.imagen("usuario_inactivo", imagenestaciones);
			}
			else if (Auto[i - 1] == "No")
			{
				imagenestaciones = (PictureBox)main_copy["usuario" + i];
				graficar.imagen("usuario_warning", imagenestaciones);
			}
		}

		private void graficar_producto (int i)
		{
			imagenestaciones = (PictureBox)main_copy["estacion" + i];
			graficar.imagen(OrdenEst[i - 1], imagenestaciones);
		}
    }
}
