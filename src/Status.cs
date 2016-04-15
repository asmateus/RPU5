using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

/*
*
* Esta clase verifica todas las conexiones del programa y genera las acciones adecuadas
* dependiendo del estado de las mismas. Para ello debe recibir la conexión inicial, esto
* es, la clase, ella misma no puede iniciar un vínculo de conexión. 
* 
*/

namespace RPU5
{
    class Status : BaseThread
    {
        private static int SLEEP_TIME = 500;
        private Connection conn;

        public Status(Connection conn) {
            this.conn = conn;
        } 
        public override void RunThread()
        {
            while (true)
            {
                Thread.Sleep(SLEEP_TIME);

                // Verificar conexiones
                if (conn.status() == false)
                {
                    //Console.Write("NOT CONNECTED\n");
                }
                else
                {
                    //Console.Write("CONNECTED\n");
                }
            }
        }
    }
}
