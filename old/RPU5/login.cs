using RPU5;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace portalesrcl
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

       

        private void ingresar_Click(object sender, EventArgs e)
        {
            if ((txtuser.Text != "") && (txtpass.Text != ""))
            {
                if ((txtuser.Text == "laboratorios") && (txtpass.Text == "uninorte"))

                {
                    this.Hide();
                    Main main = new Main();
                    main.ShowDialog();
                    this.Close();
                }
                else
                    MessageBox.Show("Autenticación Errónea");
            }
            else
            {
                MessageBox.Show("Debe ingresar su Usuario y contraseña");
            }
        }

        private void borrar_Click(object sender, EventArgs e)
        {
            txtuser.Clear();
            txtpass.Clear();
        }

        private void cancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void login_Load(object sender, EventArgs e)
        {

        }

        private void showpass_CheckedChanged(object sender, EventArgs e)
        {
            if (showpass.Checked == true)
            {
                txtpass.UseSystemPasswordChar = false;
            }
            else
            {
                txtpass.UseSystemPasswordChar = true;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtpass_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
