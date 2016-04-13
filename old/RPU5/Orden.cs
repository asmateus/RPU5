using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPU5
{
    public class Orden
    {
        private Producto[] productos;
        private int cantidad;

        public Orden(Producto[] productos, int cantidad)
        {
            this.cantidad = cantidad;
            this.productos = productos;
        }

        public int getCantidad()
        {
            return this.cantidad;
        }

        public Producto[] getProductos()
        {
            return this.productos;
        }

        public Producto getProducto(int objetivo)
        {
            return productos[objetivo];
        }
    }
}
