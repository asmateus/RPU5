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
    public partial class OrdenForm : Form
    {
        Orden orden;
        String recursos;

        public OrdenForm()
        {
            InitializeComponent();
        }

        public OrdenForm(String nuevoTitulo)
        {
            InitializeComponent();
            this.Text = nuevoTitulo;
        }

        public void setOrden(Orden orden,String recursos)
        {
            this.orden = orden;
            this.recursos = recursos;
        }

        private void OrdenForm_Load(object sender, EventArgs e)
        {
            int cant = orden.getCantidad();
            Producto[] prods = orden.getProductos();
            for (int i=0;i<cant;i++){
                listBox1.Items.Add(prods[i].tipoToString()+" , "+prods[i].estadoToString());
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = listBox1.SelectedIndex;
            Producto p = orden.getProducto(i);
            tipoText.Text = p.tipoToString();
            EstadoTag.Text = p.estadoToString();
            EPCTag.Text = p.getId();
            pictureBox1.BackgroundImage = Image.FromFile(recursos+ "/" + p.tipoToString() + ".jpg");
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
        }

    }
}
