using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _OLC1_Proyecto1
{
    class Nodo
    {

        private Nodo izquierda;
        private Nodo derecha;
        private string valor;
        private int index;
        private Token.Tipo tipo;
        private Token.Tipo anterior;
        public Nodo(string valor, Token.Tipo tipo, int index, Token.Tipo anterior)
        {
            this.izquierda = null;
            this.derecha = null;
            this.valor = valor;
            this.index = index;
            this.tipo = tipo;
            this.anterior = anterior;
        }

        public Nodo getizquierda()
        {
            return izquierda;
        }

        public void setizquierda(Nodo izquierda)
        {
            this.izquierda = izquierda;
        }

        public Nodo getderecha()
        {
            return derecha;
        }

        public void setderecha(Nodo derecha)
        {
            this.derecha = derecha;
        }

        public String getValor()
        {
            return valor;
        }

        public void setValor(String valor)
        {
            this.valor = valor;
        }

        public int getIndex()
        {
            return index;
        }

        public void setIndex(int index)
        {
            this.index = index;
        }

        public Token.Tipo getTipo()
        {
            return tipo;
        }

        public void setTipo(Token.Tipo tipo)
        {
            this.tipo = tipo;
        }

        public Token.Tipo getAnterior()
        {
            return anterior;
        }

        public void setAnterior(Token.Tipo anterior)
        {
            this.anterior = anterior;
        }

        public LinkedList<Transicion> transicions() {

            LinkedList<Transicion> transiciones = new LinkedList<Transicion>();



            return transiciones;
        }

        
        
        public void imprimirNodos()
        {
            Console.WriteLine("Valor: " + valor );
            if (izquierda != null)
            {
                Console.WriteLine("izquierda ");
                izquierda.imprimirNodos();
            }
            if (derecha != null)
            {
                Console.WriteLine("derecha ");
                derecha.imprimirNodos();
            }
        }

        public string generarAFN(string first, string last) {
            string cadena ="";
            if (this.anterior == Token.Tipo.OR)
            {
                
                cadena += first + " -> " + last+ index + "1" + "[label = \"ε\"];\n";
                cadena += first + " -> " + last +  index + "2" + "[label = \"ε\"];\n";
                cadena += izquierda.generarAFN(last + index +"1", last + index +"3");
                cadena += derecha.generarAFN(last + index + "2", last + index +"4");
                cadena += last + index + "4" + " -> " + last + "[label = \"ε\"];\n";
                cadena += last + index + "3" + " -> " + last + "[label = \"ε\"];\n";
            }
            else if (this.anterior == Token.Tipo.ASTERISCO)
            {
                cadena += first + "->"+ last + index + "1[label = \"ε\"];\n";
                cadena += last + index + "2->" + last+ "[label = \"ε\"];\n";
                cadena += first + "->" + last+" [label = \"ε\"];\n";
                cadena += last + index + "2->" + last + index + "1[label = \"ε\"];\n";
                cadena += izquierda.generarAFN(last + index + "1", last + index + "2");
            }
            else if (this.anterior == Token.Tipo.PUNTO)
            {


                cadena += izquierda.generarAFN(first, last + index + "1");
                cadena += derecha.generarAFN(last + index + "1", last);

            }
            else if (this.anterior == Token.Tipo.INTERROGACION)
            {

                cadena += first + " -> " + last + index + "1" + "[label = \"ε\"];\n";
                cadena += first + " -> " + last + index + "2" + "[label = \"ε\"];\n";
                cadena += izquierda.generarAFN(last + index + "1", last + index + "3");
                cadena += last + index + "2" + " -> " + last + index + "4" + "[label = \"ε\"];\n";
                cadena += last + index + "4" + " -> " + last + "[label = \"ε\"];\n";
                cadena += last + index + "3" + " -> " + last+ "[label = \"ε\"];\n";
            }
            else if (this.anterior == Token.Tipo.MAS)
            {


                cadena += first+ "->" + last + index + "1[label = \"ε\"];\n";
                cadena += last + index + "2->" + last + index + "3[label = \"ε\"];\n";
                cadena += first + "->" + last + index + "3[label = \"ε\"];\n";
                cadena += last + index + "2->" + last + index + "1[label = \"ε\"];\n";
                cadena += izquierda.generarAFN(last + index + "1", last + index + "2");

                cadena += izquierda.generarAFN(last + index + "3", last);
            }

            else if (this.anterior == Token.Tipo.CADENA || this.anterior == Token.Tipo.ID)
            {
                cadena += first + "->" + last + "[label = \"" + valor + "\"]";
            }

            return cadena;
        }


        public LinkedList<Transicion> generarTransiciones(string first, string last, LinkedList<Transicion> transicions) {

            
            if (this.anterior == Token.Tipo.OR)
            {

                transicions.AddLast(new Transicion(first, last + index +"1", "ε"));
                transicions.AddLast(new Transicion(first, last + index + "2", "ε"));
                transicions = izquierda.generarTransiciones(last + index + "1", last + index + "3", transicions);
                transicions = derecha.generarTransiciones(last + index + "2", last + index + "4", transicions);
                transicions.AddLast(new Transicion(last + index + "4", last, "ε"));
                transicions.AddLast(new Transicion(last + index + "3", last, "ε"));
            }
            else if (this.anterior == Token.Tipo.ASTERISCO)
            {
                transicions.AddLast(new Transicion(first, last + index + "1", "ε"));
                transicions.AddLast(new Transicion(last + index + "2", last, "ε"));                
                transicions.AddLast(new Transicion(first , last, "ε"));
                transicions.AddLast(new Transicion(last + index + "2", last + index + "1", "ε"));
                transicions = izquierda.generarTransiciones(last + index + "1", last + index + "2", transicions);
            }
            else if (this.anterior == Token.Tipo.PUNTO)
            {
                
                transicions = izquierda.generarTransiciones(first, last + index + "1", transicions);
                transicions = derecha.generarTransiciones(last + index + "1", last, transicions);

            }
            else if (this.anterior == Token.Tipo.INTERROGACION)
            {

                transicions.AddLast(new Transicion(first, last + index + "1", "ε"));
                transicions.AddLast(new Transicion(first, last + index + "2", "ε"));
                transicions = izquierda.generarTransiciones(last + index + "1", last + index + "3", transicions);
                transicions.AddLast(new Transicion( last + index + "2", last + index + "4", "ε"));
                transicions.AddLast(new Transicion(last + index + "4", last, "ε"));
                transicions.AddLast(new Transicion(last + index + "3",last, "ε"));
            }
            else if (this.anterior == Token.Tipo.MAS)
            {
                transicions.AddLast(new Transicion(first, last + index + "1", "ε"));
                transicions.AddLast(new Transicion(last + index + "2", last + index + "3", "ε"));
                transicions.AddLast(new Transicion(first, last + index + "3", "ε"));
                transicions.AddLast(new Transicion(last + index + "2", last + index + "1", "ε"));
                transicions = izquierda.generarTransiciones(last + index + "1", last + index + "2", transicions);
                transicions = izquierda.generarTransiciones(last + index + "3",last, transicions);
            }

            else if (this.anterior == Token.Tipo.CADENA || this.anterior == Token.Tipo.ID)
            {
                transicions.AddLast(new Transicion(last, last, valor));
            }
            return transicions;

        }

    }
}
