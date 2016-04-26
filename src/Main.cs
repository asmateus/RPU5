/*
 * Note: 
 * Este es el Main file, un archivo de direcciones y control,
 * no lo edites mucho, solo es para cosas generales, como crear
 * objetos, ejecutar hilos y ciertas labores de inicialización del 
 * sistema.
 * 
 * @author asmateus
 * 
 */

using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading;
using System;

namespace RPU5
{
    public partial class Main : Form
    {

        private Connection connection;
        private Status status_thread;

        public Main()
        {
            // Esta función inicializa la interfaz gráfica y todos los elementos asociados
            InitializeComponent();

            // Intentar establecer conexiones necesarias
            connection = new Connection("10.20.21.142", 3306);
            connection.Open("rpu5", "picking", "PiCkInG");
            //Dictionary<string, string> dict = new Dictionary<string, string>();
            //dict.Add("nombre", "Sesdfs");
            //dict.Add("edad", "434sdfs");
            //dict.Add("sexo", "Asdfsf");
            //connection.push("test", dict);
            //connection.update("Operarios", "Estacion='4'", "Nombre = 'proniela Charris'");

            List<string> temp = new List<string>();
            temp = connection.pullRow("orden", "EPC");
            for (int i = 0; i < temp.Count; ++i)
                Console.Write("\n " + temp[i] + "\n");

            // Este hilo es opcional, lo que hace es probar, periódicamente si la conexión es funciona
            status_thread = new Status(connection);
            status_thread.Start();
            
        }
    }
}
