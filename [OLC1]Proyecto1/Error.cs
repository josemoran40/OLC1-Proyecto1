using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _OLC1_Proyecto1
{
    class Error
    {
        private string token;
        private int fila;
        private int columna;
        private string tipo;

        public Error(string token, int fila, int columna, string tipo)
        {
            this.token = token;
            this.fila = fila;
            this.columna = columna;
            this.tipo = tipo;
        }

        public string getToken()
        {
            return token;
        }

        public int getFila()
        {
            return fila;
        }
        public int getColumna()
        {
            return columna;
        }
        public string getTipo()
        {
            return tipo;
        }

    }
}
