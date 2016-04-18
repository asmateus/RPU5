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
    public partial class Nuevo_Operario : Form
    {
        String recursos;
        public Nuevo_Operario(String recursos)
        {
            InitializeComponent();
            this.recursos = recursos;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                String nombre = textBox1.Text;
                String apellido = textBox2.Text;
                String id = textBox3.Text;
                int cargo = comboBox1.SelectedIndex;
                System.IO.StreamReader file =
                    new System.IO.StreamReader(recursos + "/Operarios.txt");
                System.IO.StreamWriter file2 =
                    new System.IO.StreamWriter(recursos + "/OperariosAux.txt");
                String linea;
                while (((linea = file.ReadLine()) != null))
                {
                    file2.WriteLine(linea);
                }
                file2.WriteLine(nombre + ";" + apellido + ";" + id + ";" + cargo);
                file.Close();
                file2.Close();
                System.IO.File.Copy(recursos + "/OperariosAux.txt", recursos + "/Operarios.txt",true);
                System.IO.File.Delete(recursos + "/OperariosAux.txt");
                MessageBox.Show("Operario agregado con éxito","Aviso");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el operario"+ex, "Error");
            }
        }

    }
}
