using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using portalesrcl;

namespace RPU5
{
    public partial class Main : Form
    {
        //Constantes
        private int PICKING = 0;
        private int PORTAL1 = 8;
        private int PORTAL2 = 9;

        //Esta es la ventana principal que maneja todo el proceso.
        private int pendientesSalida;
        private Servidor servidor;
        private Orden orden;
        private int completados;
        private int actualPicking;
        private String red;
        public static String recursos;
        public static String rutaPicking;
        private Estacion[] estaciones;

        //Hilos
        private Thread senderPicking;
        private Thread receiverPicking;
        private Thread updater;
        private Thread connecter;

        //Timers
        private Stopwatch timer0 = new Stopwatch();
        private Stopwatch timer1 = new Stopwatch();
        private Stopwatch timer2 = new Stopwatch();
        private Stopwatch timer3 = new Stopwatch();
        private Stopwatch timer4 = new Stopwatch();
        private Stopwatch timer5 = new Stopwatch();
        private Stopwatch timer6 = new Stopwatch();
        private Stopwatch timer7 = new Stopwatch();
        private Stopwatch timerMaster = new Stopwatch();

        //Banderas
        public bool ordenSelected;
        private bool allConnected;
        private bool processing;
        private bool allStations;
        private bool[] hayAutorizado;

        public Main()
        {
            InitializeComponent();
            this.completados = 0;
            this.red = @"C:\Users\Administrador\Desktop\base_de_datos\RPU5\old\RFID\estaciones\textos\"; //colocar la ruta de texto de estaciones
            recursos = @"C:\Users\Administrador\Desktop\base_de_datos\RPU5\old\RFID\Recursos\"; //Aquí la direccion de recursos
            servidor = new Servidor(this.red, 10);
            // picking, estaciones 1 - 7, portal 1-2
            //String rutaRainer = @"C:\Users\win8\Desktop\Recursos";
            //String rutaHector = @"\\USER\Users\Public\RFID\picking";
            rutaPicking = @"C:\Users\Administrador\Desktop\base_de_datos\RPU5\old\RFID\picking\";
            /*RutaRainer y RutaHector son equivalentes a RutaPicking del informe, como son dos grupos de picking con los
            que se tuvo que hacer pruebas simultaneas, entonces se tomaron variables para cada grupo*/
            String[] carpetas = { rutaPicking, red, red, red, red, red, red, red, red, red };
            servidor.setClientPaths(carpetas);
            this.actualPicking = 0;
            this.pendientesSalida = 0;
            initEstaciones();
            connecter = new Thread(conectador);
            updater = new Thread(actualizador);
            senderPicking = new Thread(enviadorPicking);
            receiverPicking = new Thread(recibidorPicking);
            orden = null;
            ordenSelected = false;
            allConnected = true;
            processing = false;
            allStations = false;
            hayAutorizado = new bool[7];
        }

        private void comenzarProceso()
        {
            loadInitialData();
            senderPicking.Start();
            timer8.Start();

            servidor.sendTo(red, recursos, "operariosbase.txt", "operariosbase.txt");
            servidor.sendTo(red, recursos, "productos.txt", "productos.txt");
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CloseCancel() == false)
            {
                e.Cancel = true;
            }
        }

        public static bool CloseCancel()
        {
            const string message = "¿Desea salir del programa sin haber terminado?";
            const string caption = "Finalizar Proceso";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            updater.Start();
            connecter.Start();
        }

        private void ayudaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            String mensaje;

            mensaje = "Bienvenido al asistente de procesos de fabricación asistido por RFID.\n\n";
            mensaje = mensaje + "→ Presione el botón 'Ver Pedido' en la esquina superior izquierda de la interfaz para ver los productos pendientes por fabricar y su información.\n";
            mensaje = mensaje + "→ Puede hacer click sobre las imágenes correspondientes a cada etapa para ver los detalles de lectura y del operario que ocupa ese puesto.\n";
            mensaje = mensaje + "\nADVERTENCIA: No cierre el programa sin haber terminado el proceso. Se perderán los datos de la operación.";

            MessageBox.Show(mensaje, "Ayuda de RPU5");
        }

        private void acercaDeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("RFID Process Unit 5.2 \n\nAutores:\nGuillermo León - Iván Ramirez\nJuan Sanchez - Diego Castaño\nDaniel Arrubla\n\nUniversidad del Norte\n\t2015", "Acerca del Programa");
        }

        private void loadInitialData()
        {
            generateProductsTxt();
            autorizados2operariosBase();
        }

        private void loadOrder()
        {
            String line;
            int[] productos = new int[50];
            String[] EPCs = new String[50];
            int cantidad = 0;
            System.IO.StreamReader file =
                    new System.IO.StreamReader(recursos + "/Orden.txt");
            while ((line = file.ReadLine()) != null)
            {
                productos[cantidad] = int.Parse(line);
                cantidad++;
            }
            file.Close();
            System.IO.StreamReader file2 =
                    new System.IO.StreamReader(recursos + "/EPCs.txt");
            for (int i = 0; i < cantidad; i++)
            {
                EPCs[i] = file2.ReadLine();
            }
            Producto[] p = new Producto[cantidad];
            for (int i = 0; i < cantidad; i++)
            {
                p[i] = new Producto(productos[i], EPCs[i]);
            }
            this.orden = new Orden(p, cantidad);
            file2.Close();
        }

        private void generateProductsTxt()
        {
            Producto[] p = orden.getProductos();
            int cant = orden.getCantidad();
            if (System.IO.File.Exists(recursos + "productos.txt"))
            {
                System.IO.File.Delete(recursos + "productos.txt");
            }

            System.IO.StreamWriter file =
                new System.IO.StreamWriter(recursos + "productos.txt");
            for (int i = 0; i < cant; i++)
            {
                //Empanada 1
                String s = p[i].getId();
                String c1 = s.Substring(0, 4);
                String c2 = s.Substring(4, 4);
                String c3 = s.Substring(8, 4);
                String tipo;

                switch (p[i].getTipo())
                {
                    case 1:
                        {
                            tipo = "avion";
                            break;
                        }
                    case 2:
                        {
                            tipo = "carro";
                            break;
                        }
                    case 3:
                        {
                            tipo = "dinosaurio";
                            break;
                        }
                    default:
                        {
                            tipo = "RektM8";
                            break;
                        }
                }
                file.WriteLine(c1 + " " + c2 + " " + c3 + ";" + tipo);
            }
            //fin empanada 1
            file.Close();
        }

        private void reviseAutorizados()
        {
            System.IO.StreamReader fileCuenta =
                new System.IO.StreamReader(red + "/operariosestaciones.txt");
            String linea;
            int numOperarios = 0;
            while (((linea = fileCuenta.ReadLine()) != null))
            {
                String[] cadenas = cadenas = linea.Split(';');
                //if (Int32.Parse(cadenas[3]) >1)
                //{ //esto es para evitar el error con las estaciones y operarios picker o de base 
                //    hayAutorizado[numOperarios] = false;


                //    //problema para guardar la posicion de cada linea que sí es de estaciones
                //    int estacion = Int32.Parse(cadenas[3]) - 1;
                //    hayAutorizado[estacion - 1] = true;
                //}
                if (cadenas[0] == "si")
                {
                    hayAutorizado[numOperarios] = true;

                }
                else { hayAutorizado[numOperarios] = false; }
                numOperarios = numOperarios + 1;
            }
            fileCuenta.Close();

            //------------------2015----------------------
            //System.IO.StreamReader file =
            //    new System.IO.StreamReader(recursos + "/Operarios.txt");

            //for (int i = 0; i < numOperarios; i++)
            //{
            //    hayAutorizado[i] = false;
            //}

            //for (int i = 0; i < numOperarios; i++)
            //{
            //    linea = file.ReadLine();
            //    String[] cadenas = linea.Split(';');
            //    int estacion = Int32.Parse(cadenas[3])-1;
            //    hayAutorizado[estacion - 1] = true;
            //}
            // file.Close();

            allStations = true;
            for (int i = 0; i < numOperarios; i++)
            {
                allStations = allStations && hayAutorizado[i];
            }
        

    }

        private void testConnections()
        {
            int cant = this.servidor.getCantidad();
            for (int i = 0; i < cant; i++)
            {
                bool flag;
                if ((i == 0) || (i > 7))
                {
                    //flag = servidor.sendTo(i, recursos, "/test.txt", "/test.txt");
                    flag = servidor.receiveFrom(rutaPicking, recursos, "/test.txt");

                }
                else
                {
                    flag = servidor.receiveFrom(red, recursos, "/conexionestacion" + i + ".txt");
                }
                if (flag)
                {
                    servidor.revive(i);
                }
                else
                {
                    servidor.neuter(i);
                }
            }
        }

        private void initEstaciones()
        {
            estaciones = new Estacion[10];
            for (int i = 0; i < 10; i++)
            {
                estaciones[i] = new Estacion();
            }
            updateLabels();
            autorizados2operariosBase();

        }

        private void updateImages()
        {
            reviseAutorizados();
            int[] e = new int[9];
            for (int i = 0; i <= 8; i++)
            {
                e[i] = estaciones[i].getStatus();
            }
            bool[] c = new bool[10];
            allConnected = true;
            c = servidor.checkConnections();
            for (int i = 0; i < 7; i++)
            {
                allConnected = allConnected && c[i];
            }

            ////Empanada 2 2016: Conversor de texto a matriz :v
            //System.IO.StreamReader file0 =
            // new System.IO.StreamReader(recursos + "/Operarios.txt");
            //// operariosbase= (0:epc, 1:estación, 2:nombre, 3:epc"código") .-.
            //// operarios = (0:Nom, 1:apell, 2:cod, 3:estacion)
            //int cant = 0;
            //String line; String[] cadenas=null; String[,] texto = null;
            //while ((line = file0.ReadLine()) != null)
            //{
            //    cadenas = line.Split(';');
            //    for (int i = 0; i < cadenas.Length; i++)
            //    {
            //        texto[i, cant] = cadenas[i];
            //    } //[filas][columnas]
            //    cant++;
            //}
            //file0.Close();
            //// aquí termina el conversor


            //Picking
            statusPic0.BackgroundImage= Image.FromFile(servidor.led(0,c, recursos));
            pictureBox0.BackgroundImage = Image.FromFile(servidor.rellenarIma(0,e, recursos));
            pictureBox0.BackgroundImageLayout = ImageLayout.Stretch;
            //Estacion 1
            statusPic1.BackgroundImage = Image.FromFile(servidor.led(1,c, recursos));
            pictureBox1.BackgroundImage = Image.FromFile(servidor.rellenarIma(1,e, recursos));
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            //Estacion 2
            statusPic2.BackgroundImage = Image.FromFile(servidor.led(2,c, recursos));
            pictureBox2.BackgroundImage = Image.FromFile(servidor.rellenarIma(2,e, recursos));
            pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;
            //Estacion 3
            statusPic3.BackgroundImage = Image.FromFile(servidor.led(3,c, recursos));
            pictureBox3.BackgroundImage = Image.FromFile(servidor.rellenarIma(3,e, recursos));
            pictureBox3.BackgroundImageLayout = ImageLayout.Stretch;
            //Estacion 4
            statusPic4.BackgroundImage = Image.FromFile(servidor.led(4,c, recursos));
            pictureBox4.BackgroundImage = Image.FromFile(servidor.rellenarIma(4,e, recursos));
            pictureBox4.BackgroundImageLayout = ImageLayout.Stretch;
            //Estacion 5
            statusPic5.BackgroundImage = Image.FromFile(servidor.led(5,c, recursos));
            pictureBox5.BackgroundImage = Image.FromFile(servidor.rellenarIma(5,e, recursos));
            pictureBox5.BackgroundImageLayout = ImageLayout.Stretch;
            //Estacion 6
            statusPic6.BackgroundImage = Image.FromFile(servidor.led(6,c, recursos));
            pictureBox6.BackgroundImage = Image.FromFile(servidor.rellenarIma(6,e, recursos));
            pictureBox6.BackgroundImageLayout = ImageLayout.Stretch;
            //Estacion 7
            statusPic7.BackgroundImage = Image.FromFile(servidor.led(7,c, recursos));
            pictureBox7.BackgroundImage = Image.FromFile(servidor.rellenarIma(7,e, recursos));
            pictureBox7.BackgroundImageLayout = ImageLayout.Stretch;


            ///////////Esto bota problemas de uso en varios subprocesos .-.
            ////////System.IO.StreamReader file0 =
            //////// new System.IO.StreamReader(recursos + "/Operarios.txt");
            //////////operariosbase = (0:epc, 1:estación, 2:nombre, 3:epc"código") .-.
            ////////// operarios = (0:Nom, 1:apell, 2:cod, 3:cargo)
            ////////int cant = 0;
            ////////String line; String[] cadenas;
            ////////while ((line = file0.ReadLine()) != null)

            ////////{
            ////////    cadenas = line.Split(';');

            ////////    switch (Int32.Parse(cadenas[3]))
            ////////    {
            ////////        case 1:
            ////////            {
            ////////                this.label8.Text = cadenas[0];
            ////////                this.label10.Text = cadenas[2];
            ////////                this.label4.Text = "Activo";
            ////////                break;
            ////////            }


            ////////        case 2:
            ////////            {

            ////////                this.label13.Text = cadenas[0];
            ////////                this.label11.Text = cadenas[2];
            ////////                this.label17.Text = "Activo";
            ////////                break;
            ////////            }
            ////////        case 3:
            ////////            {
            ////////                this.label21.Text = cadenas[0];
            ////////                this.label19.Text = cadenas[2];
            ////////                this.label25.Text = "Activo";
            ////////                break;
            ////////            }
            ////////        case 4:
            ////////            {

            ////////                this.label29.Text = cadenas[0];
            ////////                this.label27.Text = cadenas[2];
            ////////                this.label33.Text = "Activo";
            ////////                break;
            ////////            }
            ////////        case 5:
            ////////            {
            ////////                this.label37.Text = cadenas[0];
            ////////                this.label35.Text = cadenas[2];
            ////////                this.label41.Text = "Activo";
            ////////                break;
            ////////            }
            ////////        case 6:
            ////////            {

            ////////                this.label45.Text = cadenas[0];
            ////////                this.label43.Text = cadenas[2];
            ////////                this.label49.Text = "Activo";
            ////////                break;
            ////////            }

            ////////        case 7:
            ////////            {
            ////////                this.label53.Text = cadenas[0];
            ////////                this.label51.Text = cadenas[2];
            ////////                this.label57.Text = "Activo";
            ////////                break;
            ////////            }
            ////////        case 8:
            ////////            {

            ////////                this.label61.Text = cadenas[0];
            ////////                this.label59.Text = cadenas[2];
            ////////                this.label65.Text = "Activo";
            ////////                break;
            ////////            }

            ////////    }
            ////////    cant++;
            ////////}
            ////////file0.Close();
            ////////// aquí termina el conversor              
        }

        private void updateLabels() {
            String[] newLineas = new String[50];
            System.IO.StreamReader file0 =
             new System.IO.StreamReader(recursos + "/Operarios.txt");
            int cant = 0;
            String line;
            while ((line = file0.ReadLine()) != null)
            {
                newLineas[cant] = line; cant++;
            }
            file0.Close();
            String[] cadenas;
            for (int i = 0; i < cant; i++)
            {
                //Empanada 2 2016
                cadenas = newLineas[i].Split(';');
                // operariosbase= (0:epc, 1:estación, 2:nombre, 3:epc"código") .-.
                switch (int.Parse(cadenas[3]))
                {
                    case 1:
                        {
                            this.label8.Text = cadenas[0] + " " + cadenas[1];
                            this.label10.Text = cadenas[2];
                            this.label4.Text = "Activo";
                            break;
                        }


                    case 2:
                        {

                            this.label13.Text = cadenas[0] + " " + cadenas[1];
                            this.label11.Text = cadenas[2];
                            this.label17.Text = "Activo";
                            break;
                        }
                    case 3:
                        {
                            this.label21.Text = cadenas[0] + " " + cadenas[1];
                            this.label19.Text = cadenas[2];
                            this.label25.Text = "Activo";
                            break;
                        }
                    case 4:
                        {

                            this.label29.Text = cadenas[0] + " " + cadenas[1];
                            this.label27.Text = cadenas[2];
                            this.label33.Text = "Activo";
                            break;
                        }
                    case 5:
                        {
                            this.label37.Text = cadenas[0] + " " + cadenas[1];
                            this.label35.Text = cadenas[2];
                            this.label41.Text = "Activo";
                            break;
                        }
                    case 6:
                        {

                            this.label45.Text = cadenas[0] + " " + cadenas[1];
                            this.label43.Text = cadenas[2];
                            this.label49.Text = "Activo";
                            break;
                        }

                    case 7:
                        {
                            this.label53.Text = cadenas[0] + " " + cadenas[1];
                            this.label51.Text = cadenas[2];
                            this.label57.Text = "Activo";
                            break;
                        }
                    case 8:
                        {

                            this.label61.Text = cadenas[0] + " " + cadenas[1];
                            this.label59.Text = cadenas[2];
                            this.label65.Text = "Activo";
                            break;
                        }
                }
            }
        }

        private void updateStatusTags()
        {
            if (processing)
            {
                int porcentaje = (completados * 100) / orden.getCantidad();
                if (porcentaje <= 100) { this.setPorcentaje(porcentaje); }
                this.label2.Text = "Pendientes en Salida: " + pendientesSalida;

            }
            else
            {
                processStatus.Text = "No se ha iniciado un proceso";
            }
            if (ordenSelected)
            {
                orderStatus.Text = "Orden seleccionada";
            }
            else
            {
                orderStatus.Text = "No se ha seleccionado orden";
            }
            if (allStations)
            {
                stationsStatus.Text = "Estaciones autorizadas";
            }
            else
            {
                stationsStatus.Text = "Faltan estaciones por autorizar";
            }
            if (allConnected)
            {
                connectionStatus.Text = "Estaciones conectadas";
            }
            else
            {
                connectionStatus.Text = "Faltan estaciones por conectar";
            }
        }

        private void actualizador()
        {
            while (true)
            {
                testConnections();
                updateImages();
                //updateLabels();
                //Actualizar la barra de progreso
                checkEstaciones();
                updateStatusTags();
                Thread.Sleep(1500);
            }
        }

        private void conectador()
        {
            while (true)
            {
                testConnections();
                Thread.Sleep(3000);
            }
        }

        private void enviadorPicking()
        {
            while (true)
            {
                try
                {
                    if (estaciones[PICKING].getStatus() != estaciones[PICKING].READY)
                    {
                        if (actualPicking < orden.getCantidad())
                        {
                           while (!servidor.sendTo(PICKING, recursos,
                                orden.getProducto(this.actualPicking).tipoToString() + ".txt", "/Pedido1.txt"));
                           // orden.getProducto(this.actualPicking).tipoToString() = Avion, carro o dinosaurio

                            estaciones[PICKING].setStatus(orden.getProducto(this.actualPicking).getTipo() - 1);
                           ;
                        }
                        else {
                            if (actualPicking == orden.getCantidad())
                                { estaciones[PICKING].setStatus(estaciones[PICKING].READY); } 
                        }
                       
                        if (receiverPicking.IsAlive) { receiverPicking.Interrupt(); }
                        else { receiverPicking.Start(); }
                    }
                    Thread.Sleep(Timeout.Infinite);
                }
                catch (ThreadInterruptedException e)
                {

                }
            }
        }

        private void recibidorPicking()
        {
            while (true)
            {
                try
                {
                    while (!servidor.receiveFrom(PICKING, recursos, "Tiempo.txt")) { Thread.Sleep(1500); } 
                    servidor.deleteFrom(PICKING, "Tiempo.txt");
                    if (actualPicking < orden.getCantidad()+1) { actualPicking++; }
                    else { estaciones[PICKING].setStatus(estaciones[PICKING].READY); }
                    if (senderPicking.IsAlive)
                    { senderPicking.Interrupt(); }
                    else { senderPicking.Start(); }
                    Thread.Sleep(Timeout.Infinite);
                }
                catch (ThreadInterruptedException e)
                {

                }
            }
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (senderPicking.IsAlive) { senderPicking.Abort(); }
            if (receiverPicking.IsAlive) { receiverPicking.Abort(); }
            if (updater.IsAlive) { updater.Abort(); }
            if (connecter.IsAlive) { connecter.Abort(); }
        }

        private void timer8_Tick(object sender, EventArgs e)
        {
            timerMaster.Start();
            TimeSpan ts = timerMaster.Elapsed;
            String tiempo = string.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
            masterTime.Text = tiempo;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (completados <= 0)
            {
                MessageBox.Show("Hubo un reingreso de la estación 5 a la 4.", "Aviso");
            }
            else
            {/*
                OrdenForm h = new OrdenForm("Pendientes para la salida");
                Orden o = new Orden();

                h.setOrden(o);
             */
            }
        }

        private void setPorcentaje(int porcentaje)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.progressBar1.InvokeRequired)
            {
                setPorcentajeCallback d = new setPorcentajeCallback(setPorcentaje);
                this.Invoke(d, new object[] { porcentaje });
            }
            else
            {
                this.progressBar1.Value = porcentaje;
            }
        }

        private delegate void setPorcentajeCallback(int a);

        private void autorizados2operariosBase()
        {
            String[] nuevasLineas = new String[50];
            if (System.IO.File.Exists(recursos + "/operariosbase.txt"))
            {
                System.IO.File.Delete(recursos + "/operariosbase.txt");
            }
            System.IO.StreamReader file0 =
                new System.IO.StreamReader(recursos + "/Operarios.txt");
            int cant = 0;
            String line;
            while ((line = file0.ReadLine()) != null)
            {
                nuevasLineas[cant] = line; cant++;
            }
            file0.Close();
            System.IO.StreamWriter file =
                new System.IO.StreamWriter(recursos + "/operariosbase.txt");
            String[] cadenas;
            for (int i = 0; i < cant; i++)
            {
                //Empanada 1 2016
                cadenas = nuevasLineas[i].Split(';');
                if (Int32.Parse(cadenas[3]) > 1) { 
                    // operarios = (0:Nom, 1:epc, 2:cod, 3:estacion)
                    // operariosbase= (0:epc, 1:estación, 2:nombre, 3:"código") .-.
                    file.WriteLine(cadenas[1] + ";" + "" + (Int32.Parse(cadenas[3]) - 1) + ";" + cadenas[0] + ";" + cadenas[2]);
                }
            }
            file.Close();
        }

        private void verPedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ordenSelected)
            {
                OrdenForm h = new OrdenForm();
                h.setOrden(this.orden, recursos);
                h.Show();
            }
            else
            {
                MessageBox.Show("No hay orden seleccionada. Seleccione primero una orden de pedido.", "Aviso");
            }

        }

        private void comenzarProcesoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ordenSelected && allConnected && allStations)
            {
                comenzarProceso();
            }
            else
            {
                String Mensaje = "No se puede iniciar el proceso. No se cumplen las condiciones: \n\n";
                if (!ordenSelected)
                {
                    Mensaje += "→ No se ha seleccionado una orden de productos.\n";
                }
                if (!allConnected)
                {
                    Mensaje += "→ No se han conectado todas las estaciones.\n";
                }
                if (!allStations)
                {
                    Mensaje += "→ No hay un usuario autorizado en cada estación\n";
                }
                MessageBox.Show(Mensaje, "Error");
            }
        }

        private void nuevoPedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!processing)
            {
                VerOrdenes h = new VerOrdenes(recursos);
                DialogResult r = h.ShowDialog();
                if (r == DialogResult.OK)
                {
                    ordenSelected = true;
                    loadOrder();
                }
                else
                {
                    ordenSelected = false;
                }
            }
            else
            {
                MessageBox.Show("No puede modificar las ordenes cuando ya hay una en proceso.", "Aviso");
            }
        }

        private void portalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            portal h = new portal();
            h.Show();
        }
        
        private void operariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VerOperarios h = new VerOperarios(recursos);
            h.Show();
        }

        private void checkEstaciones()
        {
           String[] newLineas = new String[50];
            for (int i = 1; i <= 7; i++) {
                servidor.receiveFrom(red, recursos, "/estacion" + i + ".txt");
                System.IO.StreamReader file0 =
                 new System.IO.StreamReader(recursos + "/estacion" + i + ".txt");
                int cant2 = 0;
                String line;
                while ((line = file0.ReadLine()) != null)
                {
                    newLineas[cant2] = line; cant2++;
                }
                file0.Close();
                String[] tipo;
                tipo = newLineas[0].Split(';');
                if (tipo[1]=="avion")
                {estaciones[i].setStatus(0);}
                if (tipo[1] == "carro")
                { estaciones[i].setStatus(1); }
                if (tipo[1] == "dinosaurio")
                { estaciones[i].setStatus(2); }
            }
        }



        private void verLecturasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            portal h = new portal();
            h.Show();
        }
    }
}
