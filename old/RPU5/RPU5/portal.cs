using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using Impinj.OctaneSdk;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace portalesrcl
{
    public partial class portal : Form
    {

        //Declaracion de Variables, Strings[]
        public static ImpinjReader reader = new ImpinjReader();
        public static string[] EpcLeidos = new string[135];
        public static string[] EpcLeidossalida = new string[135];
        public static string[] EpcLeidosNoAutorizados = new string[100];
        public static string[] EpcLeidosNoAutorizadosSalida = new string[135];
        public static string[] EpcLeidos2 = new string[135];
        public static string[] EpcLeidos2Salida = new string[135];
        public static string[] EpcLeidos3 = new string[135];
        public static string[] EpcLeidos3Salida = new string[135];
        public static string[] Productos = new string[200];
        public static string[] Productos2 = new string[200];
        public string[] DatosProductos2 = new string[3];
        public static int i = 0;
        public static int ii = 0;
        public static int cero = 0;
        public static int n = 0;
        public static int y = 0;
        public static int x = 0;
        public static int nn = 0;
        public static int na = 0;
        public static string[] EpcLeidos4 = new string[135];
        public static string[] EpcLeidos22 = new string[135];
        public static string[] EpcLeidos32 = new string[135];
        public static string[] EpcLeidosNoAutorizados2 = new string[100];
        public static int b = 0;
        public static int y2 = 0;
        public static string[,] fecha = new string[135, 2];
        public static string[] portalesrcl = new string[135];
        public static string[] portalesrcl4 = new string[135];


        // VARIABLES ESTACIONES


        //public static List<ImpinjReader> readers = new List<ImpinjReader>();

        public static string[,] LineasEstaciones = new string[8, 16];
        public static string[,] Lineas_Epc_Quitados = new string[8, 16];
        public static string[,] TiemposAuxiliaresEstaciones = new string[8, 11];
        public static string[,] TiemposFinalesEstaciones = new string[8, 11];

        public static string[] OperariosAutorizados = new string[8];
        public static string[] ListaProductos = new string[50];
        public static string[] DatosProductos = new string[3];
        //public static string[] LineasEstacionesReingreso = new string[ 21 ]; // CAMBIAR POR MATRIZ
        public static string[,] LineasEstacionesReingreso = new string[8, 16];
        public static string[] Ultima_Estacion = new string[31];

        public static string Viene_Estacion_Anterior = null;
        public static string EstacionSaliente = null;
        public static string Epc_Reingreso;
        public static string ReingresoMalo = null;
        public static string TagAgain = null;
        public static string antena = null;
        public static string epc = null;
        public static string ruta;
        public static int Contador_Ultima_Estacion = 1;
        public static int CantidadProductos = 0;
        public static int SegundosDelDia = 0;

        public static string LabelLectores = null;


        public portal()
        {

            InitializeComponent();
            timer1.Enabled = true;     //Habilita el Timer1 siempre al inicio  
            timer2.Enabled = true;
            ActualizarProductosPortales();
            // ESTACIONES 

            ruta = (@"\\USER\Users\Public\RFID\estaciones\textos\/");


            ActualizarProductos();
            Inicializar();
            //ConfigurarLectores();

        }


        public void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //Crea una lista de los lectores a conectar
        static List<ImpinjReader> readers = new List<ImpinjReader>();

        private void btnStart_Click(object sender, EventArgs e)
        {

            try
            {
                Console.WriteLine("ConectandoLectores..");

                readers.Add(new ImpinjReader("192.168.0.44", "Lector1")); //AQUI SE CAMBIA LA IP (192.168.1.42 o 192.168.1.44)
                readers.Add(new ImpinjReader("192.168.0.42", "Lector2"));//Agrega lectores usando IP y se le da un nombre

                foreach (ImpinjReader reader in readers)
                {


                    if (reader.Name.ToString() == "Lector1" || reader.Name.ToString() == "Lector2")
                    {
                        reader.Connect();

                        //Conecta los lectores y configura caracteristicas internas como potencia, No. de , etc...
                        //Potencia
                        Settings settings = reader.QueryDefaultSettings();
                        settings.Report.IncludeFirstSeenTime = true;
                        settings.Report.IncludeLastSeenTime = true;
                        settings.Report.IncludeSeenCount = true;


                        settings.Antennas.GetAntenna(1).IsEnabled = true;
                        settings.Antennas.GetAntenna(2).IsEnabled = true;
                        settings.Antennas.GetAntenna(3).IsEnabled = true;
                        settings.Antennas.GetAntenna(4).IsEnabled = true;

                        // Assign the TagsReported event handler.
                        settings.Report.IncludeAntennaPortNumber = true;

                        // This specifies Tx power in Dbm
                        settings.Antennas.GetAntenna(1).TxPowerInDbm = 25;
                        settings.Antennas.GetAntenna(2).TxPowerInDbm = 25;
                        settings.Antennas.GetAntenna(3).TxPowerInDbm = 25;
                        settings.Antennas.GetAntenna(4).TxPowerInDbm = 25;

                        //Especifica un tiempo de retardo en la lectura
                        //Mejor Vizualizacion
                        settings.Report.Mode = ReportMode.BatchAfterStop;
                        settings.AutoStart.Mode = AutoStartMode.Periodic;
                        settings.AutoStart.PeriodInMs = 600;
                        settings.AutoStop.Mode = AutoStopMode.Duration;
                        settings.AutoStop.DurationInMs = 600;

                        //when tags reports are available.
                        reader.TagsReported += OnTagsReported;

                        // Apply the default settings.
                        reader.ApplySettings(settings);

                        // Start reading.
                        reader.Start();
                    }
                    label13.Text = "Lectores Portal Conectados";
                }

            }

            //Captura errores en el try y los muestra
            catch (OctaneSdkException ee)
            {
                Console.WriteLine("{0}", ee.Message);
            }

            catch (Exception ee)
            {
                Console.WriteLine("{0}", ee.Message);
            }
        }

        static void OnTagsReported(ImpinjReader sender, TagReport report)
        {
            //Se llama para cada lector

            string[] DatosProductos2 = new string[3];

            foreach (Tag tag in report)
            {
                if (sender.Name.ToString() == "Reader #1" || sender.Name.ToString() == "Reader #2")
                {  // CODIGO ESTACIONES

                    //Console.WriteLine("SI ENTRA EN LOS LECTORES ESTACIONES");
                    
                    epc = tag.Epc.ToString();
                    
                    if (sender.Name.ToString() == "Reader #1")
                    { antena = tag.AntennaPortNumber.ToString(); }
                    else if (sender.Name.ToString() == "Reader #2")
                    { antena = Convert.ToString(tag.AntennaPortNumber + 4); }
                    //Console.WriteLine("-------------------------------------------------------"); // SEGUIMIENTO
                    //Console.WriteLine("EPC : {0} , Antenna : {1}", epc, antena);
                    if (antena == "7")
                    {
                        Escribir_Ultima_Estacion();
                    }

                    if (epc[0] == '0')
                    {
                        AutorizarOperarios();
                    }
                    else
                    {
                        EtiquetaAgain();
                        //Console.WriteLine( "---------------------" ); // SEGUIMIENTO
                        //Console.WriteLine( "ETIQUETA AGAIN?  : " + TagAgain ); // SEGUIMIENTO
                        if (TagAgain == "no")
                        {
                            Verificar_Viene_Estacion_Anterior(); // Verifica si el producto nuevo es de reingreso o de la estacion anterior
                                                                 //Console.WriteLine( "---------------------" ); // SEGUIMIENTO
                                                                 //Console.WriteLine( "VIENE DE LA ESTACION ANTERIOR?  : " + Viene_Estacion_Anterior ); // SEGUIMIENTO
                            if (Viene_Estacion_Anterior == "no")
                            {
                                Verificar_Reingreso();
                                //Console.WriteLine( "---------------------" ); // SEGUIMIENTO
                                //Console.WriteLine( "ES UN REINGRESO MALO?  : " + ReingresoMalo ); // SEGUIMIENTO
                            }
                            EscribirEstacionActual();
                            BorrarEpc();
                        }
                    }

                }
                else
                { // CODIGO PORTALES
                    Console.WriteLine(sender.Name.ToString());
                    string nombre_lector = sender.Name;
                    //Console.WriteLine( nombre_lector );

                    if (nombre_lector == "Lector1") //Si los reportes vienen del Lector 1 se ejecuta
                    {
                        //Console.WriteLine( "EPC : {0} " , tag.Epc );

                        string lines = tag.Epc.ToString(); //EPC
                        string yaleido = "na"; //Por definicion es no autorizado
                        string a = "no";


                        for (int j = 0; j <= 134; j++) //Compara si el epc leido ya se leyo dentro de los autorizados
                        {
                            if (lines == EpcLeidos2[j])
                            {
                                yaleido = "si";
                            }
                        }


                        for (int j = 0; j <= 134; j++)

                        {
                            if (lines == EpcLeidos3[j])
                            {
                                yaleido = "si";
                            }

                        }

                        //Comportalesrcl si la epc esta dentro de los productos Autorizados o si es un producto No Autorizado
                        if (yaleido == "na")
                        {
                            for (int l = 0; l <= 104; l++)
                            {
                                a = "no";
                                DatosProductos2 = Productos[l].Split(';');
                                if (DatosProductos2[0] == lines)
                                {
                                    yaleido = "no";
                                    l = 200;
                                }
                            }

                            string[] datosxx = new string[4];

                            for (int na = 0; na <= 104; na++)
                            {
                                //Console.WriteLine( "prod: " + Productos[ na ] );

                                datosxx = Productos[na].Split(';');
                                if (datosxx[0] == lines)
                                {
                                    a = "si";
                                }
                            }

                            //Comportalesrcl si el epc no autorizado la los escribio para no repetirlo
                            string repetido = "no";
                            if (a == "no")
                            {
                                for (int bv = 0; bv <= 99; bv++)
                                {
                                    if (EpcLeidosNoAutorizados[bv] == lines)
                                    {
                                        repetido = "si";
                                        break;
                                    }

                                }

                                if (repetido == "no")
                                {
                                    EpcLeidosNoAutorizados[y] = lines;
                                    y++;
                                    yaleido = "na";
                                }
                            }
                        }

                        //Se determino que el epc es autorizado y que no se ha escrito
                        if (yaleido == "no")
                        {
                            for (int l = 0; l <= 200; l++)
                            {
                                try
                                {
                                    DatosProductos2 = Productos[l].Split(';');
                                    if (DatosProductos2[0] == lines)
                                    {
                                        EpcLeidos[i] = DatosProductos2[0]; //Guarda el epc en un string[] que a la vez llena el textBox1 con EPC y Fecha;Hora
                                        EpcLeidos2[i] = lines;
                                        string portalesrcl2 = EpcLeidos[i];
                                        portalesrcl[i] = portalesrcl2 + "     " + DateTime.Now.ToString("dd/MM/dddd;hh:mm:ss");
                                        break;
                                    }
                                }
                                catch
                                {

                                }

                            }
                            i = i + 1;
                        }

                    }

                    //Se pregunta si el reporte viene del Lector 2(Salidaportal)
                    if (nombre_lector == "Lector2")
                    {
                        //Console.WriteLine( "EPC : {0} " , tag.Epc );

                        string lines = tag.Epc.ToString();
                        string yaleido = "na";
                        string a = "no";

                        //Comportalesrcl si ya se escribio el EPC
                        for (int j = 0; j <= 134; j++)
                        {
                            if (lines == EpcLeidos22[j])
                            {
                                yaleido = "si";
                            }
                        }

                        for (int j = 0; j <= 134; j++)
                        {
                            if (lines == EpcLeidos32[j])
                            {
                                yaleido = "si";
                            }
                        }


                        if (yaleido == "na")
                        {
                            for (int l = 0; l <= 99; l++) //Verifica si el epc reportado se encuentra en la lista de Productos Finalizados autorizados a salir
                            {
                                a = "no";
                                DatosProductos2 = Productos2[l].Split(';');
                                if (DatosProductos2[0] == lines)
                                {
                                    yaleido = "no";
                                    l = 200;

                                }



                            }
                            string[] datosxx = new string[4];

                            for (int na = 0; na <= 104; na++)
                            {
                                //Console.WriteLine( "prod: " + Productos2[ na ] );

                                datosxx = Productos2[na].Split(';');
                                if (datosxx[0] == lines)
                                {
                                    a = "si";
                                }
                            }

                            string repetido = "no";
                            if (a == "no")
                            {
                                for (int bv = 0; bv <= 99; bv++) //Comportalesrcl si esta escrito en lista de no autorizados
                                {
                                    if (EpcLeidosNoAutorizados2[bv] == lines)
                                    {
                                        repetido = "si";
                                        break;
                                    }

                                }
                                if (repetido == "no") //Escribe el Epc si es no autorizado y no se ha escrito
                                {
                                    EpcLeidosNoAutorizados2[y2] = lines;
                                    y2++;//Guarda un contador de los no autorizados
                                    yaleido = "na";


                                }
                            }
                        }

                        if (yaleido == "no")//Verifica que este en la lista de productos autorizados a salir
                        {
                            for (int l = 0; l <= 200; l++)
                            {
                                try
                                {
                                    DatosProductos2 = Productos2[l].Split(';'); //Se usa para colocar un nombre a un epc especifico(Definido por base de datos)
                                    if (DatosProductos2[0] == lines)
                                    {
                                        EpcLeidos22[b] = lines; //Escribe el epc autorizado para salir y lo guarda para comprobar si se repite futuramente
                                        EpcLeidos32[b] = lines;
                                        string portalesrcl3 = EpcLeidos32[b];
                                        portalesrcl4[b] = portalesrcl3 + "     " + DateTime.Now.ToString("dd/MM/dddd;hh:mm:ss"); //Se utiliza para asociar a un EPC o producto especifico una fecha y hora de salida
                                        break;
                                    }
                                }
                                catch
                                {

                                }

                            }
                            b = b + 1; //Este es el contador de EPC autorizados que salieron
                        }

                    }

                }

            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void botonstop_Click(object sender, EventArgs e)
        {
            foreach (ImpinjReader reader in readers)
            {
                if (reader.Name.ToString() == "Lector1" || reader.Name.ToString() == "Lector2")
                {
                    // Stop reading.
                    reader.Stop();

                    // Disconnect from the reader.
                    reader.Disconnect();
                }
            }
            label13.Text = "Lectores Portal Desconectados";
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            //Asigna inicialmente los valores que va a tomar cada componente en la interfaz cuando cambie
            textBox1.Lines = portalesrcl;
            advertencia.Lines = EpcLeidosNoAutorizados;
            textBox2.Lines = EpcLeidosNoAutorizadosSalida;
            cantidad.Text = i.ToString();
            totalna.Text = y.ToString();
            textBox2.Lines = EpcLeidosNoAutorizados2;
            label5.Text = b.ToString();
            textBox3.Lines = portalesrcl4;
            totalna2.Text = y2.ToString();
        }

        private void actualizate_Click(object sender, EventArgs e)
        {
            ActualizarProductosPortales();
        }

        static void ActualizarProductosPortales()
        {
            //Lee un archivo que viene de base de datos con productos autorizados a entrada y salida
            StreamReader productosbase;
            StreamReader productosbase2;

            string rutaproductos = @"\\USER\Users\Public\RFID\estaciones\textos\" + "piezas.txt";
            string rutaproductos2 = @"\\USER\Users\Public\RFID\estaciones\textos\" + "piezas2.txt";

            while (true)
            {
                try
                {
                    productosbase = new StreamReader(@rutaproductos);
                    break;
                }
                catch
                {


                }
            }

            while (true)
            {
                try
                {
                    productosbase2 = new StreamReader(@rutaproductos2);
                    break;
                }
                catch
                {

                }
            }

            //Lee los archivos linea a linea y los guarda en vectores (Productos[] y Productos2[])
            int k = 0;
            while (!productosbase.EndOfStream)

            {
                Productos[k] = productosbase.ReadLine();
                k = k + 1;
            }

            int k2 = 0;
            while (!productosbase2.EndOfStream)
            {
                Productos2[k2] = productosbase2.ReadLine();
                k2 = k2 + 1;
            }
        }



        private void advertencia_TextChanged(object sender, EventArgs e)
        {

        }

        private void limpia_Click(object sender, EventArgs e)
        {
            //asigna nuevamente strings[] en blanco para limpiar valores
            string[] textBox1 = new string[135];
            string[] advertencia = new string[100];
            string[] EpcLeidosNoAutorizados = new string[100];
            string[] EpcLeidos = new string[135];
            string[] EpcLeidos2 = new string[135];
            i = 0;//Contadores en cero
            y = 0;
        }

        private void cantidad_Click(object sender, EventArgs e)
        {

        }

        private void portal_Load(object sender, EventArgs e)
        {

        }
        private void inventario_Click(object sender, EventArgs e)
        {
            //Guarda la lista actual de productos autorizados para entrar en un archivo .txt para Base de datos
            StreamWriter productosinventario;


            string rutaproductosinventario = @"\\USER\Users\Public\RFID\estaciones\textos\" + "piezasinventario.txt";


            while (true)
            {
                try
                {
                    productosinventario = new StreamWriter(@rutaproductosinventario);
                    break;
                }
                catch
                {


                }

            }
            for (int g = 0; g <= i; g++)
            {
                productosinventario.WriteLine(EpcLeidos2[g]);
            }
            productosinventario.Close();
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }
        private void actualizatesalida_Click(object sender, EventArgs e)
        {
            //Lee el archivo de productos autorizados para salir generado por la base de datos
            StreamReader productosbasesalida;
            string rutaproductosterminados = @"\\USER\Users\Public\RFID\estaciones\textos\" + "piezasterminadas.txt";

            while (true)
            {
                try
                {
                    productosbasesalida = new StreamReader(@rutaproductosterminados);
                    break;
                }
                catch
                {


                }
            }
            int k = 0;
            while (!productosbasesalida.EndOfStream)

            {
                Productos2[k] = productosbasesalida.ReadLine();
                k = k + 1;
            }
        }
        private void limpiasalida_Click(object sender, EventArgs e)
        {
            b = 0;
            y2 = 0;

        }
        private void inventario2_Click(object sender, EventArgs e)
        {
            //Guarda un archivo .txt de los productos autorizados que salieron
            StreamWriter productosinventario2;


            string rutaproductosinventario2 = @"\\USER\Users\Public\RFID\estaciones\textos\" + "piezasinventario2.txt";


            while (true)
            {
                try
                {
                    productosinventario2 = new StreamWriter(@rutaproductosinventario2);
                    break;
                }
                catch
                {


                }

            }
            for (int g = 0; g <= b; g++)
            {
                productosinventario2.WriteLine(EpcLeidos22[g]);
            }
            productosinventario2.Close();
        }
        // FUNCIONES ESTACIONES
        static void Escribir_Txt_Estaciones(int est)// ESCRIBIR EN EL TXT DE LA ESTACION CORRESPONDIENTE 
        {
            StreamWriter estacionactualescc;

            string rutaestacionactual = ruta + "estacion" + est + ".txt";
            //Console.WriteLine( "LA ESTACION QUE SE ESCRIBIRA ES:  " +est); // SEGUIMIENTO

            while (true)
            {
                try
                {
                    estacionactualescc = new StreamWriter(@rutaestacionactual); break;
                }
                catch { }
            }


            for (int i = 1; i <= 15; i++)
            {
                estacionactualescc.WriteLine(LineasEstaciones[est, i]);
            }
            estacionactualescc.Close(); // ESCRIBE EN EL TXT LAS LINEAS
        }
        static void EscribirEstacionActual()// ORGANIZA LAS LINEAS DE LAS ESTACIONES QUE SERAN ESCRITAS EN EL TXT
        {

            //Console.WriteLine( "ENTRA ESCRIBIR ESTACION ACTUAL" ); // SEGUIMIENTO

            int LineaDisponible = 0; // QUE LINEA ESTA DISPONIBLE PARA ESCRIBIR
            int LineaPrioridad = 0; // QUE LINEA TIENE LA PRIORIDAD
            int LineasUsadas = 0;  // CANTIDAD DE LINEA USADAS

            int Cambio = 0;

            string[] DatosEstacionActual = new string[3];
            string[] DatosProductos2 = new string[3];
            string[] DatosActualizar = new string[3];


            for (int i = 1; i <= 10; i++)
            {
                DatosEstacionActual = LineasEstaciones[Convert.ToUInt16(antena), i].Split(';');
                // [0]= EPC     |        [1]= TIPO      |        [2]= PRIORIDAD

                if (DatosEstacionActual[0] != "nada")
                {
                    LineasUsadas++; // Cantidad de lineas usadas       
                }

                if (DatosEstacionActual[0] == "nada" && Cambio == 0)
                {
                    Cambio = 1;
                    LineaDisponible = i; // Primera linea disponible en el txt
                }

                if (DatosEstacionActual[2] == "conprioridad")
                {
                    LineaPrioridad = i; // Linea que tiene la prioridad
                }
            }

            if (ReingresoMalo == "si")
            {
                GuardarReingreso();
                //Console.WriteLine( "SI ENTRA AQUI Y LA ESTACION SALIENTE ES: " + EstacionSaliente ); // SEGUIMIENTO 
                for (int i = 1; i <= 10; i++)
                {
                    DatosActualizar = LineasEstaciones[Convert.ToUInt16(EstacionSaliente), i].Split(';');
                    // [0]=epc    |     [1]=tipo(carro,etc)     |     [2]=prioridad     |     [3]=Segundos de la hora de ingreso
                    if (DatosActualizar[0] == epc)
                    {
                        //Console.WriteLine( "SI ENTRO A ACTUALIZAR LOS DATOSSSS" );
                        DatosProductos[0] = DatosActualizar[0];
                        DatosProductos[1] = DatosActualizar[1];
                        //Viene_Estacion_Anterior = "si";
                        break;
                    }
                }
            }

            if (LineasUsadas == 0)
            {
                LineasEstaciones[Convert.ToUInt16(antena), 1] = DatosProductos[0] + ';' + DatosProductos[1] + ';' + "conprioridad";

            }
            else
            {
                //Console.WriteLine("Vino de la anterior?  :  " + Viene_Estacion_Anterior);
                if (Viene_Estacion_Anterior == "no")
                {
                    //Console.WriteLine( "ENTRO EN VIENE ESTACION ANTERIOR = NO" ); 

                    LineasEstaciones[Convert.ToUInt16(antena), LineaDisponible] = DatosProductos[0] + ';' + DatosProductos[1] + ';' + "conprioridad";

                    DatosProductos2 = LineasEstaciones[Convert.ToUInt16(antena), LineaPrioridad].Split(';');
                    // DATOS DE LA LINEA QUE TENIA LA PRIORIDAD

                    if (ReingresoMalo == "si")
                    {
                        Guardar_Epc_Quitado_Reingreso(DatosProductos2[0]); // GUARDAR EL EPC AL QUE SE LE QUITO LA PRIORIDAD
                    }

                    LineasEstaciones[Convert.ToUInt16(antena), LineaPrioridad] = DatosProductos2[0] + ';' + DatosProductos2[1] + ';' + "sinprioridad";

                    Epc_Reingreso = DatosProductos2[0];

                }
                else
                {
                    LineasEstaciones[Convert.ToUInt16(antena), LineaDisponible] = DatosProductos[0] + ';' + DatosProductos[1] + ';' + "sinprioridad";
                }
            }

            //Console.WriteLine( "EPC ESCRITO: " + DatosProductos[ 0 ] + " PRODUCTO: " + DatosProductos[ 1 ] ); // SEGUIMIENTO

            //Console.WriteLine( "SALE ESCRIBIR ESTACION ACTUAL" ); // SEGUIMIENTO

            Escribir_Txt_Estaciones(Convert.ToUInt16(antena));

        }
        static void Verificar_Viene_Estacion_Anterior() // VERIFICA SI EL EPC LEIDO VIENE DE LA ESTACION ANTERIOR
        {
            //Console.WriteLine( "ENTRA A VERIFICAR VIENE ESTACION ANTERIOR" ); // SEGUIMIENTO 

            string[] DatosEstacionAnterior = new string[4];
            Viene_Estacion_Anterior = "no";

            if (antena != "1")
            {
                for (int i = 1; i <= 10; i++)
                {
                    DatosEstacionAnterior = LineasEstaciones[Convert.ToUInt16(antena) - 1, i].Split(';');
                    // [0]=epc    |     [1]=tipo(carro,etc)     |     [2]=prioridad     |     [3]=Segundos de la hora de ingreso

                    if (DatosEstacionAnterior[0] == epc)
                    {
                        DatosProductos[0] = DatosEstacionAnterior[0];
                        DatosProductos[1] = DatosEstacionAnterior[1];
                        Viene_Estacion_Anterior = "si";
                        break;
                    }
                }
            }
            else // ANTENA = 1
            {
                //Console.WriteLine( "si entra antenaaaaaaa=1" );
                //Console.WriteLine( "Contador: " + CantidadProductos );
                for (int i = 1; i <= CantidadProductos; i++)
                {
                    DatosProductos = ListaProductos[i].Split(';'); //   [0]=epc       [1]=tipo( carro, etc)  [2]=Si ha sido leido
                    //Console.WriteLine("epc producto: " + DatosProductos[ 0 ] + "  producto: " + DatosProductos[ 1 ] + " leido: " + DatosProductos[ 2 ] );
                    if (DatosProductos[0] == epc)
                    {
                        //Console.WriteLine( "si son iguales: " + DatosProductos[2] );
                        if (DatosProductos[2] == "noleido")
                        {
                            ListaProductos[i] = DatosProductos[0] + ';' + DatosProductos[1] + ';' + "sileido";
                            Viene_Estacion_Anterior = "si";
                        }
                        break;
                    }
                }

            } // CIERRA ELSE

            //Console.WriteLine( "SALE DE VERIFICAR VIENE ESTACION ANTERIOR" ); // SEGUIMIENTO 
        }
        static void Verificar_Reingreso()
        {
            //Console.WriteLine( "ENTRA A VERIFICAR REINGRESO" ); // SEGUIMIENTO

            string[] DatosReingreso = new string[3];
            int cambio = 0;

            ReingresoMalo = "si";
            /*
            for ( int i=1 ;i<=20 ;i++ )
            {
                DatosReingreso = LineasEstacionesReingreso[ i ].Split( ';' );

                if ( epc==DatosReingreso[0])
                {
                    ReingresoMalo = "no";
                    break;
                }
            }
            */

            for (int i = Convert.ToUInt16(antena); i <= 7; i++)
            {
                for (int j = 1; j <= 15; j++)
                {
                    DatosReingreso = LineasEstacionesReingreso[i, j].Split(';');
                    if (epc == DatosReingreso[0])
                    {
                        ReingresoMalo = "no";
                        cambio = 1;
                        break;
                    }
                    //Console.WriteLine( "LINEA ESTACIONES REINGRESO: " + LineasEstacionesReingreso[ i,j ] ); // SEGUIMIENTO
                }
                if (cambio == 1)
                {
                    break;
                }
            }

            //Console.WriteLine( "SALE DE VERIFICAR REINGRESO" ); // SEGUIMIENTO
        }
        static void GuardarReingreso()// GUARDAR EL EPC DE REINGRESO Y LAS ESTACIONES INVOLUCRADAS EN EL REINGRESO
        {
            //Console.WriteLine( "ENTRA A GUARDAR REINGRESO" ); // SEGUIMIENTO ¡¡¡¡ahí va la serpiente de tierra caliente!!!!!

            string[] DatosReingreso = new string[4];
            EstacionSaliente = null;
            int cambio = 0;

            for (int i = 1; i <= 7; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    DatosReingreso = LineasEstaciones[i, j].Split(';');
                    if (epc == DatosReingreso[0])
                    {
                        EstacionSaliente = i.ToString();
                        break;
                    }
                }
                if (EstacionSaliente != null)
                { break; }
            }

            /*
            for ( int i = 1 ; i <= 20 ; i++ ) // CAMBIAR POR MATRIZ
            {
            if ( LineasEstacionesReingreso[i]=="nada;nada;nada")
            {
            //Console.WriteLine( "si entra en la asignacion de reingreso malo" );
            LineasEstacionesReingreso[ i ] = epc + ';' + antena + ';' + EstacionSaliente;
            //Console.WriteLine( "la linea escrita del reingreso malo es" + LineasEstacionesReingreso[i] ); // SEGUIMIENTO
            break;
            }
            }
            */


            for (int i = 1; i <= 7; i++) // CAMBIAR POR MATRIZ
            {
                for (int j = 1; j <= 15; j++)
                {
                    if (LineasEstacionesReingreso[Convert.ToUInt16(antena), j] == "nada;nada") ;
                    {
                        LineasEstacionesReingreso[Convert.ToUInt16(antena), j] = epc + ';' + EstacionSaliente;
                        cambio = 1;
                        break;
                    }
                }
                if (cambio == 1)
                {
                    break;
                }
            }


            // Console.WriteLine( "El epc: " + epc + ", Entro a la estacion : " + antena + ", Proveniente de la estacion: " + EstacionSaliente ); // SEGUIMIENTO

            //Console.WriteLine( "SALE GUARDAR REINGRESO" ); // SEGUIMIENTO

        }
        static void Guardar_Epc_Quitado_Reingreso(string epcquitado) // GUARDAR EL EPC AL QUE SE LE QUITO LA PRIORIDAD
        {

            for (int i = 1; i <= 15; i++)
            {
                if ((Lineas_Epc_Quitados[Convert.ToUInt16(antena), i].Split(';'))[0] == "nada")
                {
                    Lineas_Epc_Quitados[Convert.ToUInt16(antena), i] = epcquitado;
                }
            }

        }
        static void BorrarEpc()// BORRAR EL EPC DE LA ESTACION QUE SALIO 
        {

            string[] DatosActualizar = new string[3];
            int cambio = 0;

            //Console.WriteLine( "ENTRA A BORRAR EPC" ); // SEGUIMIENTO 

            //Console.WriteLine("estacion anterior: " + Viene_Estacion_Anterior ); // SEGUIMIENTO

            //Console.WriteLine("reingreso malo en borrar es : " +  ReingresoMalo ); // SEGUIMIENTO

            string[] DatosBorrarEpc = new string[4];
            int Antena_Borrar = 0;

            string rutaestacionactual = ruta + "estacion" + (Antena_Borrar) + ".txt";


            if (Viene_Estacion_Anterior == "si")
            {
                if (antena != "1")
                {
                    Antena_Borrar = Convert.ToUInt16(antena) - 1;
                }


                for (int i = 1; i <= 10; i++)
                {
                    DatosBorrarEpc = LineasEstaciones[Antena_Borrar, i].Split(';');
                    // [0]= EPC     |        [1]= TIPO      |        [2]= PRIORIDAD

                    if (DatosBorrarEpc[0] == epc)
                    {
                        LineasEstaciones[Antena_Borrar, i] = "nada;nada;nada";
                        Organizar_Lineas_Estaciones(Antena_Borrar);

                        if (DatosBorrarEpc[2] == "conprioridad")
                        {
                            DatosBorrarEpc = LineasEstaciones[Antena_Borrar, 1].Split(';');

                            if (DatosBorrarEpc[0] != "nada")
                            {
                                LineasEstaciones[Antena_Borrar, 1] = DatosBorrarEpc[0] + ';' + DatosBorrarEpc[1] + ";conprioridad";
                            }
                            else
                            {
                                LineasEstaciones[Antena_Borrar, 1] = DatosBorrarEpc[0] + ';' + DatosBorrarEpc[1] + ";nada";
                            }
                        }
                        break;
                    }

                }

            }

            if (ReingresoMalo == "si")
            {
                Antena_Borrar = Convert.ToUInt16(EstacionSaliente);

                //Console.WriteLine( "SI ENTRA EN EL REINGRESO MALO DE BORRAR EL EPC, antena saliente :" + Antena_Borrar ); // SEGUIMIENTO 

                for (int i = 1; i <= 10; i++)
                {
                    DatosBorrarEpc = LineasEstaciones[Antena_Borrar, i].Split(';');
                    // [0]= EPC     |        [1]= TIPO      |        [2]= PRIORIDAD

                    if (DatosBorrarEpc[0] == epc)
                    {
                        //Console.WriteLine( "lo que se borrará sera: " + LineasEstaciones[Antena_Borrar,i] );


                        LineasEstaciones[Antena_Borrar, i] = "nada;nada;nada";
                        Organizar_Lineas_Estaciones(Antena_Borrar);


                        if (DatosBorrarEpc[2] == "conprioridad")
                        {
                            DatosBorrarEpc = LineasEstaciones[Antena_Borrar, 1].Split(';');

                            if (DatosBorrarEpc[0] != "nada")
                            {
                                LineasEstaciones[Antena_Borrar, 1] = DatosBorrarEpc[0] + ';' + DatosBorrarEpc[1] + ";conprioridad";
                            }
                            else
                            {
                                LineasEstaciones[Antena_Borrar, 1] = DatosBorrarEpc[0] + ';' + DatosBorrarEpc[1] + ";nada";
                            }
                        }

                        break;

                    }

                } // CIERRA FOR 10
            } // CIERRA REINGRESO MALO 

            if (ReingresoMalo == "no")  // REINGRESO = "NO"
            {

                /*
                for ( int i = 1 ; i <= 20 ; i++ )
                {
                DatosBorrarEpc = LineasEstacionesReingreso[ i ].Split( ';' );
                
                if ( DatosBorrarEpc[ 0 ] == epc )
                {
                Antena_Borrar = Convert.ToUInt16( DatosBorrarEpc[ 1 ] );
                break;
                }
                } // CIERRA FOR 20 
                */

                for (int i = 1; i <= 7; i++) // CAMBIAR POR MATRIZ
                {
                    for (int j = 1; j <= 15; j++)
                    {
                        DatosBorrarEpc = LineasEstacionesReingreso[i, j].Split(';');
                        if (DatosBorrarEpc[0] == epc)
                        {
                            cambio = 1;
                            Antena_Borrar = i;
                            break;
                        }
                    }
                    if (cambio == 1)
                    {
                        break;
                    }
                }





                for (int i = 1; i <= 10; i++)
                {
                    DatosBorrarEpc = LineasEstaciones[Antena_Borrar, i].Split(';');
                    // [0]= EPC     |        [1]= TIPO      |        [2]= PRIORIDAD

                    if (DatosBorrarEpc[0] == epc)
                    {
                        LineasEstaciones[Antena_Borrar, i] = "nada;nada;nada";
                        Organizar_Lineas_Estaciones(Antena_Borrar);

                        if (DatosBorrarEpc[2] == "conprioridad")
                        {

                            DatosBorrarEpc = LineasEstaciones[Antena_Borrar, i].Split(';');


                            if (DatosBorrarEpc[0] != "nada")
                            {
                                LineasEstaciones[Antena_Borrar, 1] = DatosBorrarEpc[0] + ';' + DatosBorrarEpc[1] + ";conprioridad";
                            }
                            else
                            {
                                LineasEstaciones[Antena_Borrar, 1] = DatosBorrarEpc[0] + ';' + DatosBorrarEpc[1] + ";nada";
                            }

                        }
                    }
                } // CIERRA FOR 10

            }

            Escribir_Txt_Estaciones(Antena_Borrar);



            //Console.WriteLine( "SALE DE  BORRAR EPC" ); // SEGUIMIENTO 

        }
        static void BorrarEpc_Ultima_Estacion(string Epc_ultimo)
        {
            string[] DatosActualizar = new string[3];
            string[] DatosBorrarEpc = new string[4];
            string rutaestacionactual = ruta + "estacion" + 7 + ".txt";
            int Antena_Borrar = 7;

            for (int i = 1; i <= 10; i++)
            {
                DatosBorrarEpc = LineasEstaciones[Antena_Borrar, i].Split(';');
                // [0]= EPC     |        [1]= TIPO      |        [2]= PRIORIDAD

                if (DatosBorrarEpc[0] == Epc_ultimo)
                {
                    LineasEstaciones[Antena_Borrar, i] = "nada;nada;nada";
                    Organizar_Lineas_Estaciones(Antena_Borrar);

                    if (DatosBorrarEpc[2] == "conprioridad")
                    {
                        DatosBorrarEpc = LineasEstaciones[Antena_Borrar, 1].Split(';');

                        if (DatosBorrarEpc[0] != "nada")
                        {
                            LineasEstaciones[Antena_Borrar, 1] = DatosBorrarEpc[0] + ';' + DatosBorrarEpc[1] + ";conprioridad";
                        }
                        else
                        {
                            LineasEstaciones[Antena_Borrar, 1] = DatosBorrarEpc[0] + ';' + DatosBorrarEpc[1] + ";nada";
                        }
                    }
                    break;
                }
            }

        }
        static void Organizar_Lineas_Estaciones(int estacion)
        {
            int i = 1;
            while (i <= 10) //se recorre la matriz
            {
                if (LineasEstaciones[estacion, i] == "nada;nada;nada")//si se encuentra una posicion vacia, escriba en ella lo que se encuentre en la siguiente posicion con algo escrito
                {
                    for (int i2 = i; i2 <= 10; i2++) // se busca a partir de la posicion donde se encontro vacio el vector      
                    {
                        if (LineasEstaciones[estacion, i2] != "nada;nada;nada")//encuentra una posicion con algo escrito despues de la posicion donde estaba vacio el vector
                        {
                            LineasEstaciones[estacion, i] = LineasEstaciones[estacion, i2];//remplaza la posicion vacia por la que encontro con algo escrito

                            LineasEstaciones[estacion, i2] = "nada;nada;nada"; //remplaza por vacio puesto que ya se encuentra en la posicion donde estaba vacio
                            break;
                        }
                    }
                }
                i++;
            }
        }
        static void Escribir_Ultima_Estacion()
        {
            DateTime tiempo = DateTime.UtcNow;
            string[] Datos_Estacion = new string[3];
            string escrito = null;

            int Total_segundos = tiempo.Hour * 60 * 60 + tiempo.Minute * 60 + tiempo.Second;
            for (int i = 1; i <= Contador_Ultima_Estacion; i++)
            {
                Datos_Estacion = Ultima_Estacion[i].Split(';');
                if (epc == Datos_Estacion[0])
                {
                    escrito = "si";
                    Ultima_Estacion[i] = Datos_Estacion[0] + ';' + Total_segundos;
                    break;
                }

            }
            if (escrito != "si")
            {
                Ultima_Estacion[Contador_Ultima_Estacion] = epc + ';' + Total_segundos;
                Contador_Ultima_Estacion++;
            }

        }
        static void Inicializar()// INICIALIZA TODOS LOS TXT EN BLANCO 
        {
            //Console.WriteLine( "si entra" );
            string strest = "nada;nada;nada";


            for (int i = 1; i <= 30; i++)
            {
                Ultima_Estacion[i] = "nada;nada;nada";
            }

            for (int i = 0; i <= 7; i++)
            {
                for (int j = 0; j <= 15; j++)
                {
                    Lineas_Epc_Quitados[i, j] = "nada;nada;nada";
                }
            }

            //for ( int i=1 ;i<=20 ;i++ )
            //{
            //    LineasEstacionesReingreso[ i ] = "nada;nada";
            //}

            for (int i = 0; i <= 7; i++)
            {
                for (int j = 0; j <= 15; j++)
                {
                    LineasEstacionesReingreso[i, j] = "nada;nada";
                }
            }


            for (int i = 0; i <= 7; i++)
            {
                for (int j = 0; j <= 15; j++)
                {
                    LineasEstaciones[i, j] = "nada;nada;nada";
                }
            }


            for (int i = 0; i <= 7; i++)
            {
                OperariosAutorizados[i] = "nada";
            }

            while (true)
            {
                try
                {
                    StreamWriter file8 = new StreamWriter(@ruta + "operariosestaciones.txt");
                    for (int i = 1; i <= 7; i++)
                    {
                        file8.WriteLine("no;No autorizado;No autorizado");
                    }
                    file8.Close();
                    break;
                }
                catch { }
            }

            while (true)
            {
                try
                {
                    StreamWriter file9 = new StreamWriter(ruta + "estacion1.txt");
                    for (int i = 1; i <= 15; i++)
                    {
                        file9.WriteLine(strest);
                    }
                    file9.Close();
                    break;
                }
                catch { }
            }


            while (true)
            {
                try
                {
                    StreamWriter file10 = new StreamWriter(ruta + "estacion2.txt");
                    for (int i = 1; i <= 15; i++)
                    {
                        file10.WriteLine(strest);
                    }
                    file10.Close();
                    break;
                }
                catch { }
            }


            while (true)
            {
                try
                {
                    StreamWriter file11 = new StreamWriter(ruta + "estacion3.txt");
                    for (int i = 1; i <= 15; i++)
                    {
                        file11.WriteLine(strest);
                    }
                    file11.Close();
                    break;
                }
                catch { }
            }


            while (true)
            {
                try
                {
                    StreamWriter file12 = new StreamWriter(ruta + "estacion4.txt");
                    for (int i = 1; i <= 15; i++)
                    {
                        file12.WriteLine(strest);
                    }
                    file12.Close();
                    break;
                }
                catch { }
            }


            while (true)
            {
                try
                {
                    StreamWriter file13 = new StreamWriter(ruta + "estacion5.txt");
                    for (int i = 1; i <= 15; i++)
                    {
                        file13.WriteLine(strest);
                    }
                    file13.Close();
                    break;
                }
                catch { }
            }


            while (true)
            {
                try
                {
                    StreamWriter file14 = new StreamWriter(ruta + "estacion6.txt");
                    for (int i = 1; i <= 15; i++)
                    {
                        file14.WriteLine(strest);
                    }
                    file14.Close();
                    break;
                }
                catch { }
            }


            while (true)
            {
                try
                {
                    StreamWriter file15 = new StreamWriter(ruta + "estacion7.txt");
                    for (int i = 1; i <= 15; i++)
                    {
                        file15.WriteLine(strest);
                    }
                    file15.Close();
                    break;
                }
                catch { }
            }

            //Console.WriteLine( "si entra" );
        }
        static void ConfigurarLectores()// CONFIGURA LOS LECTORES E INICIA LAS LECTURAS 
        {
            Console.WriteLine("Conectando Lectores...");

            try
            {
                readers.Add(new ImpinjReader("192.168.0.20", "Reader #1"));
                readers.Add(new ImpinjReader("192.168.0.13", "Reader #2"));
                foreach (ImpinjReader reader in readers)
                {
                    reader.Connect();
                    Impinj.OctaneSdk.Settings settings = reader.QueryDefaultSettings();
                    settings.Report.IncludeAntennaPortNumber = true;
                    settings.ReaderMode = ReaderMode.AutoSetDenseReader;
                    settings.SearchMode = SearchMode.DualTarget;
                    settings.Session = 2;
                    settings.Antennas.DisableAll();

                    settings.Antennas.GetAntenna(1).IsEnabled = true;
                    settings.Antennas.GetAntenna(2).IsEnabled = true;
                    settings.Antennas.GetAntenna(3).IsEnabled = true;

                    settings.Antennas.GetAntenna(1).TxPowerInDbm = 12;
                    settings.Antennas.GetAntenna(2).TxPowerInDbm = 12;
                    settings.Antennas.GetAntenna(3).TxPowerInDbm = 12;

                    settings.Antennas.GetAntenna(1).MaxRxSensitivity = true;
                    settings.Antennas.GetAntenna(2).MaxRxSensitivity = true;
                    settings.Antennas.GetAntenna(3).MaxRxSensitivity = true;

                    if (reader.Name.ToString() == "Reader #1")
                    {
                        settings.Antennas.GetAntenna(4).IsEnabled = true;
                        settings.Antennas.GetAntenna(4).TxPowerInDbm = 12;
                        settings.Antennas.GetAntenna(4).MaxRxSensitivity = true;
                    }
                    // Send a tag report every time the reader stops (period is over).
                    settings.Report.Mode = ReportMode.BatchAfterStop;
                    settings.AutoStart.Mode = AutoStartMode.Periodic;
                    settings.AutoStart.PeriodInMs = 400; //  CADA 10...150
                    settings.AutoStop.Mode = AutoStopMode.Duration;
                    settings.AutoStop.DurationInMs = 400; // 5 SEG.....250

                    reader.ApplySettings(settings);


                    reader.TagsReported += OnTagsReported;

                    reader.Start();

                }
            }
            catch (OctaneSdkException e) { Console.WriteLine("Octane SDK exception: {0}", e.Message); }
            catch (Exception e) { Console.WriteLine("Exception : {0}", e.Message); }
            
            Console.WriteLine("LECTORES CONECTADOS");
        }
        static void DetenerLectores()// DETIENE LA LECTURA Y DESCONECTA LOS LECTORES 
        {
            foreach (ImpinjReader reader in readers)
            {
                if (reader.Name.ToString() == "Reader #1" || reader.Name.ToString() == "Reader #2")
                {
                    try
                    {
                        reader.Stop();
                        reader.Disconnect();
                    }
                    catch { Console.WriteLine("No se pueden detener los lectores"); }
                }
                

            }
        }
        static void AutorizarOperarios()// AUTORIZA EL OPERARIO EN LA ESTACION 
        {
            StreamReader operariosestacioness;
            StreamReader operariosbasee;
            StreamWriter operariosestaciones22;

            string[] LineasOperariosestaciones = new string[9];
            string[] Datosoperario = new string[3];

            string rutaopeesta = ruta + "operariosestaciones.txt";
            string rutaopebase = ruta + "operariosbase.txt";
            string autorizado = null;

            while (true)
            {
                try
                {
                    operariosestacioness = new StreamReader(@rutaopeesta);
                    //txt donde se autorizan los operarios..va hacia las estaciones..7 lineas
                    break;
                }
                catch { }
            } //Lee txt de operarios que lee las estaciones-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-


            for (int i = 1; i < 8; i++)
            {
                LineasOperariosestaciones[i] = operariosestacioness.ReadLine();
            }
            operariosestacioness.Close();

            while (true)
            {
                try
                {
                    operariosbasee = new StreamReader(@rutaopebase);
                    //txt donde estan los operarios autorizados por base..viene desde base
                    break;
                }
                catch { }
            }//Lee txt donde base autoriza a los operarios-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-

            string[] Datosoperario2 = new string[4];

            while (!operariosbasee.EndOfStream)
            {
                Datosoperario = operariosbasee.ReadLine().Split(';'); // [0]=epc(codigo) [1]=estacion  [2]=nombre

                if (epc == Datosoperario[0] && antena == Datosoperario[1])
                {
                    OperariosAutorizados[Convert.ToUInt16(antena)] = Datosoperario[0];
                    Datosoperario2 = Datosoperario;
                    autorizado = "si";
                    break;
                }
                else if (epc == Datosoperario[0])
                {
                    Datosoperario2 = Datosoperario;
                    autorizado = "no";
                }
            }
            operariosbasee.Close();

            LineasOperariosestaciones[Convert.ToUInt16(antena)] = autorizado + ";" + Datosoperario2[2] + ";" + Datosoperario2[3];
            while (true)
            {
                try
                {
                    operariosestaciones22 = new StreamWriter(@rutaopeesta);
                    // escribe txt operarios estaciones...decide si autoriza o no
                    break;
                }
                catch { }
            }// Escribe txt que lee estaciones de operarios autorizados -.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-

            for (int i = 1; i < 8; i++)
            {
                operariosestaciones22.WriteLine(LineasOperariosestaciones[i]);
            }
            operariosestaciones22.Close();
        }
        static void ActualizarProductos() // FALTA TERMINAR...VERIFICAR QUE NO SE HAYA ESCRITO Y CUANDO ACTUALICE QUE SOLO GUARDE LOS QUE FALTAN 
        {

            StreamReader productos;
            string rutaproductos = ruta + "productos.txt"; // BASE


            while (true)
            {
                try
                {
                    productos = new StreamReader(@rutaproductos);
                    //txt donde estan los productos autorizados por base
                    break;
                }
                catch { }
            }// Lee txt productos que autoriza base

            while (!productos.EndOfStream)
            {
                CantidadProductos++;
                ListaProductos[CantidadProductos] = productos.ReadLine() + ';' + "noleido";
            }
            productos.Close();


        }
        static void EtiquetaAgain()// VERIFICAR SI EL EPC LEIDO YA SE ESCRIBIO EN LA ESTACION 
        {
            //Console.WriteLine( "ENTRA A ETIQUETA AGAIN" ); // SEGUIMIENTO
            TagAgain = "no";
            string[] DatosTag = new string[4];


            for (int i = 1; i <= 10; i++)
            {
                //Console.Write( "LA LINEA:" + i + " DE LA ESTACION:" + antena ); // SEGUIMIENTO
                //Console.WriteLine( "  TIENE ESCRITO: " + LineasEstaciones[ Convert.ToUInt16( antena ) , i ] ); // SEGUIMIENTO

                DatosTag = LineasEstaciones[Convert.ToUInt16(antena), i].Split(';');

                // [0]=epc    |     [1]=tipo(carro,etc)     |     [2]=prioridad     |     [3]=Segundos de la hora de ingreso

                //Console.WriteLine( "EPC TXT: " + DatosTag[ 0 ] + " Y EPC: " + epc ); // SEGUIMIENTO

                if (DatosTag[0] == epc)
                {
                    TagAgain = "si";
                    break;
                }
            }


            //Console.WriteLine( "SALE A ETIQUETA AGAIN" ); // SEGUIMIENTO 
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            {
                //Console.WriteLine( "ABRE FOR PARA ESCRIBIR TODAS LAS LINEAS DE LA ULTIMA" );
                for (int i = 1; i <= Contador_Ultima_Estacion; i++)
                {
                    //Console.WriteLine( "las lineas de estacion ultima: " + Ultima_Estacion[ i ] );
                }
                //Console.WriteLine( "CIERRA FOR PARA ESCRIBIR TODAS LAS LINEAS DE LA ULTIMA-----" );
                if (antena == "7")
                {
                    DateTime tiempo2 = DateTime.UtcNow;
                    string[] Datos_Ultima_estacion = new string[3];
                    int Resta_Segundos = 0;
                    int Total_segundos = tiempo2.Hour * 60 * 60 + tiempo2.Minute * 60 + tiempo2.Second;

                    for (int i = 1; i <= Contador_Ultima_Estacion - 1; i++)
                    {
                        Datos_Ultima_estacion = Ultima_Estacion[i].Split(';');

                        Console.WriteLine("en la linea de la ultima estacion: " + Ultima_Estacion[i]);

                        if (Datos_Ultima_estacion[0] != "nada")
                        {
                            Console.WriteLine("segundos antes: " + Datos_Ultima_estacion[1]);
                            Console.WriteLine("segundos despues: " + Total_segundos);

                            Resta_Segundos = Total_segundos - Convert.ToInt32(Datos_Ultima_estacion[1]);

                            Console.WriteLine("resta de segundos: " + Resta_Segundos);
                        }
                        if (Resta_Segundos >= 2)
                        {
                            Console.WriteLine("SALIO DE LA ESTACION 7 EL EPC:  " + epc + "----------------------------");
                            BorrarEpc_Ultima_Estacion(Datos_Ultima_estacion[0]);
                            Organizar_Lineas_Estaciones(7);
                            Escribir_Txt_Estaciones(7);

                        }
                    }


                }
                //Console.WriteLine( "cada 1 segundo" );

            }
        }
        private void label13_Click(object sender, EventArgs e)
        {

        }
        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void startestaciones_Click(object sender, EventArgs e)
        {
            ConfigurarLectores();
            //while (true)
            //{
            //    //Console.WriteLine( "ABRE FOR PARA ESCRIBIR TODAS LAS LINEAS DE LA ULTIMA" );
            //    for (int i = 1; i <= Contador_Ultima_Estacion; i++)
            //    {
            //        //Console.WriteLine( "las lineas de estacion ultima: " + Ultima_Estacion[ i ] );
            //    }
            //    //Console.WriteLine( "CIERRA FOR PARA ESCRIBIR TODAS LAS LINEAS DE LA ULTIMA-----" );
            //    if (antena == "7")
            //    {
            //        DateTime tiempo2 = DateTime.UtcNow;
            //        string[] Datos_Ultima_estacion = new string[3];
            //        int Resta_Segundos = 0;
            //        int Total_segundos = tiempo2.Hour * 60 * 60 + tiempo2.Minute * 60 + tiempo2.Second;

            //        for (int i = 1; i <= Contador_Ultima_Estacion - 1; i++)
            //        {
            //            Datos_Ultima_estacion = Ultima_Estacion[i].Split(';');

            //            // Console.WriteLine( "en la linea de la ultima estacion: " + Ultima_Estacion[ i ] );

            //            if (Datos_Ultima_estacion[0] != "nada")
            //            {
            //                //  Console.WriteLine( "segundos antes: " + Datos_Ultima_estacion[ 1 ] );
            //                // Console.WriteLine( "segundos despues: " + Total_segundos );

            //                Resta_Segundos = Total_segundos - Convert.ToInt32(Datos_Ultima_estacion[1]);

            //                //  Console.WriteLine( "resta de segundos: " + Resta_Segundos );
            //            }
            //            if (Resta_Segundos >= 4)
            //            {
            //                // Console.WriteLine( "SALIO DE LA ESTACION 7 EL EPC:  " + epc + "----------------------------" );
            //                BorrarEpc_Ultima_Estacion(Datos_Ultima_estacion[0]);
            //                Organizar_Lineas_Estaciones(7);
            //                Escribir_Txt_Estaciones(7);

            //            }
            //        }


            //    }
            //    //Console.WriteLine( "cada 1 segundo" );
            //    Thread.Sleep(1000);

            //}
            //Console.ReadKey();
            label15.Text = LabelLectores;
        }

        private void stopestaciones_Click(object sender, EventArgs e)
        {
            DetenerLectores();
            label15.Text = LabelLectores;
        }
        ////Prevenciones
        public enum CtrlTypes
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT,
            CTRL_CLOSE_EVENT,
            CTRL, LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT
        }
        private static bool ConsoleCtrlCheck(CtrlTypes ctrlType)// CODIGO A EJECUTARSE AL MOMENTO DE CERRAR BRUSCAMENTE  
        {
            DetenerLectores();
            return true;
        }

        //Interfaz Portales
        private void signoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            login log = new login();
            log.ShowDialog();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}