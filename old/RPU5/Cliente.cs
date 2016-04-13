
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPU5
{
    public class Cliente
    {
        private String path;
        private bool status;

        public Cliente(String path)
        {
            this.path = path;
            this.status = false;
        }

        public bool checkStatus()
        {
            return status;
        }

        public void setStatus(bool status)
        {
            this.status = status;
        }

        public String getPath()
        {
            return this.path;
        }
    }
}
