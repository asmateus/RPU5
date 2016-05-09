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
    partial class VerOperarios : Form
    {
        String[] cadenas; string sel;
        private Estacion seleccionado;
        private Connection connectionhandler;

        public VerOperarios(Estacion reinodedaniela, Connection connectionhandler) //colocar , Estacion reinodedaniela
        {
            InitializeComponent();
            this.seleccionado = reinodedaniela;
            this.connectionhandler = connectionhandler;
        }

        private void VerOperarios_Load(object sender, EventArgs e)
        {
            reloadList();
        }

        private void reloadList()
        {
            listBox1.Items.Clear();
            for (int i = 0; i < 9; i++)
            {
                string nombre = seleccionado.getOperario(i.ToString(), "Nombre");
                string codigo = seleccionado.getOperario(i.ToString(), "Codigo");
                string epc = seleccionado.getOperario(i.ToString(), "EPC");
                string cargo = cargoToString(i); //OJO AGREGAR ESO EN Estacion| 0:base 8:picking

                listBox1.Items.Add(nombre + " | " + codigo + " | " + epc + " | " + cargo);
            }
        }

        ////....Codigo para agregar en Estacion.........
        private string cargoToString(int NoEsta)
        {
            string cargo = "Inicial";
            if (NoEsta == 0) { cargo = "Administrador"; }
            else if (NoEsta < 8) { cargo = "Estación " + NoEsta; }
            else if (NoEsta == 8) { cargo = "Picker"; }
            return cargo;
        }
        ////......Aqui termina :v ............................


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] stringSeparators = new string[] { " | " };
            sel = ""+ listBox1.SelectedItem;
            cadenas = sel.Split(stringSeparators,StringSplitOptions.RemoveEmptyEntries);
            labelnombre.Text = cadenas[0];
            labelcodigo.Text = cadenas[1];
            labelEPC.Text = cadenas[2];
            labelcargo.Text = cadenas[3];            
        }
    

        private void Edit_Click(object sender, EventArgs e)
        {
            
            try
            {
                Nuevo_Operario h = new Nuevo_Operario(cadenas,connectionhandler);
                DialogResult f=h.ShowDialog();
                reloadList();
                listBox1.SelectedItem = sel;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el operario "+ex);
            }
        }
    }
}