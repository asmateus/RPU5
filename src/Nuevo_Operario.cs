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
    partial class Nuevo_Operario : Form
    {
        String[] sel;
        Connection Base;

        public Nuevo_Operario(String[] seleccionado, Connection ManejadorConexion)
        {
            InitializeComponent();
            this.sel = seleccionado;
            this.Base = ManejadorConexion;
            textBoxNombre.Text = seleccionado[0];
            textBoxCodigo.Text = seleccionado[1];
            textBoxEPC.Text = seleccionado[2];
            //try
            //{
                comboBoxCargo.SelectedIndex =cargoToInt(seleccionado[3]);
            //}
            //catch (Exception e)
            //{ MessageBox.Show("" + e); }
        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private int cargoToInt(string cargoString)
        {
            int cargoInt = 0;
                switch (cargoString)
                {
                    case "Administrador": { cargoInt = 0; break; }
                    case "Picker": { cargoInt = 8; break; }
                    case "Estación 1": { cargoInt = 1; break; }
                    case "Estación 2": { cargoInt = 2; break; }
                    case "Estación 3": { cargoInt = 3; break; }
                    case "Estación 4": { cargoInt = 4; break; }
                    case "Estación 5": { cargoInt = 5; break; }
                    case "Estación 6": { cargoInt = 6; break; }
                    case "Estación 7": { cargoInt = 7; break; }
                    default: {break; };
            }
            return cargoInt;
        }

        private void Aceptar_Click(object sender, EventArgs e)
        {
            try
            {
                String nombre = textBoxNombre.Text;
                String epc = textBoxEPC.Text;
                String codigo = textBoxCodigo.Text;
                String cargo = "" + comboBoxCargo.SelectedItem;

                //AQUI COLOCAR LA PARTE DE UPDATE CON CONNECTIONS
                //Con busqueda en NoEsta=cargo y reemplazando Nombre, Codigo, EPC y NoEsta=cargoSTRING.
                Base.update("Operarios", "Estacion='" + cargoToInt(sel[3]) + "'", "Nombre='" + nombre + "'");
                Base.update("Operarios", "Estacion='" + cargoToInt(sel[3]) + "'", "Codigo='" + codigo + "'");
                Base.update("Operarios", "Estacion='" + cargoToInt(sel[3]) + "'", "EPC='" + epc + "'");
                Base.update("Operarios", "Estacion='" + cargoToInt(sel[3]) + "'", "Estacion='" + cargo + "'");

                if (cargo.Equals(sel[3])==false) //si no hay cambio de cargo se update normal
                {
                    //si hay cambio entonces update con busqueda en NoEsta = cargoINT
                    //reemplazando SOLO el NoEsta = sel[3]
                    Base.update("Operarios", "Estacion='" + cargoToInt(cargo) + "'", "Estacion='" + cargoToInt(sel[3]) + "'");
                }

                //Otro update con busqueda en NoEsta=cargoSTRING y reemplazando SOLO NoEsta=cargoINT.
                Base.update("Operarios", "Estacion='" + cargo + "'", "Estacion='" + cargoToInt(cargo) + "'");

                MessageBox.Show("Operario editado con éxito","Aviso");
                this.Close();
                //llegó un Estacion 2 y se cambió a 5, entonces al de estacion 2 se le pone 5 y al de la 5 se le pone 2.

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al editar el operario"+ex, "Error");
            }
        }

    }
}
