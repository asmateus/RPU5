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
        private static int SLEEP_TIME = 2000;
        private Connection conn;

        public Status(Connection conn) {
            this.conn = conn;
        } 
        public override void RunThread()
        {
            FeedBack mess = new FeedBack();
            while (true)
            {
                // Verificar conexiones
                switch (this.conn.status()) { 
                    case 0:
                        mess.SendSignal("CONNECTION_IDLE");
                        break;
                    case -1:
                        mess.SendSignal("CONNECTION_ERROR");
                        this.conn.Open();
                        break;
                    case 1:
                        mess.SendSignal("CONNECTION_OK");
                        break;
                }

                Thread.Sleep(SLEEP_TIME);
            }
        }
    }
}
