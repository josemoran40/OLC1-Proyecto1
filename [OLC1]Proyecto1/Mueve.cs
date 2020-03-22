using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _OLC1_Proyecto1
{
    class Mueve
    {
        private Cerradura cerradura;
        private string estado;
        private string entrada;
        private string nombre;

        public Mueve()
        {
        }

        public Mueve(Cerradura cerradura, String nombre, String estado, String entrada)
        {
            this.cerradura = cerradura;
            this.nombre = nombre;
            this.estado = estado;
            this.entrada = entrada;
        }



        public Cerradura getCerradura()
        {
            return cerradura;
        }

        public void setCerradura(Cerradura cerradura)
        {
            this.cerradura = cerradura;
        }

        public String getNombre()
        {
            return nombre;
        }

        public void setNombre(String nombre)
        {
            this.nombre = nombre;
        }

        public String getEstado()
        {
            return estado;
        }

        public void setEstado(String estado)
        {
            this.estado = estado;
        }

        public String getEntrada()
        {
            return entrada;
        }

        public void setEntrada(String entrada)
        {
            this.entrada = entrada;
        }


    }
    }
