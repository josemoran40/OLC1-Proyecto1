using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _OLC1_Proyecto1
{
    class ValidarExpresion
    {
        LinkedList<string> expresiones;
        public LinkedList<string> validarExpresiones(LinkedList<Token> lTokens, LinkedList<LinkedList<Mueve>> lMueves, LinkedList<Conjunto> lConjuntos, int fin) {
            expresiones = new LinkedList<string>();

            for (int i = fin; i < lTokens.Count; i++)
            {
                Token token = lTokens.ElementAt(i);
                if (token.getTipo() == Token.Tipo.ID) {
                    string nombre = token.getValor();
                    i = i + 3;
                    token = lTokens.ElementAt(i);
                    for (int j = 0; j < lMueves.Count; j++)
                    {
                        string cadena = token.getValor();
                        string cadenaInicial = cadena;
                        if (lMueves.ElementAt(j).Count != 0)
                        {
                            string estado = lMueves.ElementAt(j).ElementAt(0).getEstado(); ;
                            bool aceptacion = false;
                            bool aceptacionInicio = lMueves.ElementAt(j).ElementAt(0).getCerradura().getTransiciones().Contains("L");
                            for (int k = 0; k < lMueves.ElementAt(j).Count; k++)
                            {
                                Mueve mueve = lMueves.ElementAt(j).ElementAt(k);
                                int size = mueve.getEntrada().Length;
                                string caracter = "";
                                if (cadena.Length > 0) {
                                    caracter = cadena[0].ToString();
                                }

                                if (estado == mueve.getEstado() && existeConjunto(lConjuntos, mueve.getEntrada(), caracter)) {
                                    string temporal = cadena;
                                    cadena = "";
                                    for (int r = 1; r < temporal.Length; r++)
                                    {
                                        cadena += temporal[r];
                                    }
                                    estado = mueve.getCerradura().getNombre();
                                    if (mueve.getCerradura().getTransiciones().Contains("L"))
                                    {
                                        aceptacion = true;
                                    }
                                    else
                                    {
                                        aceptacion = false;
                                    }
                                    k = 0;
                                }
                                else if (size <= cadena.Length && estado == mueve.getEstado()) {
                                    string comparacion = "";
                                    for (int t = 0; t < size; t++)
                                    {
                                        comparacion += cadena[t];
                                    }
                                    if (mueve.getEntrada() == comparacion) {
                                        estado = mueve.getCerradura().getNombre();
                                        string temporal = cadena;
                                        cadena = "";
                                        for (int x = size; x < temporal.Length; x++)
                                        {
                                            cadena += temporal[x];
                                        }
                                        k = 0;
                                        if (mueve.getCerradura().getTransiciones().Contains("L"))
                                        {
                                            aceptacion = true;
                                        }
                                        else {
                                            aceptacion = false;
                                        }
                                    }                                   

                                }
                                

                            }
                            if ((cadena == "" && aceptacion) || (cadenaInicial == "" && aceptacionInicio))
                            {
                                expresiones.AddLast(nombre + " es valida con la expresion " + (j + 1));
                                Console.WriteLine(nombre + " es valida con la expresion " + (j + 1));
                            }
                        }
                    }
                }
            }
            return expresiones;
        }

        bool existeConjunto(LinkedList<Conjunto> conjuntos, string nombre, string entrada) {

            for (int i = 0; i < conjuntos.Count; i++)
            {
                if (conjuntos.ElementAt(i).getId() == nombre && conjuntos.ElementAt(i).getCaracteres().Contains(entrada)) {
                    return true;
                }
            }

            return false;
        }
    }


}
