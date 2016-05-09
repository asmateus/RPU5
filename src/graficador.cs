using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;


using System.ComponentModel;
using System.Data;


namespace RPU5
{
    class Graficador
    {
        String recursos;
        //Pen pen; SolidBrush fondorect; Rectangle frame;
        //Graphics panel;
        //Point punto;
        ToolTip toolTip = new ToolTip(); Control[,] ConjuntoControles; Control[] c = new Control[6];
        Control[] p = new Control[8]; //Controles de picking
        Estacion estacioneshandler;
        Connection basehandler;


        public Graficador(string rutaRecursos, Control[,] MatrizDeControlesEstaciones, Control[] ControlesPicking, Estacion reinodedaniela, Connection connection)

        {
            this.estacioneshandler = reinodedaniela;
            this.basehandler = connection;
            //panel = panel_estaciones.CreateGraphics();
            ConjuntoControles = MatrizDeControlesEstaciones;
            p = ControlesPicking;
            recursos = rutaRecursos;
            
            //Aquí se configura el comportamiento del pop up
            toolTip.AutoPopDelay = 10000;
            toolTip.InitialDelay = 500;
            toolTip.ReshowDelay = 0;
            toolTip.ShowAlways = true;
            
        }


        public void imagen(string imagen, PictureBox nombrePicBox)
        {
            //Para usar este método es necesario que antes de llamarlo se cree el PictureBox que se ploteará:
            //nombrePicBox = (PictureBox)this.Controls["nombreenlaGUI"+indice];
            nombrePicBox.BackgroundImage = Image.FromFile(recursos +"/"+ imagen + ".png");
            //nombrePicBox.BackgroundImage = Image.FromFile(imagen);
            //RPU5.Properties.Resources.frame_green
            nombrePicBox.BackgroundImageLayout = ImageLayout.Stretch;
        }

        public ToolTip ifoestaciones(int IndiceEstacion, bool FlagAutorizado)
        {

            string mensaje = "Estacion " + IndiceEstacion + "\n";
            mensaje = mensaje + "Nombre del Operario: " + estacioneshandler.getOperario("" + IndiceEstacion, "Nombre") + ".\n";
            mensaje = mensaje + "Código del Operario: " + estacioneshandler.getOperario("" + IndiceEstacion, "Codigo") + ".\n";
            if (FlagAutorizado==false)
            {
                mensaje = mensaje + "EPC no autorizado: " + Estacion.operariono["EPC"];
                
            }

            if (IndiceEstacion == 8) //Si la estacion es Picking
            {
                toolTip.SetToolTip(p[0], mensaje); //Aquí se setea el tooltip
                toolTip.SetToolTip(p[1], mensaje);
                toolTip.SetToolTip(p[2], mensaje);
                toolTip.SetToolTip(p[3], mensaje);
                toolTip.SetToolTip(p[4], mensaje);
                toolTip.SetToolTip(p[5], mensaje);
                toolTip.SetToolTip(p[6], mensaje);
            }
            else
            {
                IndiceEstacion = IndiceEstacion - 1;
                for (int i = 0; i < 6; i++) //Esto crea el vector de pictures y labels de la estación que se requiere
                {
                    c[i] = ConjuntoControles[i, IndiceEstacion];
                }

                toolTip.SetToolTip(c[0], mensaje); //Aquí se setea el tooltip
                toolTip.SetToolTip(c[1], mensaje);
                toolTip.SetToolTip(c[2], mensaje);
                toolTip.SetToolTip(c[3], mensaje);
                toolTip.SetToolTip(c[4], mensaje);
            }
            return toolTip;
        }

        public string notificacionPortal(string inORout)
        {
            string Notificacion;
            List<string> infoportales = basehandler.pull(inORout, "Piezas='0000'");
            Notificacion = infoportales[3];
            return Notificacion;
        }

        public void ayuda()
        {       
            String mensaje;
            mensaje = "Bienvenido al asistente de procesos de fabricación asistido por RFID.\n\n";
            mensaje = mensaje + "→ Presione el botón 'Ver Pedido' en la esquina superior izquierda de la interfaz para ver los productos pendientes por fabricar y su información.\n";
            mensaje = mensaje + "→ Puede dejar el puntero sobre las imágenes correspondientes a cada etapa para ver los detalles de lectura y del operario que ocupa ese puesto.\n";
            mensaje = mensaje + "\nADVERTENCIA: No cierre el programa sin haber terminado el proceso. Se perderán los datos de la operación.";

            MessageBox.Show(mensaje, "Ayuda de RPU5");
        }

        public void acercade()
        {
            MessageBox.Show("RFID Process Unit 5 \n\nAutores:\nJuan Gómez -Jorge Ortega\nSebastian Salas - Sorelys Sandoval\nAndrés Simancas\n\nUniversidad del Norte\n\t2016", "Acerca del Programa");
        }


    }
    
}
