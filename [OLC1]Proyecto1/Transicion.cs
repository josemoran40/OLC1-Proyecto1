using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _OLC1_Proyecto1
{
    class Transicion
    {

        private string estadoA;
        private string estadoB;
        private string valor;

        public Transicion(string estadoA, string estadoB, string valor) {
            this.estadoA = estadoA;
            this.estadoB = estadoB;
            this.valor = valor;
        }

        public string getEstadoA() {
            return estadoA;
        }
        public string getEstadoB()
        {
            return estadoB;
        }
        public string getValor()
        {
            return valor;
        }

        public void setEstadoA(string estadoA) {
            this.estadoA = estadoA;
        }

        public void setEstadoB(string estadoB)
        {
            this.estadoB = estadoB;
        }

        public void setValor(string valor)
        {
            this.valor = valor;
        }
    }
}
