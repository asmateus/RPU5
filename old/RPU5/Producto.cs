using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPU5
{
    public class Producto
    {
        private int tipo;
        private String id;
        private bool terminado;

        public Producto(int tipo, String id)
        {
            this.tipo = tipo;
            this.id = id;
            this.terminado = false;
        }

        public int getTipo()
        {
            return this.tipo;
        }

        public String getId()
        {
            return this.id;
        }

        public void finish()
        {
            this.terminado = true;
        }

        public bool isFinished()
        {
            return this.terminado;
        }

        public String tipoToString()
        {
            String s;
            switch (tipo)
            {
                case 1:
                    {
                        s = "Avion";
                        break;
                    }
                case 2:
                    {
                        s = "Carro";
                        break;
                    }
                case 3:
                    {
                        s = "Dinosaurio";
                        break;
                    }
                default:
                    {
                        s = "No Definido";
                        break;
                    }
            }
            return s;
        }

        public String estadoToString()
        {
            String s;
            switch (terminado)
            {
                case true:
                    {
                        s = "Terminado";
                        break;
                    }
                case false:
                    {
                        s = "Pendiente";
                        break;
                    }
                default:
                    {
                        s = "No Definido";
                        break;
                    }
            }
            return s;
        }
    }
}
