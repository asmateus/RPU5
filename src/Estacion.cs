using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPU5
{
    class Estacion
    {
        string Nombre;
        string EPC;
        int Warning;
        int stat;
        string[] ProducEst = { "Waiting", "Waiting", "Waiting", "Waiting", "Waiting", "Waiting", "Waiting" };
        int[] sigue = { 0, 0, 0, 0, 0, 0, 0 };
        Connection connection;
        public Estacion(Connection connection)
        {
            this.connection = connection;
        }

        public void InitializeOpe()
        {
            for (int i = 1; i < 8; i++)
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
            switch (info) {
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
                        cualidad = infoperario[4];
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


                Dictionary<string, string> operariono = new Dictionary<string, string>();
                operariono.Add("EPC", EPC);
                operariono.Add("Estacion", NoEsta);
                //operariono.Add("Tiempo", "Asdfsf");
                operariono.Add("Nombre", Nombre);
                connection.push("operariosno", operariono);
                Int32.TryParse(NoEsta, out stat);
                sigue[stat - 1] = 1;
            }

        }

        public int OperarioWarning(string NoEsta)
        {
            OperarioNoAutorizado(NoEsta);
            Int32.TryParse(NoEsta, out stat);
            if (sigue[stat - 1] == 1)
            {

                Warning = 1;
            }
            else { Warning = 0; }
            return Warning;
        }

        public string[] OrdenEstacion(string NoEsta)
        {
            List<string> Orden = connection.pull("orden", "Estado='"+NoEsta+"'");
            Int32.TryParse(NoEsta, out stat);
            if (Orden.Count != 0)
            {

                ProducEst[stat - 1] = Orden[0];

            }
            else { ProducEst[stat - 1] = "Waiting"; }
            return ProducEst;
        }

    }


}
