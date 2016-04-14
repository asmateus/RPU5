using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPU5
{
    public class Servidor
    {
        private Cliente[] estaciones;
        private String red;
        private int cantidad;

        public Servidor(String path, int cantidad)
        {
            this.red = path;
            this.cantidad = cantidad;
            this.estaciones = new Cliente[cantidad];
        }

        public bool[] checkConnections()
        {
            bool[] estados = new bool[this.cantidad];
            for (int i = 0; i < this.cantidad; i++)
            {
                estados[i] = this.estaciones[i].checkStatus();
            }
            return estados;
        }

        public void neuter(int destino)
        {
            estaciones[destino].setStatus(false);
        }

        public void revive(int destino)
        {
            estaciones[destino].setStatus(true);
        }

        public void setClientPaths(String[] rutas)
        {
            for (int i = 0; i < this.cantidad; i++)
            {
                estaciones[i] = new Cliente(rutas[i]);
            }
        }

        public bool sendTo(int destino,String recursos ,String aEnviar,String nombreNuevo)
        {
            try
            {
                string sourceFile = recursos+aEnviar;
                string destFile = estaciones[destino].getPath()+nombreNuevo;
                System.IO.File.Copy(sourceFile, destFile, true);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool sendTo(String destino, String recursos, String aEnviar, String nombreNuevo)
        {
            try
            {
                string sourceFile = recursos + aEnviar;
                string destFile = destino + nombreNuevo;
                System.IO.File.Copy(sourceFile, destFile, true);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool receiveFrom(int destino,String recursos,String aRecibir)
        {
            try
            {
                string sourceFile = estaciones[destino].getPath()+aRecibir;
                string destFile = recursos+aRecibir;
                System.IO.File.Copy(sourceFile, destFile, true);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool receiveFrom(String destino, String recursos, String aRecibir)
        {
            try
            {
                string sourceFile = destino + aRecibir;
                string destFile = recursos + aRecibir;
                System.IO.File.Copy(sourceFile, destFile, true);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public void deleteFrom(int destino, String aEliminar)
        {
            try
            {
                System.IO.File.Delete(estaciones[destino].getPath()+aEliminar);
            }
            catch (Exception e)
            {
            }
        }

        public int getCantidad()
        {
            return this.cantidad;
        }

        public string led(int i, Boolean[] c, String recursos)
        {
            try
            {
                String ruta;
                if (c[i])
                {
                    return ruta = recursos + "/Connected.png";                   
                }
                else
                {
                    return ruta = recursos + "/Disconnected.png";
                }
                
            }
            catch (Exception e)
            {
                return "Error"; //hacer la box de mensaje
            }
        }

        
        public string rellenarIma(int i, int[] e, String recursos)
        {
            String ruta;
            try
            {
                switch (e[i])
                {
                    case 0: { return ruta = recursos + "/Avion.jpg"; }
                    case 1: { return ruta = recursos + "/Carro.jpg"; }
                    case 2: { return ruta = recursos + "/Dinosaurio.jpg"; }
                    case 3: { return ruta = recursos + "/Clock.png"; }
                    case 4: { return ruta = recursos + "/Ready.png"; }
                    default: { return ruta = recursos + "/Error.jpg"; }
                }
            }
            catch { return "Error"; }

          } //termina rellenar imagenes de los productos
        }
        
}
