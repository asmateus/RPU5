using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPU5
{
    public class Estacion
    {
        //Constantes
        public int AVION = 0;
        public int CARRO= 1;
        public int DINOSAURIO = 2;
        public int IDLE = 3;
        public int READY = 4;

        private Operario operario;
        private Producto productoActual;
        private int estado;

        public Estacion()
        {
            this.operario = null;
            this.productoActual = null;
            this.estado = 3;
        }

        public Operario getOperario()
        {
            return this.operario;
        }

        public Producto getProducto()
        {
            return this.productoActual;
        }

        public void setOperario(Operario operario)
        {
            this.operario = operario;
        }

        public void setProducto(Producto producto)
        {
            this.productoActual = producto;
        }

        public void setStatus(int estado)
        {
            this.estado = estado;
        }

        public int getStatus()
        {
            return this.estado;
        }

    }
}
