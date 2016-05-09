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

        private List<string> table_content;
        private List<string> table_content_prev = new List<string>();

		private string[] OrdenEst = { "Waiting", "Waiting", "Waiting", "Waiting", "Waiting", "Waiting", "Waiting", "Waiting" };
		private string[] prev_OrdenEst = { "Carro", "Waiting", "Waiting", "Waiting", "Waiting", "Waiting", "Waiting", "Waiting" };
        private string[] sem_state = { "gray", "gray", "gray", "gray", "gray", "gray", "gray", "gray"};
        private string[] Auto = { "NA", "NA", "NA", "NA", "NA", "NA", "NA", "NA" };
		private string[] state = { "No", "Si", "No", "No", "No", "No", "No", "No" };
		private string[] marco = { "frame_gray", "frame_green", "frame_yellow", "frame_red" };
		private int[] WarningOp = { 0, 0, 0, 0, 0, 0, 0, 0 };
		private bool graphicate_once = false;
        bool eq;


        private Estacion manejador_estaciones;
		private Graficador graficar;
		private Control.ControlCollection main_copy;


		public Status(Connection conn, Estacion manejador_estaciones, Graficador graficar, Control.ControlCollection main_copy) {
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

            // La función de los ciclos siguientes es verificar evitar que se redibuje el UI si nada
            // ha cambiado.
			eq = true;
            for (int i = 0; i < table_content_prev.Count; ++i)
                if (table_content_prev[i] != table_content[i]) {
                    eq = false;
                    table_content_prev[i] = table_content[i];
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
            table_content = conn.pullAll("operarios");
            if (table_content_prev.Count == 0) {
                for (int i = 0; i < table_content.Count; ++i)
                    table_content_prev.Add(table_content[i]);
            }
            for (int i = 6, k = 5, l = 8, j = 0; i < table_content.Count; i += 9,k +=9, l += 9, ++j)
            {
                state[j] = table_content[i];
                Auto[j] = table_content[k];
                sem_state[j] = table_content[l];
            }
			for (int i = 1; i < 9; i++)
			{
				string station = "" + i;
				WarningOp[i - 1] = manejador_estaciones.OperarioWarning(station);
				OrdenEst = manejador_estaciones.OrdenEstacion(station);
			}
		}

		private void update_form()
		{
            // Vamos a hacer esto paso a paso
            for (int j = 1; j < 9; ++j) {
				graficar_marco (j);
				graficar_conexion (j);
				graficar_autorizacion (j);
				graficar_producto (j);
                if (j == 8)
                    graficar_picking();
			}
		}

		private void graficar_marco(int j)
		{
            imagenestaciones = (PictureBox)main_copy["Frame" + j];
            if (j != 8)
            {
                graficar.imagen("Frame_" + sem_state[j-1], imagenestaciones);
            }
            else
            {
                graficar.imagen("Frame_" + sem_state[j-1] + "_Picking", imagenestaciones);
            }
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
            if (i != 8) {
                imagenestaciones = (PictureBox)main_copy["estacion" + i];
                graficar.imagen(OrdenEst[i - 1], imagenestaciones);
            }
		}

        private void graficar_picking ()
        {
            string[] plotear = new string[3];
            if (conn.pullRow("orden", "Tipo").Count == 0)
            { plotear = new string[3] { OrdenEst[7], OrdenEst[7], OrdenEst[7] }; }
            else
            {
                //List<string> orden = connection.pullRow("orden", "Tipo");
                List<string> orden = conn.pullse("orden", "Tipo", "Estado = 10");
                if (orden.Count > 2)
                {
                    plotear = new string[3] { orden[0], orden[1], orden[2] };
                }
                else
                {
                    if (orden.Count == 2)
                    {
                        plotear = new string[3] { orden[0], orden[1], "Waiting" };
                    }
                    else
                    {
                        if (orden.Count == 1)
                        {
                            plotear = new string[3] { orden[0], "Waiting", "Waiting" };
                        }
                        else
                        {
                            plotear = new string[3] { "Waiting", "Waiting", "Waiting" };
                        }
                    }
                }

            }

            for (int j = 0; j < 3; j++)
            {
                imagenestaciones = (PictureBox)main_copy["Picking" + (j + 1)];
                graficar.imagen(plotear[j], imagenestaciones);
            }
        }
    }
}
