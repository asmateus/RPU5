using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPU5
{

    // Esta clase actualiza los elementos de la GUI de acuerdo a ciertas señales del sistema

    class FeedBack
    {
        public FeedBack()
        {

        }

        public void SendSignal(string signal)
        {
            if (signal == "CONNECTION_IDLE") { Console.Write("CONNECTION IDLE\n"); }
        }
    }
}
