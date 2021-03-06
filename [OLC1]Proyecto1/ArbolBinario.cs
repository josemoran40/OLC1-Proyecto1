﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace _OLC1_Proyecto1
{
    class ArbolBinario
    {
        LinkedList<Token> lTokens;
        LinkedList<Nodo> lNodos;
        Token token;
        LinkedList<Token> lNuevos;
        LinkedList<Nodo> tokensNuevos;
        LinkedList<Image> imagenes;
        LinkedList<Image> AFD;
        LinkedList<Mueve> mueves;
        LinkedList<Image> Tablas;
        LinkedList<string> nombresExp;
        int graficar;
        public int fin;
        public static LinkedList<string> entradas;
        public static LinkedList<LinkedList<Mueve>> lMueves;
        
        public LinkedList<Nodo> generarLista(LinkedList<Token> tokens)
        {
            imagenes = new LinkedList<Image>();
            graficar = 0;
            AFD = new LinkedList<Image>();
            mueves = new LinkedList<Mueve>();
            Tablas = new LinkedList<Image>();
            lTokens = tokens;
            lMueves = new LinkedList<LinkedList<Mueve>>();
            nombresExp = new LinkedList<string>();
            fin = 0;
            for (int i = 0; i < lTokens.Count(); i++)
            {
                token = lTokens.ElementAt(i);
               
                if (token.getTipo() == Token.Tipo.ID && lTokens.ElementAt(i+1).getTipo() == Token.Tipo.FLECHA &&
                        (lTokens.ElementAt(i+2).getTipo() == Token.Tipo.ASTERISCO || lTokens.ElementAt(i+2).getTipo() == Token.Tipo.PUNTO ||
                        lTokens.ElementAt(i+2).getTipo() == Token.Tipo.INTERROGACION || lTokens.ElementAt(i+2).getTipo() == Token.Tipo.OR ||
                        lTokens.ElementAt(i+2).getTipo() == Token.Tipo.MAS))
                {
                    lNodos = new LinkedList<Nodo>();
                    lNuevos = new LinkedList<Token>();
                    tokensNuevos = new LinkedList<Nodo>();
                    entradas = new LinkedList<string>();
                    nombresExp.AddLast(token.getValor());
                    i = i + 2;
                    token = lTokens.ElementAt(i);
                    while (token.getTipo() != Token.Tipo.PUNTOYCOMA)
                    {
                        if (token.getTipo() != Token.Tipo.COMILLA_DOBLES && token.getTipo() != Token.Tipo.LLAVE_ABRE && token.getTipo() != Token.Tipo.LLAVE_CIERRA 
                            && token.getTipo() != Token.Tipo.CORCHETECIERRA && token.getTipo() != Token.Tipo.CORCHETEABRE && token.getTipo() != Token.Tipo.DOS_PUNTOS)
                        {
                            lNuevos.AddLast(token);
                        }
                        i++;
                        token = lTokens.ElementAt(i);
                    }
                    Nodo nodo;
                    for (int j = 0; j < lNuevos.Count(); j++)
                    {
                        token = lNuevos.ElementAt(j);
                        switch (token.getTipo())
                        {

                            case Token.Tipo.PUNTO:
                                nodo = new Nodo(token.getValor(), token.getTipo(), j, token.getTipo());
                                tokensNuevos.AddLast(nodo);
                                break;
                            case Token.Tipo.OR:
                                nodo = new Nodo(token.getValor(), token.getTipo(), j, token.getTipo());
                                tokensNuevos.AddLast(nodo);
                                break;
                            case Token.Tipo.MAS:
                                nodo = new Nodo(token.getValor(), token.getTipo(), j, token.getTipo());
                                tokensNuevos.AddLast(nodo);
                                break;
                            case Token.Tipo.ASTERISCO:
                                nodo = new Nodo(token.getValor(), token.getTipo(), j, token.getTipo());
                                tokensNuevos.AddLast(nodo);
                                break;
                            case Token.Tipo.INTERROGACION:
                                nodo = new Nodo(token.getValor(), token.getTipo(), j, token.getTipo());
                                tokensNuevos.AddLast(nodo);
                                break;
                            case Token.Tipo.CADENA:
                                nodo = new Nodo(token.getValor(), token.getTipo(), j, token.getTipo());
                                tokensNuevos.AddLast(nodo);
                                break;
                            case Token.Tipo.ID:
                                nodo = new Nodo(token.getValor(), token.getTipo(), j, token.getTipo());
                                tokensNuevos.AddLast(nodo);
                                break;

                            case Token.Tipo.SALTODELINEA:
                                nodo = new Nodo(token.getValor(), token.getTipo(), j, token.getTipo());
                                tokensNuevos.AddLast(nodo);
                                break;
                            case Token.Tipo.TABULACION:
                                nodo = new Nodo(token.getValor(), token.getTipo(), j, token.getTipo());
                                tokensNuevos.AddLast(nodo);
                                break;
                            case Token.Tipo.COMILLA:
                                nodo = new Nodo(token.getValor(), token.getTipo(), j, token.getTipo());
                                tokensNuevos.AddLast(nodo);
                                break;
                            case Token.Tipo.SLASHCOMILLAS:
                                nodo = new Nodo(token.getValor(), token.getTipo(), j, token.getTipo());
                                tokensNuevos.AddLast(nodo);
                                break;
                            case Token.Tipo.TODO:
                                nodo = new Nodo(token.getValor(), token.getTipo(), j, token.getTipo());
                                tokensNuevos.AddLast(nodo);
                                break;
                        }
                    }

                    int r = 0;
                    while (tokensNuevos.Count() > 1)
                    {

                        if (nodoBinario(r))
                        {

                            r = 0;
                        }
                        else if (nodoUnario(r))
                        {
                            r = 0;
                        }
                        else
                        {
                            r++;
                        }
                        Console.WriteLine("entra");
                    }

                    tokensNuevos.ElementAt(0).imprimirNodos();
                   imagenes.AddLast( generarGraphviz());
                    LinkedList<Transicion> transicions = new LinkedList<Transicion>();
                     transicions = tokensNuevos.ElementAt(0).generarTransiciones("F","L",transicions);
                    Thompson thompson = new Thompson();
                    mueves = thompson.generarAFD(transicions);
                    lMueves.AddLast(thompson.getMueves());
                    AFD.AddLast(Run(thompson.getGraphivz()));
                    Tablas.AddLast(Run(thompson.tablaDeTransiciones()));
                   // generarImagen(generarGraphviz());


                }
                else if (token.getTipo() == Token.Tipo.PORCENTAJE)
                {
                    fin = i;
                    i = lTokens.Count();
                    
                }
            }
            
            return lNodos;
        }

        public LinkedList<Image> getImagenes() {
            return imagenes;
        }

        public LinkedList<Image> getAFD()
        {
            return AFD;
        }

        public LinkedList<Image> getTablas()
        {
            return Tablas;
        }

        public int getFin() {
            return fin;
        }

        bool nodoBinario(int indice)
        {
            if ((indice + 2) < tokensNuevos.Count())
            {
                //-----------------Verifica si viene operador
                if (tokensNuevos.ElementAt(indice).getTipo() == Token.Tipo.PUNTO
                        || tokensNuevos.ElementAt(indice).getTipo() == Token.Tipo.OR)
                {
                    //-------------Verifica si viene el primer operando
                    if (tokensNuevos.ElementAt(indice+1).getTipo() == Token.Tipo.CADENA
                            || tokensNuevos.ElementAt(indice+1).getTipo() == Token.Tipo.ID || tokensNuevos.ElementAt(indice+1).getTipo() == Token.Tipo.OPERADOR
                            || tokensNuevos.ElementAt(indice + 1).getTipo() == Token.Tipo.SALTODELINEA || tokensNuevos.ElementAt(indice + 1).getTipo() == Token.Tipo.TABULACION
                            || tokensNuevos.ElementAt(indice + 1).getTipo() == Token.Tipo.SLASHCOMILLAS || tokensNuevos.ElementAt(indice + 1).getTipo() == Token.Tipo.COMILLA
                            || tokensNuevos.ElementAt(indice + 1).getTipo() == Token.Tipo.TODO)
                    {
                        //--------Veifica si viene el segundo operando
                        if (tokensNuevos.ElementAt(indice + 2 ).getTipo() == Token.Tipo.CADENA
                                || tokensNuevos.ElementAt(indice + 2 ).getTipo() == Token.Tipo.ID || tokensNuevos.ElementAt(indice + 2 ).getTipo() == Token.Tipo.OPERADOR
                            || tokensNuevos.ElementAt(indice + 2).getTipo() == Token.Tipo.SALTODELINEA || tokensNuevos.ElementAt(indice + 2).getTipo() == Token.Tipo.TABULACION
                            || tokensNuevos.ElementAt(indice + 2).getTipo() == Token.Tipo.SLASHCOMILLAS || tokensNuevos.ElementAt(indice + 2).getTipo() == Token.Tipo.COMILLA
                            || tokensNuevos.ElementAt(indice + 2).getTipo() == Token.Tipo.TODO)
                        {
                            tokensNuevos.ElementAt(indice).setizquierda(tokensNuevos.ElementAt(indice+1));
                            tokensNuevos.ElementAt(indice).setderecha(tokensNuevos.ElementAt(indice + 2 ));
                            tokensNuevos.ElementAt(indice).setAnterior(tokensNuevos.ElementAt(indice).getTipo());
                            tokensNuevos.ElementAt(indice).setTipo(Token.Tipo.OPERADOR);
                            tokensNuevos.Remove(tokensNuevos.ElementAt(indice + 1));
                            tokensNuevos.Remove(tokensNuevos.ElementAt(indice + 1));
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        bool nodoUnario(int indice)
        {

            if ((indice + 1) < tokensNuevos.Count())
            {
                //-----------------Verifica si viene operador
                if (tokensNuevos.ElementAt(indice).getTipo() == Token.Tipo.MAS
                        || tokensNuevos.ElementAt(indice).getTipo() == Token.Tipo.ASTERISCO || tokensNuevos.ElementAt(indice).getTipo() == Token.Tipo.INTERROGACION)
                {
                    //-------------Verifica si viene el primer operando
                    if (tokensNuevos.ElementAt(indice+1).getTipo() == Token.Tipo.CADENA
                            || tokensNuevos.ElementAt(indice+1).getTipo() == Token.Tipo.ID || tokensNuevos.ElementAt(indice+1).getTipo() == Token.Tipo.OPERADOR
                            || tokensNuevos.ElementAt(indice + 1).getTipo() == Token.Tipo.SALTODELINEA || tokensNuevos.ElementAt(indice + 1).getTipo() == Token.Tipo.TABULACION
                            || tokensNuevos.ElementAt(indice + 1).getTipo() == Token.Tipo.SLASHCOMILLAS || tokensNuevos.ElementAt(indice + 1).getTipo() == Token.Tipo.COMILLA
                            || tokensNuevos.ElementAt(indice + 1).getTipo() == Token.Tipo.TODO)
                    {
                        tokensNuevos.ElementAt(indice).setizquierda(tokensNuevos.ElementAt(indice+1));
                        tokensNuevos.ElementAt(indice).setAnterior(tokensNuevos.ElementAt(indice).getTipo());
                        tokensNuevos.ElementAt(indice).setTipo(Token.Tipo.OPERADOR);
                        tokensNuevos.Remove(tokensNuevos.ElementAt(indice+1));
                        return true;

                    }
                }
            }

            return false;
        }

        Image generarGraphviz()
        {

            String cadena = "";
            LinkedList<Transicion> transicions = new LinkedList<Transicion>();
            cadena += "digraph g{\n";
            cadena += "Graph" + "[label = \"AFN\"];\n";
            cadena += "rankdir=LR\n;";
            cadena += "N [shape = doublecircle, fontsize = 1; style = filled fillcolor=white,  fontcolor = white, color = white];";
            cadena += "F [shape = circle, fontsize = 10;style = filled fillcolor=aquamarine, color = aquamarine];";
            cadena += "L [shape = doublecircle, fontsize = 10; style = filled fillcolor=aquamarine, color = aquamarine]; N->F;";
            cadena += "node [shape = circle, fontsize = 10; style = filled fillcolor=aquamarine, color = aquamarine]";
            cadena +=tokensNuevos.ElementAt(0).generarAFN("F","L");
            transicions = tokensNuevos.ElementAt(0).generarTransiciones("F", "L", transicions);
            cadena += "}";

            Console.WriteLine(cadena);
            Image dot = Run(cadena);
            return dot;
        }



        public static string graphviz = @"dot.exe";
        public static string archivoentrada = @"grafico.txt";
      private static Bitmap Run(string dot)
        {
            try
            {
                string executable = graphviz;
                string output = archivoentrada;
                File.WriteAllText(output, dot);

                System.Diagnostics.Process process = new System.Diagnostics.Process();

                // Stop the process from opening a new window
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                // Setup executable and parameters
                process.StartInfo.FileName = executable;
                process.StartInfo.Arguments = string.Format(@"{0} -Tjpg -O", output);

                // Go
                process.Start();
                // and wait dot.exe to complete and exit
                process.WaitForExit();
                Bitmap bitmap = null; ;
                using (Stream bmpStream = System.IO.File.Open(output + ".jpg", System.IO.FileMode.Open))
                {
                    Image image = Image.FromStream(bmpStream);
                    bitmap = new Bitmap(image);
                }

                string path = output + ".jpg";
                File.Delete(output + ".jpg");
                return bitmap;
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("Algo ha salido mal :(", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }/*

        private Bitmap Run(string dot)
        {
            try
            {
                System.IO.File.WriteAllText("archivo.txt", dot);
                ProcessStartInfo so = new ProcessStartInfo("dot.exe");
                so.Arguments = "-Tpng \"archivo.txt \" -o \"archivo"+graficar+".png\"";
                
                Process.Start(so);
                Bitmap bitmap = null; 
                using (Stream bmpStream = System.IO.File.Open("archivo"+graficar+".png", System.IO.FileMode.Open))
                {
                    Image image = Image.FromStream(bmpStream);
                    bitmap = new Bitmap(image);
                }

                graficar++;
                return bitmap;
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("Algo ha salido mal :(", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                return null;
        }*/
        
        public LinkedList<string> getNombres()
        {
            return nombresExp;
        }
    }

}
