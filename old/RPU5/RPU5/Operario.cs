using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPU5
{
    public class Operario
    {
        private String nombre;
        private String id;
        private int cargo;

        public Operario(String nombre, String id, int cargo)
        {
            this.nombre = nombre;
            this.id = id;
            this.cargo = cargo;
        }


        public String getNombre()
        {
            return this.nombre;
        }

        public String getId()
        {
            return this.id;
        }

        public int getCargo()
        {
            return this.cargo;
        }

        public String cargoToString()
        {
            String s;

            switch (cargo)
            {
                case 0:
                    {
                        s = "Database Manager";
                        break;
                    }
                case 1:
                    {
                        s = "Picker";
                        break;
                    }
                case 2:
                    {
                        s = "Estación 1";
                        break;
                    }
                case 3:
                    {
                        s = "Estación 2";
                        break;
                    }
                case 4:
                    {
                        s = "Estación 3";
                        break;
                    }
                case 5:
                    {
                        s = "Estación 4";
                        break;
                    }
                case 6:
                    {
                        s = "Estación 5";
                        break;
                    }
                case 7:
                    {
                        s = "Estación 6";
                        break;
                    }
                case 8:
                    {
                        s = "Estación 7";
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
