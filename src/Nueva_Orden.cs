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
    partial class Nueva_Orden : Form
    {
        private Connection BaseHandler;
        public static Dictionary<string, string> new_order;
        private List<string>[] productos = new List<string>[3];
        private string[,] EPCs = new string[8,4];
        public static int[] cantidades;
        int[]  posicion = new int[3]; //Indica la posicion de EPCs en que debe empezar la siguiente orden
        int[] maximo;
        int intento;
        string[,] productosSeleccionados; // Tipo EPC [columnas,filas][] [0,x][ y [1,x] [1,x]
        // [ {::}, {..}, {::}] -> 3 ordenes, 2 productos en orden 1 y 3, 1 solo producto en orden 2

            // Es complicado hacer la comunicacion de la tabla de aquí para la otra GUI así que mejor hazlo con la 
            // base de datos como en Ver Operarios .-.

        public Nueva_Orden(Connection basesita, int intento, int[] pos)
        //public Nueva_Orden(int intento, int[] pos)
        {

            InitializeComponent();
            this.BaseHandler = basesita;
            this.intento = intento;
            InventarioEpcs();
            this.posicion = pos;
                        
            for (int tipo = 0; tipo < 3; tipo++)
            {
                NumericUpDown a = (NumericUpDown)this.Controls["Cant_" + productos[tipo][0]];
                //if (posicion[tipo]<0) { a.Maximum = maximo[tipo] - posicion[tipo]; }
                //else { a.Maximum = maximo[tipo] - posicion[tipo] - 1; }
                a.Maximum = maximo[tipo] - posicion[tipo];
                //int nada = 0;     //Esto es para probar interrumpiendo
            }
        }

        public void InventarioEpcs()
        {

            productos[0] = BaseHandler.pull("productos", "Tipo='Carro'");
            productos[1] = BaseHandler.pull("productos", "Tipo='Avion'");
            productos[2] = BaseHandler.pull("productos", "Tipo='Dinosaurio'");


            //lista[0]=tipo1 [1]:epc1 [2]:tipo2 [3]:epc2 [4]:tipo3 [5]:epc3 .... Los epc estan en i=1... i+2
            int[] max = { productos[0].Count / 2, productos[1].Count / 2, productos[2].Count / 2 };
            maximo = max;

            for (int tipo = 0; tipo < 3; tipo++)
            {
                for (int epc = 1, i = 0; i < maximo[tipo]; epc += 2, i++)
                {
                    EPCs[i,tipo] = productos[tipo][epc]; //el error esta en que solo estoy guardando un tipo de epc, no los epc de cada tipo
                    //toca hacer la matriz
                }
            }
        }


        private void Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                cantidades = new int[]{ (int)Cant_Carro.Value, (int)Cant_Avion.Value, (int)Cant_Dinosaurio.Value };
                bool bandera = cantidades[0].Equals(0) & cantidades[1].Equals(0) & cantidades[2].Equals(0);
                if (bandera) { MessageBox.Show("Seleccione un producto al menos"); }
                else
                {
                    loadOrden();
                    getOrden();
                    MessageBox.Show("Orden #" + intento + " Creada", "Aviso");
                    intento++;                    
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar "+ex, "Aviso");
            }

        }

        
        public void loadOrden()
        {
            
           

            for (int tipo = 0; tipo < 3; tipo++)
            {
                productosSeleccionados = new string[2, cantidades[tipo] + 1];
                if (cantidades[tipo].Equals(0) == false)
                {
                    
                    for (int j = posicion[tipo], i=0; j < cantidades[tipo]; j++, i++)
                    {
                        // ESCRIBIR EN LA FILA DE Orden
                        new_order = new Dictionary<string, string>();
                        new_order.Add("Tipo", productos[tipo][0]);
                        new_order.Add("EPC", EPCs[j, tipo]);
                        new_order.Add("Estado", "0");
                        new_order.Add("Numpedido", ""+intento);
                        BaseHandler.push("orden", new_order);
                    }

                }
                posicion[tipo] = posicion[tipo]+cantidades[tipo];
            }

            

        }

        public string[,] getOrden()
        {
            return this.productosSeleccionados;
        }

        public int[] getPosicion()
        {
            return this.posicion;
        }

        public int getIntento()
        {
            return this.intento;
        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
