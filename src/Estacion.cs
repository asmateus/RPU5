using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace RPU5
{
    class Estacion
    {

        string Nombre;
        string EPC;
        int Warning;
        int stat;
        public static Dictionary<string, string> operariono;
        string[] ProducEst = { "Waiting", "Waiting", "Waiting", "Waiting", "Waiting", "Waiting", "Waiting", "Waiting" };
        int[] sigue = { 0, 0, 0, 0, 0, 0, 0, 0 };
        Connection connection;


        public Estacion(Connection connection)
        {
            this.connection = connection;
        }

        public void InitializeOpe()
        {
            for (int i = 1; i < 9; i++)
            {
                string istring = "" + i;
                connection.update("operarios", "Estacion='" + istring + "'", "Conectado= 'No'");
                connection.update("operarios", "Estacion='" + istring + "'", "Hora= '00:00'");
            }


        }

        public string getOperario(string NoEsta, string info)
        {
            List<string> infoperario = connection.pull("operarios", "Estacion='" + NoEsta + "'");
            string cualidad = "algo";
            switch (info)
            {
                case "Nombre":
                    {
                        cualidad = infoperario[2];
                        break;
                    }
                case "Autorizado":
                    {
                        cualidad = infoperario[5];
                        break;
                    }
                case "Estado":
                    {
                        cualidad = infoperario[6];
                        break;
                    }
                case "Codigo":
                    {
                        cualidad = infoperario[3];

                        break;
                    }
                case "EPC":
                    {
                        cualidad = infoperario[0];
                        break;
                    }
                case "Semaforo":
                    {
                        cualidad = infoperario[8];
                        break;
                    }
            }

            return cualidad;



        }

        public void OperarioNoAutorizado(string NoEsta)
        {
            //  List<string> Permiso = connection.pull("Operariosno", "Estacion='" + NoEsta + "'");
            List<string> infoperario = connection.pull("operarios", "Estacion='" + NoEsta + "'");
            if (infoperario[5] == "No")
            {
                EPC = infoperario[4];
                //for (int a = 1; a < 8; a++)
                //{if ()
                Nombre = infoperario[2];
                //}


                operariono = new Dictionary<string, string>();
                operariono.Add("EPC", EPC);
                operariono.Add("Estacion", NoEsta);
                //operariono.Add("Tiempo", "Asdfsf");
                operariono.Add("Nombre", Nombre);
                connection.push("operariosno", operariono);
                sigue[Int32.Parse(NoEsta) - 1] = 1;
            }

        }

        public int OperarioWarning(string NoEsta)
        {
            OperarioNoAutorizado(NoEsta);
            if (sigue[Int32.Parse(NoEsta) - 1] == 1)
            {
                Warning = 1;
            }
            else { Warning = 0; }
            return Warning;
        }

        public string[] OrdenEstacion(string NoEsta)
        {
            List<string> Orden = connection.pull("orden", "Estado='" + NoEsta + "'");
            Int32.TryParse(NoEsta, out stat);
            if (Orden.Count != 0)
            {

                ProducEst[stat - 1] = Orden[0];

            }
            else { ProducEst[stat - 1] = "Waiting"; }
            return ProducEst;
        }

        public void necpiezas()
        {
            List<string> necpieza = connection.pull("necpiezas", "Estado='1'");
            if (necpieza.Count != 0)
            {
                if (necpieza[0] != "8")
                {
                    connection.update("necpiezas", "Estacion='" + necpieza[0] + "' AND Pieza='" + necpieza[1] + "'", "Estado='2'");
                }
                if (necpieza[3] == "0")
                {
                    connection.delete("necpiezas", "Estado='0'");
                }
                if (necpieza[0] == "8")
                {
                    List<string> Piezas = connection.pull("inventarioentrada", "Autorizado='Si'");

                    if (Piezas.Count != 0)
                    {
                        int NumPieza = Int32.Parse(necpieza[1]);

                        string Pieza = "" + NumPieza;
                        List<string> PiezasSum = connection.pull("inventariopicking", "Pieza='" + Pieza + "'");
                        for (int i = 0; i < PiezasSum.Count; i++)
                        {
                            int Piezasum = Int32.Parse(PiezasSum[2]);
                            Piezasum = Piezasum + 6;
                            string cant = "" + Piezasum;
                            connection.update("inventariopicking", "Pieza='" + Pieza + "'", "Cantidad='" + cant + "'");

                        }

                    }
                    connection.update("necpiezas", "Pieza='" + necpieza[1] + "'", "Estado='0'");
                }

            }


        }
    }


}
