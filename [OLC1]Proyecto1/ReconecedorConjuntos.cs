using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _OLC1_Proyecto1
{
    class ReconecedorConjuntos
    {

        private LinkedList<Token> lTokens;
        private LinkedList<Conjunto> lConjuntos;
        private LinkedList<String> lCaracteres;
        private Token token;
        private Conjunto conjunto;

        public LinkedList<Conjunto> analizar(LinkedList<Token> tokens)
        {
            lTokens = tokens;
            lConjuntos = new LinkedList<Conjunto>();
            lCaracteres = new LinkedList<String>();

            for (int i = 0; i < lTokens.Count(); i++)
            {
                token = lTokens.ElementAt(i);
                lCaracteres = new LinkedList<String>();
                int temp = i;
                int temp2 = 0;
                if (token.getTipo() == Token.Tipo.PALABRA_RESERVADA)
                {
                    i = i + 2;//Se salta los dos puntos y llega hasta el id
                    token = lTokens.ElementAt(i);
                    conjunto = new Conjunto(token.getValor());

                    i = i + 3;//Se salta la flecha y el primer caracter, se verifica el tipo
                    token = lTokens.ElementAt(i);

                    if (token.getTipo() == Token.Tipo.VIRGULILLA)
                    {
                        i--;//Se obtiene el primer valor
                        token = lTokens.ElementAt(i);
                        char[] array1 = token.getValor().ToCharArray();
                        int  valor1 = (int)Char.GetNumericValue(array1[0]);

                        i = i + 2;//Se obtiene el segundo valor
                        temp2 = i;
                        token = lTokens.ElementAt(i);
                        char[] array2 = token.getValor().ToCharArray();
                        int valor2 = (int)Char.GetNumericValue(array2[0]);

                        for (int j = valor1; j <= valor2; j++)
                        {
                            String strAsciiTab = ""+(char)j;
                            lCaracteres.AddLast(strAsciiTab);
                        }

                    }
                    else
                    {
                        i = i - 2;//Se obtiene el primer valor
                        while (token.getTipo() != Token.Tipo.PUNTOYCOMA)
                        {
                            i++;
                            token = lTokens.ElementAt(i);
                            lCaracteres.AddLast(token.getValor());
                            i++;
                            token = lTokens.ElementAt(i);
                        }
                        temp2 = i;
                    }
                    conjunto.setCaracteres(lCaracteres);
                    lConjuntos.AddLast(conjunto);
                    

                }
            }
            imprimir(lConjuntos);
            return lConjuntos;
        }

        void imprimir(LinkedList<Conjunto> conjuntos)
        {
            for (int i = 0; i < conjuntos.Count; i++)
            {
                Console.WriteLine("Cojunto: " + conjuntos.ElementAt(i).getId());
                for (int j = 0; j < conjuntos.ElementAt(i).getCaracteres().Count; j++)
                {
                    Console.WriteLine("Elemento " + j + ": " + conjuntos.ElementAt(i).getCaracteres().ElementAt(j));
                }
            }
        }

        public LinkedList<Token> getTokens() {
            return lTokens;
        }
    }
}
