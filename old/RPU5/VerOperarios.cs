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
    public partial class VerOperarios : Form
    {
        private System.IO.StreamReader file;
        private Operario seleccionado; 
        private String recursos;
        public VerOperarios(String recursos)
        {
            InitializeComponent();
            this.recursos = recursos;
        }

        private void VerOperarios_Load(object sender, EventArgs e)
        {
            reloadList();
        }

        private void reloadList()
        {
            listBox1.Items.Clear();
            //System.IO.StreamReader file =
              file =  new System.IO.StreamReader(recursos + "/Operarios.txt");
            String line; String[] datos;
            while ((line = file.ReadLine()) != null)
            {
                datos = line.Split(';');
                listBox1.Items.Add(datos[0] + " "+ datos[1] + " , " + datos[2]);
            }
            file.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sel = listBox1.SelectedIndex;
            if ((sel < listBox1.Items.Count) && (sel >= 0))
            {
                int i = 0;
                //System.IO.StreamReader file =
                  file =  new System.IO.StreamReader(recursos + "/Operarios.txt");
                String linea = "";
                while ((i <= sel) && ((linea = file.ReadLine()) != null))
                {
                    i++;
                }
                file.Close();
                String[] cadenas = linea.Split(';');
                label4.Text = cadenas[0];
                label5.Text = cadenas[1];
                label8.Text = cadenas[2];
                seleccionado = new Operario(label4.Text+" "+label5.Text,label8.Text,Int32.Parse(cadenas[3]));
                label6.Text = seleccionado.cargoToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Nuevo_Operario h = new Nuevo_Operario(recursos);
            DialogResult f=h.ShowDialog();
            reloadList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int sel = listBox1.SelectedIndex;
                
                int i = 0;
                //System.IO.StreamReader file =
                  file =  new System.IO.StreamReader(recursos + "/Operarios.txt");
                System.IO.StreamWriter file2 =
                    new System.IO.StreamWriter(recursos + "/OperariosAux.txt");
                String linea;
                while (((linea = file.ReadLine()) != null))
                {
                    if (i != sel)
                    {
                        file2.WriteLine(linea);
                    }
                    i++;
                }
                file.Close();
                file2.Close();
                System.IO.File.Copy(recursos + "/OperariosAux.txt", recursos + "/Operarios.txt", true);
                System.IO.File.Delete(recursos + "/OperariosAux.txt");
                MessageBox.Show("Operario eliminado satisfactoriamente");
                reloadList();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error al eliminar el operario");
                MessageBox.Show(""+ex);
            }
        }
    }
}
