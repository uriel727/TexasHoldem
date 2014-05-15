// -----------------------------------------------------------------------
// <copyright file="Cartas.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Logica
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Cartas
    {
        public string Nombre { get; set; }
        public int Valor { get; set; }
        public string Palo { get; set; }
        public int PaloValor { get; set; }
        public List<Cartas> Baraja { get; set; }

        public Cartas()
        {
           
        }
        public Cartas AsignarValorCarta(int valorCarta, int valorPalo)
        {
            string palo = string.Empty;
            string carta = string.Empty;
            if (valorPalo == 1)
            {
                palo = "♣";
            }
            if (valorPalo == 2)
            {
                palo = "♦";
            }
            if (valorPalo == 3)
            {
                palo = "♠";
            }
            if (valorPalo == 4)
            {
                palo = "♥";
            }

            if (valorCarta == 2)
            {
                carta = "2";
            }
            if (valorCarta == 3)
            {
                carta = "3";
            }
            if (valorCarta == 4)
            {
                carta = "4";
            }
            if (valorCarta == 5)
            {
                carta = "5";
            }
            if (valorCarta == 6)
            {
                carta = "6";
            }
            if (valorCarta == 7)
            {
                carta = "7";
            }
            if (valorCarta == 8)
            {
                carta = "8";
            }
            if (valorCarta == 9)
            {
                carta = "9";
            }
            if (valorCarta == 10)
            {
                carta = "10";
            }
            if (valorCarta == 11)
            {
                carta = "Js";
            }
            if (valorCarta == 12)
            {
                carta = "Qs";
            }
            if (valorCarta == 13)
            {
                carta = "Ks";
            }
            if (valorCarta == 14)
            {
                carta = "As";
            }
            return new Cartas() { 
            Nombre = carta,
            Valor = valorCarta,
            Palo = palo,
            PaloValor = valorPalo
            };
        }
        public List<Cartas> AsignarCartas()
        {
            string palo = string.Empty;
            string carta = string.Empty;      
            List<Cartas> lista = new List<Cartas>();       
            for (int i = 1; i <= 4; i++)
            {              
                for (int j = 2; j <= 14; j++)
                {
                  Cartas c=  AsignarValorCarta(j,i);
                  lista.Add(c);
                }
            }
            return lista;
        }
    }
}
