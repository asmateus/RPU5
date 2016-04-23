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
    public partial class Nuevo_Producto : Form
    {
        String recursos;
        public Nuevo_Producto(String recursos)
        {
            InitializeComponent();
            this.recursos = recursos;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int canta = Int32.Parse(textBox1.Text);
                int cantc = Int32.Parse(textBox2.Text);
                int cantd = Int32.Parse(textBox3.Text);
                System.IO.StreamReader file =
                    new System.IO.StreamReader(recursos + "/Ordenes.txt");
                System.IO.StreamWriter file2 =
                    new System.IO.StreamWriter(recursos + "/OrdenesAux.txt");
                String linea;
                while (((linea = file.ReadLine()) != null))
                {
                      file2.WriteLine(linea);
                }
                file2.WriteLine(canta + ";" + cantc + ";" + cantd);
                file.Close();
                file2.Close();
                System.IO.File.Copy(recursos + "/OrdenesAux.txt", recursos + "/Ordenes.txt", true);
                System.IO.File.Delete(recursos + "/OrdenesAux.txt");
                MessageBox.Show("Orden Creada", "Aviso");
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al guardar", "Aviso");
            }

            
        }
    }
}
