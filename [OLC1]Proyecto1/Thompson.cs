using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _OLC1_Proyecto1
{
    class Thompson
    {
        LinkedList<Mueve> mueves;
        LinkedList<Cerradura> cerraduras;
        string cadena = "";
        public LinkedList<Mueve> generarAFD(LinkedList<Transicion> transiciones) {

            cerraduras = new LinkedList<Cerradura>();
            mueves = new LinkedList<Mueve>();

            Cerradura cerradura0 = new Cerradura();
            cerradura0.getEstados().AddLast("F");
            cerradura0.setTransiciones(generarCerradura(transiciones, cerradura0.getEstados()));
            cerradura0.getTransiciones().AddLast("F");
            cerraduras.AddLast(cerradura0);
            cerradura0.setNombre("0");
            int estado = 1;

            for (int i = 0; i < cerradura0.getTransiciones().Count(); i++)
            {
                Console.WriteLine(cerradura0.getTransiciones().ElementAt(i));
            }

            for (int i = 0; i < cerraduras.Count(); i++)
            {
                for (int j = 0; j < ArbolBinario.entradas.Count(); j++)
                {
                    LinkedList<string> m = new LinkedList<string>();
                    for (int k = 0; k < cerraduras.ElementAt(i).getTransiciones().Count(); k++)
                    {
                        m = generarMueve(transiciones, ArbolBinario.entradas.ElementAt(j),
                            cerraduras.ElementAt(i).getTransiciones().ElementAt(k), m);
                    }

                    LinkedList<string> x = new LinkedList<string>();
                    x = generarCerradura(transiciones, m);
                    for (int o = 0; o < m.Count(); o++)
                    {
                        x.AddLast(m.ElementAt(o));
                    }
                    Cerradura cerradura = new Cerradura(m, x);
                    if (x.Count() != 0)
                    {
                        bool bandera = true;
                        Cerradura temp = new Cerradura();
                        for (int y = 0; y < cerraduras.Count(); y++)
                        {
                            HashSet<string> a1 = new HashSet<string>();
                            HashSet<string> a2 = new HashSet<string>();
                            for (int t = 0; t < cerraduras.ElementAt(y).getTransiciones().Count(); t++)
                            {
                                a1.Add(cerraduras.ElementAt(y).getTransiciones().ElementAt(t));
                            }

                            for (int t = 0; t < x.Count(); t++)
                            {
                                a2.Add(x.ElementAt(t));
                            }

                            if (a1.SetEquals(a2))
                            {
                                bandera = false;
                                temp = cerraduras.ElementAt(y);
                            }


                        }
                        if (bandera)
                        {
                            Console.WriteLine("entro");
                            cerradura.setNombre("" + estado);
                            cerraduras.AddLast(cerradura);
                            
                            Mueve mueve = new Mueve(cerradura, cerraduras.ElementAt(i).getNombre(), cerraduras.ElementAt(i).getNombre(), ArbolBinario.entradas.ElementAt(j));
                            estado++;
                            mueves.AddLast(mueve);
                        }
                        else
                        {
                            Console.WriteLine("salio");
                            Mueve mueve = new Mueve(temp, cerraduras.ElementAt(i).getNombre(), cerraduras.ElementAt(i).getNombre(), ArbolBinario.entradas.ElementAt(j));
                            mueves.AddLast(mueve);

                        }
                    }
                    
                }
            }

            

            Console.WriteLine("-------------------------------------------------");
            cadena = "";
            cadena += "digraph g{\n";
            cadena += "Graph" + "[label = \"AFD\"] ;\n";
            cadena += "rankdir=LR\n;";
            cadena += "N [shape = doublecircle, fontsize = 1; style = filled fillcolor=white,  fontcolor = white, color = white]; \n";
            cadena += "node [shape = circle; style = filled fillcolor=aquamarine, color = aquamarine]\n";
            if (cerraduras.ElementAt(0).getTransiciones().Contains("L")) {
                cadena += "0 [shape = doublecircle; style = filled fillcolor=aquamarine, color = aquamarine];";
            }
            else{
                cadena += "0 [shape = circle; style = filled fillcolor=aquamarine]; N->0;\n";

            }
            foreach (var item in mueves)
            {
                Console.WriteLine("Mueve de: " +item.getEstado() + " con: " + item.getEntrada() + " hacia: " +item.getCerradura().getNombre());

                if (item.getEntrada().Equals("\n") || item.getEntrada().Equals("\t") || item.getEntrada().Equals("\\"))
                {
                    cadena += item.getEstado() + "->" + item.getCerradura().getNombre() + "[label = \"\\" + item.getEntrada() + "\" ];\n";
                }
                else
                {
                    cadena += item.getEstado() + "->" + item.getCerradura().getNombre() + "[label = \"" + item.getEntrada() + "\" ];\n";
                }
                if (item.getCerradura().getTransiciones().Contains("L"))
                {
                    cadena += item.getCerradura().getNombre() + " [shape = doublecircle; style = filled fillcolor=aquamarine, color = aquamarine];";
                }
            }
            Console.WriteLine("-------------------------------------------------");
            cadena += "}";
            Console.WriteLine(cadena);
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("-------------------------------------------------");
            tablaDeTransiciones();
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("-------------------------------------------------");

            return mueves;
        }

        public LinkedList<string> generarMueve(LinkedList<Transicion> transiciones, string entrada, string estado, LinkedList<string> t)
        {
            

            for (int i = 0; i < transiciones.Count(); i++)
            {
                Transicion transicion = transiciones.ElementAt(i);

                if (transicion.getEstadoA().Equals(estado) && transicion.getValor().Equals(entrada)) {
                    if (!t.Contains(transicion.getEstadoB()))
                    {
                        t.AddLast(transicion.getEstadoB());
                    }
                }
            }

            return t;
        }

        public LinkedList<string> generarCerradura(LinkedList<Transicion> transiciones, LinkedList<string> estado) {
            LinkedList<string> t = new LinkedList<string>();
            for (int j = 0; j < estado.Count(); j++)
            {
                for (int i = 0; i < transiciones.Count(); i++)
                {
                    if (transiciones.ElementAt(i).getEstadoA().Equals(estado.ElementAt(j)) && transiciones.ElementAt(i).getValor().Equals("ε"))
                    {
                        if (!t.Contains(transiciones.ElementAt(i).getEstadoB()))
                        {
                            t.AddLast(transiciones.ElementAt(i).getEstadoB());
                        }
                    }
                }

                for (int k = 0; k < t.Count(); k++)
                {
                    string estadoActual = t.ElementAt(k);
                    for (int i = 0; i < transiciones.Count(); i++)
                    {
                        if (transiciones.ElementAt(i).getEstadoA().Equals(estadoActual) && transiciones.ElementAt(i).getValor().Equals("ε"))
                        {
                            if (!t.Contains(transiciones.ElementAt(i).getEstadoB()))
                            {
                                t.AddLast(transiciones.ElementAt(i).getEstadoB());
                            }
                        }
                    }
                }
                
                    
                
            }           

            return t;
        }


        public string tablaDeTransiciones() {
            string tabla = "";
            tabla += "digraph {\n";
            tabla += "tbl [\n";
            tabla += "shape=plaintext\n";
            tabla += "label=<\n";
            tabla += "  <table border='0' cellborder='1' color='blue' cellspacing='0'>\n";
            tabla += "      <tr><td>Estados</td>\n";
            for (int i = 0; i < ArbolBinario.entradas.Count(); i++)
            {
                tabla += "      <td>"+ArbolBinario.entradas.ElementAt(i)+"</td>\n";
            }
            tabla += "      </tr>\n";

            for (int i = 0; i < cerraduras.Count(); i++)
            {
                tabla += "      <tr>\n";
                tabla += "          <td><b>" + cerraduras.ElementAt(i).getNombre() + "</b></td>\n";
                for (int j = 0; j < ArbolBinario.entradas.Count(); j++)
                {
                    string estado = "";
                    bool bandera = false;
                    for (int y = 0; y < mueves.Count(); y++)
                    {
                        if (mueves.ElementAt(y).getEntrada().Equals(ArbolBinario.entradas.ElementAt(j)) && cerraduras.ElementAt(i).getNombre().Equals(mueves.ElementAt(y).getEstado()))
                        {
                            bandera = true;
                            estado = mueves.ElementAt(y).getCerradura().getNombre();

                        }
                    }
                    if (bandera)
                    {
                        tabla += "          <td><b>" + estado + "</b></td>\n";
                    }
                    else {
                        tabla += "          <td>-</td>\n";
                    }
                    
                }
                tabla += "      </tr>\n";
            }

            tabla += "  </table>\n";
            tabla += ">];\n }";

            Console.WriteLine(tabla);

            return tabla;
        }


        public string getGraphivz() {
            return cadena;
        }

        public LinkedList<Mueve> getMueves() {
            return mueves;
        }
    }
}
