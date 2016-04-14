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
    public partial class VerOrdenes : Form
    {
        private String recursos;
        public VerOrdenes(String recursos)
        {
            InitializeComponent();
            this.recursos = recursos;
            this.DialogResult = DialogResult.Cancel;
        }

        private void VerOrdenes_Load(object sender, EventArgs e)
        {
            reloadList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Nuevo_Producto h = new Nuevo_Producto(recursos);
            DialogResult f=h.ShowDialog();

            reloadList();

        }

        private void reloadList()
        {
            listBox1.Items.Clear();
            System.IO.StreamReader file =
                new System.IO.StreamReader(recursos + "/Ordenes.txt");
            String line; String[] datos;
            while ((line = file.ReadLine()) != null)
            {
                datos = line.Split(';');
                listBox1.Items.Add("Aviones: " + datos[0] + " - Carros: "
                    + datos[1] + " - Dinosaurios: " + datos[2]);
            }
            file.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int sel = listBox1.SelectedIndex;
                int i = 0;
                System.IO.StreamReader file =
                    new System.IO.StreamReader(recursos + "/Ordenes.txt");
                System.IO.StreamWriter file2 =
                    new System.IO.StreamWriter(recursos + "/OrdenesAux.txt");
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
                System.IO.File.Copy(recursos + "/OrdenesAux.txt", recursos + "/Ordenes.txt", true);
                System.IO.File.Delete(recursos + "/OrdenesAux.txt");
                MessageBox.Show("Orden eliminada satisfactoriamente");
                reloadList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar la orden");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int canta = Int32.Parse(label6.Text);
            int cantc = Int32.Parse(label5.Text);
            int cantd = Int32.Parse(label4.Text);
            int totalProd = canta + cantc + cantd;
            Producto[] productos = new Producto[totalProd];
            if (System.IO.File.Exists(recursos + "/Orden.txt")) { System.IO.File.Delete(recursos + "/Orden.txt");}
           // if (System.IO.File.Exists(recursos + "/EPCs.txt")) { System.IO.File.Delete(recursos + "/EPCs.txt"); }
            System.IO.StreamWriter file =
                new System.IO.StreamWriter(recursos + "/Orden.txt");
            //System.IO.StreamWriter file0 =
            //    new System.IO.StreamWriter(recursos + "/EPCs.txt");
            String lastEPC = getLastEPC();
            long epcInt = Convert.ToInt64(lastEPC, 16);
            for (int i=0; i<totalProd;i++)
            {
                int tipo;
                if(i<canta){
                    tipo=1;
                }else{
                    if(i<canta+cantc){
                        tipo=2;
                    }else{
                        tipo=3;
                    }
                }
                epcInt++;
                String id= epcInt.ToString("X");
                file.WriteLine("" + tipo);
                //file0.WriteLine("" + id);
            }
            file.Close();
            //file0.Close();
            MessageBox.Show("Orden Seleccionada con éxito.","Aviso");
            this.DialogResult = DialogResult.OK;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sel = listBox1.SelectedIndex;
            if ((sel < listBox1.Items.Count)&&(sel>=0))
            {
                int i = 0;
                System.IO.StreamReader file =
                    new System.IO.StreamReader(recursos + "/Ordenes.txt");
                String linea = "";
                while ((i <= sel) && ((linea = file.ReadLine()) != null))
                {
                    i++;
                }
                file.Close();
                String[] cadenas = linea.Split(';');
                label6.Text = cadenas[0];
                label5.Text = cadenas[1];
                label4.Text = cadenas[2];
            }
        }

        private String getLastEPC()
        {
            String s="";
            if(!System.IO.File.Exists(recursos+"/EPCFin.txt"))
            {
                s = "2FFFFFFFFFFF";
            }else
            {
                System.IO.StreamReader file =
                    new System.IO.StreamReader(recursos + "/EPCFin.txt");
                String linea;
                while ((linea = file.ReadLine()) != null)
                {
                    s = linea;
                }
            }
            return s;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
