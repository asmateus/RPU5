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
using System.Threading;

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
            connection = new Connection("127.0.0.1", 3306);

            status_thread = new Status(connection);
            status_thread.Start();
            
        }
    }
}
