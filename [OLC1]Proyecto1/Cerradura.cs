using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _OLC1_Proyecto1
{
    class Cerradura
    {
        private LinkedList<string> estados;
        private LinkedList<string> transiciones;
        private string nombre;
        public Cerradura(LinkedList<string> estados, LinkedList<string> transiciones)
        {
            this.estados = estados;
            this.transiciones = transiciones;
            this.nombre = "";
        }

        public Cerradura()
        {
            this.estados = new LinkedList<string>();
            this.transiciones = new LinkedList<string>();
        }

        public LinkedList<string> getEstados()
        {
            return estados;
        }

        public void setEstados(LinkedList<string> estados)
        {
            this.estados = estados;
        }

        public LinkedList<string> getTransiciones()
        {
            return transiciones;
        }

        public void setTransiciones(LinkedList<string> transiciones)
        {
            this.transiciones = transiciones;
        }

        public string getNombre() {
            return nombre;
        }

        public void setNombre(string nombre) {
            this.nombre = nombre;
        }
    }
}
