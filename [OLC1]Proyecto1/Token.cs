using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _OLC1_Proyecto1
{
    class Token
    {
        public enum Tipo
        {
            PALABRA_RESERVADA,//-
            LLAVE_ABRE,//-
            LLAVE_CIERRA,//-
            ID,//-
            OR,//-
            PUNTO,//-
            MAS,//-
            INTERROGACION,//-
            PUNTOYCOMA,//-
            COMILLA_DOBLES,//-
            VIRGULILLA,//-
            PORCENTAJE,//-
            MENORQUE,//-
            MAYORQUE,//-
            SLASH,//*
            EXCLAMACION,//-
            ASTERISCO,//-
            FLECHA,//*
            NUMERO,//-
            COMA,//-
            DOS_PUNTOS,//-
            COMENTARIO,
            CADENA,
            ULTIMO,
            OPERADOR,


        }

        private Tipo TipoToken;
        private String valor;
        private int fila;
        private int columna;

        public Token(Tipo tipoToken, String valor, int fila, int columna)
        {
            TipoToken = tipoToken;
            this.valor = valor;
            this.fila = fila;
            this.columna = columna;

        }

        public String getValor()
        {
            return valor;
        }

        public int getFila()
        {
            return fila;
        }

        public int getColumna()
        {
            return columna;
        }

        public Tipo getTipo()
        {
            return TipoToken;
        }

        public Tipo getTipoToken()
        {
            return TipoToken;
        }

        public void setTipoToken(Tipo TipoToken)
        {
            this.TipoToken = TipoToken;
        }




        public String getToken()
        {
            switch (TipoToken)
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
                default:
                    return "desconocido";

            }

        }
    }
}
