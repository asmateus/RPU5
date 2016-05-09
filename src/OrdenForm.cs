using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPU5
{
    partial class OrdenForm : Form
    {
        
        String sel; String[] cadenas;
        string[,] orden;
        string recursos;
        private Connection BaseHandler;

        public OrdenForm(Connection connection, string recursos)
        {
            InitializeComponent();
            this.BaseHandler = connection;
            this.recursos = recursos;
        }

        private void OrdenForm_Load(object sender, EventArgs e)
        {
            // reloadlist();
            listBox1.Items.Clear();
            string[] headers = { "Tipo", "EPC", "Numpedido","Estado" };
            info_Orden(headers);
        }


        public string[,] info_Orden(string[] Headers)
        {
            int tamFilas = BaseHandler.pullRow("orden", Headers[0]).Count;
            int tamColumnas = Headers.Length;
            string escribir;
            orden = new string[tamColumnas + 1, tamFilas+1];
            //string[] Headers = { "Tipo", "EPC", "Estado", "Prioridad", "HoraIngreso", "HoraSalida", "Numpedido" };
            try
            {
                for (int filas = 0; filas < tamFilas; filas++)
                {
                    escribir = "";
                    for (int i = 0; i < tamColumnas; i++)
                    {
                        orden[i, filas] = BaseHandler.pullRow("orden", Headers[i])[filas];
                        escribir = escribir + orden[i, filas] + " | ";
                    }

                    listBox1.Items.Add(escribir);
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(""+e);
            }
            return orden; 
        }

       
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string[] headers = { "Tipo", "EPC", "Numpedido" };
            string[] stringSeparators = new string[] { " | " };
            sel = "" + listBox1.SelectedItem;
            cadenas = sel.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            NoOrden.Text = cadenas[2];
            tipoText.Text = cadenas[0];
            EPCTag.Text = cadenas[1];
            EstadoTag.Text = cadenas[3];
            pictureBox1.BackgroundImage = Image.FromFile(recursos + "/" + cadenas[0] + ".png");
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;

        }

        //public string estadoToString(string estado)
        //{
        //    //estado_String;
        //    //return estado_String
        //}
    }
}
