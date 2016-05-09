﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPU5
{
    class Picking
    {
        Connection connection;
        int tamport = 0;
        int unavez = 1;
        int acum = 0;
        public Picking(Connection connection)
        {
            this.connection = connection;
        }
        public void InitialiazePicking()
        {
            for (int i = 1; i < 8; i++)
            {

                string est = "" + i;
                connection.update("inventariopicking", "Estacion='" + est + "'", "Cantidad='0'");
            }
        }
        public void Pedidopicking()
        {
            List<string> Pedido = connection.pullRow("Orden", "Tipo");
            Dictionary<string, string> PediPick = new Dictionary<string, string>();
            for (int j = 0; j < Pedido.Count; j++)
            {

                PediPick.Add("Orden", "1");
                PediPick.Add("Tipo", Pedido[j]);
                connection.push("pedidopicking", PediPick);

            }
        }
        public void ActualPiezasPicking()
        {
            List<string> Piezas = connection.pull("inventarioentrada", "Autorizado='Si'");
            if (Piezas.Count != 0 && unavez == 1)
            {
                connection.update("inventariopicking", "Cantidad='0'", "Cantidad='7'");
                unavez = 0;
            }
        }
        public int entport(int reinicio)
        {
            List<string> Piezas = connection.pull("inventarioentrada", "Autorizado='Si'");

            tamport = Piezas.Count / 4;

            int novo = tamport - acum;
            if (reinicio == 1)
            {
                acum = tamport;
            }
            return novo;
        }
    }
}
