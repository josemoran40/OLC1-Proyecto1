using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _OLC1_Proyecto1
{
    class AnalizadorLexico
    {
        LinkedList<Token> lTokens;
        LinkedList<Error> lErrores;
        int estado;
        int fila;
        int columna;
        String comentario;
        char c;
        String token;

        public LinkedList<Token> analizar(String cadena)
        {
            lTokens = new LinkedList<Token>();
            lErrores = new LinkedList<Error>();
            fila = 1;
            columna = 0;
            estado = 0;
            token = "";
            comentario = "";

            for (int i = 0; i < cadena.Length; i++)
            {
                c = cadena[i];

                switch (estado)
                {
                    case 0:
                        if (c == '.')
                        {
                            token += c;
                            agregarToken(Token.Tipo.PUNTO);
                        }
                        else if (c == '{')
                        {
                            token += c;
                            agregarToken(Token.Tipo.LLAVE_ABRE);
                        }
                        else if (c == '}')
                        {
                            token += c;
                            agregarToken(Token.Tipo.LLAVE_CIERRA);
                        }
                        else if (c == '|')
                        {
                            token += c;
                            agregarToken(Token.Tipo.OR);
                        }
                        else if (c == '+')
                        {
                            token += c;
                            agregarToken(Token.Tipo.MAS);
                        }
                        else if (c == '*')
                        {
                            token += c;
                            agregarToken(Token.Tipo.ASTERISCO);
                        }
                        else if (c == '}')
                        {
                            token += c;
                            agregarToken(Token.Tipo.LLAVE_CIERRA);
                        }
                        else if (c == '?')
                        {
                            token += c;
                            agregarToken(Token.Tipo.INTERROGACION);
                        }
                        else if (c == '}')
                        {
                            token += c;
                            agregarToken(Token.Tipo.LLAVE_CIERRA);
                        }
                        else if (c == ';')
                        {
                            token += c;
                            agregarToken(Token.Tipo.PUNTOYCOMA);
                        }
                        else if (c == '"')
                        {
                            token += c;
                            agregarToken(Token.Tipo.COMILLA_DOBLES);
                            estado = 6;
                        }
                        else if (c == '}')
                        {
                            token += c;
                            agregarToken(Token.Tipo.LLAVE_CIERRA);
                        }
                        else if (c == '~')
                        {
                            token += c;
                            agregarToken(Token.Tipo.VIRGULILLA);
                        }
                        else if (c == '%')
                        {
                            token += c;
                            agregarToken(Token.Tipo.PORCENTAJE);
                        }
                        else if (c == '<')
                        {
                            token += c;
                            agregarToken(Token.Tipo.MENORQUE);
                            estado = 7;
                        }
                        else if (c == '}')
                        {
                            token += c;
                            agregarToken(Token.Tipo.LLAVE_CIERRA);
                        }
                        else if (c == '-')
                        {
                            token += c;
                            estado = 1;
                        }
                        else if (c == ':')
                        {
                            token += c;
                            agregarToken(Token.Tipo.DOS_PUNTOS);
                        }
                        else if (c == ',')
                        {
                            token += c;
                            agregarToken(Token.Tipo.COMA);
                        }
                        else if (char.IsDigit(c))
                        {
                            token += c;
                            estado = 2;
                        }
                        else if (c == '/')
                        {
                            token += c;
                            agregarToken(Token.Tipo.SLASH);
                            estado = 4;
                        }
                        else if (char.IsLetter(c))
                        {
                            token += c;
                            estado = 3;
                        }
                        else if (c == '\n')
                        {
                            fila++;
                            columna = 0;
                        }
                        else if (c == '\t')
                        {
                            fila++;
                            columna = 0;
                        }
                        else if (c == '\r')
                        {
                            fila++;
                            columna = 0;
                        }
                        else if (c == ' ')
                        {
                            columna++;
                        }
                        else
                        {
                            agregarError();
                        }

                        columna++;
                        break;

                    case 1:
                        if (c == '>')
                        {
                            token += c;
                            agregarToken(Token.Tipo.FLECHA);
                        }
                        columna++;
                        break;
                    case 2:
                        if (char.IsDigit(c))
                        {
                            token += c;
                            estado = 2;
                        }
                        else
                        {
                            agregarToken(Token.Tipo.NUMERO);
                            i--;
                            columna--;
                        }
                        columna++;
                        break;

                    case 3:
                        if (char.IsLetterOrDigit(c))
                        {
                            token += c;
                            estado = 3;

                            if (token.Equals("CONJ"))
                            {
                                agregarToken(Token.Tipo.PALABRA_RESERVADA);
                            }

                        }
                        else
                        {
                            agregarToken(Token.Tipo.ID);
                            i--;
                            columna--;
                        }
                        break;

                    case 4:
                        if (c == '/')
                        {
                            token += c;
                            agregarToken(Token.Tipo.SLASH);
                            estado = 5;
                        }
                        else
                        {
                            estado = 0;
                            i--;
                            columna--;
                        }
                        break;

                    case 5:
                        if (c == '\n')
                        {
                            agregarToken(Token.Tipo.COMENTARIO);
                            i--;
                            columna--;
                        }
                        else
                        {
                            token += c;
                            estado = 5;
                        }
                        break;
                    case 6:
                        if (c == '"')
                        {
                            agregarToken(Token.Tipo.CADENA);
                            token += c;
                            agregarToken(Token.Tipo.COMILLA_DOBLES);
                        }
                        else
                        {
                            token += c;
                            estado = 6;
                        }
                        break;
                    case 7:
                        if (c == '!')
                        {
                            token += c;
                            agregarToken(Token.Tipo.INTERROGACION);
                            estado = 8;
                        }
                        else
                        {
                            i--;
                            columna--;
                        }
                        break;

                    case 8:
                        if (c == '!')
                        {
                            token += c;
                            estado = 9;
                        }
                        else
                        {
                            comentario = token;
                            token += c;
                            estado = 8;
                        }
                        break;

                    case 9:
                        if (c == '>')
                        {
                            token = comentario;
                            agregarToken(Token.Tipo.COMENTARIO);
                            token = "!";
                            agregarToken(Token.Tipo.EXCLAMACION);
                            token += c;
                            agregarToken(Token.Tipo.MAYORQUE);
                        }
                        else
                        {
                            token += c;
                            estado = 8;
                        }
                        break;

                }
            }

            imprimirListaToken(lTokens);
            imprimirListaError(lErrores);

            return lTokens;
        }

        public void agregarToken(Token.Tipo tipo)
        {
            lTokens.AddLast(new Token(tipo, token, fila, columna));

            token = "";
            estado = 0;
        }

        public void agregarError()
        {
            lErrores.AddLast(new Error(token, fila, columna, "Lexico"));
            token = "";
            estado = 0;
        }

        public LinkedList<Error> getListaErrores()
        {
            return lErrores;

        }

        public void imprimirListaToken(LinkedList<Token> lista)
        {
            foreach (Token item in lista)
            {
                Console.WriteLine(item.getToken() + " <--> " + item.getValor());
            }
        }

        public void imprimirListaError(LinkedList<Error> lista)
        {
            foreach (Error item in lista)
            {
                Console.WriteLine(item.getToken() + " <--> " + "ERROR!");
            }
        }

    }
}
