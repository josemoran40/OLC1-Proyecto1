using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _OLC1_Proyecto1
{
    class AnalizadorSintactico
    {

        int controlToken;
        Token tokenActual;
        LinkedList<Token> lTokens;
        LinkedList<Error> lErrores;

        public void parsear(LinkedList<Token> tokens, LinkedList<Error> errores)
        {
            //Se inicializa la lista para un analisis nuevo y se elije el primer token de la lista
            this.lTokens = tokens;
            this.lErrores = errores;
            controlToken = 0;
            tokenActual = lTokens.ElementAt(controlToken);
            emparejar(Token.Tipo.LLAVE_ABRE);
            BODY();
            emparejar(Token.Tipo.LLAVE_CIERRA);
        }

        void BODY() {
            
            CONJUNTO();
            EXPRESIONOLEXEMA();
            PORCENTAJE();
            COMENTARIO();
        }

        void PORCENTAJE()
        {
            if (tokenActual.getTipo() == Token.Tipo.PORCENTAJE)
            {
                emparejar(Token.Tipo.PORCENTAJE);
                BODY();
            }
        }
        void CONJUNTO()
        {
            if(tokenActual.getTipo() == Token.Tipo.PALABRA_RESERVADA)
            {
                emparejar(Token.Tipo.PALABRA_RESERVADA);
                emparejar(Token.Tipo.DOS_PUNTOS);
                emparejar(Token.Tipo.ID);
                emparejar(Token.Tipo.FLECHA);
                emparejar(Token.Tipo.CARACTER);
                VIRGULILLA();
                COMA();
                emparejar(Token.Tipo.PUNTOYCOMA);
                BODY();
            }
        }

        void VIRGULILLA()
        {
            if (tokenActual.getTipo() == Token.Tipo.VIRGULILLA) {
                emparejar(Token.Tipo.VIRGULILLA);
                emparejar(Token.Tipo.CARACTER);
            }
        }

        void COMA() { 
            if(tokenActual.getTipo() == Token.Tipo.COMA)
            {
                emparejar(Token.Tipo.COMA);
                emparejar(Token.Tipo.CARACTER);
                COMA();
            }
        }

        void EXPRESIONOLEXEMA()
        {
            if(tokenActual.getTipo() == Token.Tipo.ID)
            {
                emparejar(Token.Tipo.ID);
                LEXEMA();
                EXPRESION();
                emparejar(Token.Tipo.PUNTOYCOMA);
                BODY();
            }
        }

        void LEXEMA()
        {
            if(tokenActual.getTipo() == Token.Tipo.DOS_PUNTOS)
            {
                emparejar(Token.Tipo.DOS_PUNTOS);
                emparejar(Token.Tipo.COMILLA_DOBLES);
                emparejar(Token.Tipo.CADENA);
                emparejar(Token.Tipo.COMILLA_DOBLES);
            }
        }

        void EXPRESION()
        {
            if(tokenActual.getTipo() == Token.Tipo.FLECHA)
            {
                emparejar(Token.Tipo.FLECHA);
                EXP();
            }
        }

        void EXP()
        {
            if(tokenActual.getTipo()== Token.Tipo.OR)
            {
                emparejar(Token.Tipo.OR);
                EXP();
                EXP();
            }else if(tokenActual.getTipo() == Token.Tipo.MAS)
            {
                emparejar(Token.Tipo.MAS);
                EXP();
            }else if(tokenActual.getTipo() == Token.Tipo.PUNTO)
            {
                emparejar(Token.Tipo.PUNTO);
                EXP();
                EXP();
            }else if(tokenActual.getTipo() == Token.Tipo.ASTERISCO) {
                emparejar(Token.Tipo.ASTERISCO);
                EXP();
            }else if(tokenActual.getTipo() == Token.Tipo.INTERROGACION)
            {
                emparejar(Token.Tipo.INTERROGACION);
                EXP();
            }else if(tokenActual.getTipo() == Token.Tipo.LLAVE_ABRE)
            {
                emparejar(Token.Tipo.LLAVE_ABRE);
                emparejar(Token.Tipo.ID);
                emparejar(Token.Tipo.LLAVE_CIERRA);
            }
            else if(tokenActual.getTipo() == Token.Tipo.SALTODELINEA) {
                emparejar(Token.Tipo.SALTODELINEA);
            }else if(tokenActual.getTipo() == Token.Tipo.TABULACION)
            {
                emparejar(Token.Tipo.TABULACION);
            }else if(tokenActual.getTipo()== Token.Tipo.CORCHETEABRE)
            {
                emparejar(Token.Tipo.CORCHETEABRE);
                emparejar(Token.Tipo.DOS_PUNTOS);
                emparejar(Token.Tipo.CADENA);
                emparejar(Token.Tipo.DOS_PUNTOS);
                emparejar(Token.Tipo.CORCHETECIERRA);
            }else if(tokenActual.getTipo() == Token.Tipo.SLASHCOMILLAS)
            {
                emparejar(Token.Tipo.SLASHCOMILLAS);
            }else 
            {
                emparejar(Token.Tipo.COMILLA_DOBLES);
                emparejar(Token.Tipo.CADENA);
                emparejar(Token.Tipo.COMILLA_DOBLES);
            }

        }


        void COMENTARIO()
        {
            if (tokenActual.getTipo() == Token.Tipo.SLASH) {
                emparejar(Token.Tipo.SLASH);
                emparejar(Token.Tipo.SLASH);
                emparejar(Token.Tipo.COMENTARIO);
                BODY();
            }else if(tokenActual.getTipo() == Token.Tipo.MENORQUE)
            {
                emparejar(Token.Tipo.MENORQUE);
                emparejar(Token.Tipo.EXCLAMACION);
                emparejar(Token.Tipo.COMENTARIO);
                emparejar(Token.Tipo.EXCLAMACION);
                emparejar(Token.Tipo.MAYORQUE);
                BODY();
            }
        }

        public void emparejar(Token.Tipo tip)
        {
            if (tokenActual.getTipo() != tip)
            {
                Console.WriteLine("Error se esperaba " + getTipoParaError(tip));
                lErrores.AddLast(new Error("Se esperaba " + getTipoParaError(tip), tokenActual.getFila(), tokenActual.getColumna(), "Sintactico: " + tokenActual.getValor()));
            }
            if (tokenActual.getTipo() != Token.Tipo.ULTIMO)
            {
                controlToken += 1;
                tokenActual = lTokens.ElementAt(controlToken);
            }
        }

        public String getTipoParaError(Token.Tipo tip)
        {
            switch (tip)
            {
                case Token.Tipo.PALABRA_RESERVADA:
                    return "Palabra Reservada";
                case Token.Tipo.LLAVE_ABRE:
                    return "Llave Abre";
                case Token.Tipo.LLAVE_CIERRA:
                    return "Llave Cierra";
                case Token.Tipo.ID:
                    return "ID";
                case Token.Tipo.OR:
                    return "OR";
                case Token.Tipo.PUNTO:
                    return "Punto";
                case Token.Tipo.MAS:
                    return "Mas";
                case Token.Tipo.INTERROGACION:
                    return "Interrogacion";
                case Token.Tipo.PUNTOYCOMA:
                    return "Punto y coma";
                case Token.Tipo.COMILLA_DOBLES:
                    return "Comillas Dobles";
                case Token.Tipo.VIRGULILLA:
                    return "Virguilla";
                case Token.Tipo.PORCENTAJE:
                    return "Porcentaje";
                case Token.Tipo.MENORQUE:
                    return "Menor Que";
                case Token.Tipo.MAYORQUE:
                    return "Mayor Que";
                case Token.Tipo.EXCLAMACION:
                    return "Exclamacion";
                case Token.Tipo.ASTERISCO:
                    return "Asterisco";
                case Token.Tipo.FLECHA:
                    return "Flecha";
                case Token.Tipo.NUMERO:
                    return "Numero";
                case Token.Tipo.DOS_PUNTOS:
                    return "Dos Puntos";
                case Token.Tipo.COMA:
                    return "Coma";
                case Token.Tipo.COMENTARIO:
                    return "Comentario";
                case Token.Tipo.CADENA:
                    return "Cadena";
                case Token.Tipo.SLASH:
                    return "Slash";
                case Token.Tipo.SALTODELINEA:
                    return "Salto de linea";
                case Token.Tipo.TABULACION:
                    return "Tabulacion";
                case Token.Tipo.COMILLA:
                    return "Comilla";
                case Token.Tipo.TODO:
                    return "Todo";
                case Token.Tipo.SLASHCOMILLAS:
                    return "Comillas Dobles";
                case Token.Tipo.CARACTER:
                    return "Caracter";
                default:
                    return "desconocido";

            }
        }

        public LinkedList<Error> GetErrors() {
            return lErrores;
        }
    }
}
