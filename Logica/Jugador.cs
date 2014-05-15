// -----------------------------------------------------------------------
// <copyright file="Jugador.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Logica
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Jugador
    {
        public string Nombre { get; set; }
        public int Identificador { get; set; }
        Cartas _carta;
        public List<Cartas> BarajaRepartida { get; set; }

        public Jugador()
        {
            _carta = new Cartas();
        }
        public Jugador(string nombre, int identificador)
        {
            Nombre = nombre;
            Identificador = identificador;
            _carta = new Cartas();
        }
        public List<Cartas> RepartirCartasRonda(int numeroCartasRepartir)
        {
            List<Cartas> lista = new List<Cartas>();
            List<Cartas> lsitaTempRetorno = new List<Cartas>();
            if (BarajaRepartida == null)
            {
                BarajaRepartida = lista;
            }
            if (BarajaRepartida.Count > 0)
            {
                lista = BarajaRepartida;
            }
            //Generar los valores aleatorios para las cartas
            Random randomPalo = new Random();
            Random randomCarta = new Random();

            //Repartir cartas de acuerdo a la ronda
            for (int i = 1; i < numeroCartasRepartir + 1; i++)
            {
                int vPalo = randomPalo.Next(1, 5);
                int vCarta = randomCarta.Next(2, 15);
                //Verificar que en la misma ronda no existan dos cartas iguales
                var consulta = lista.Where(x => x.PaloValor == vPalo && x.Valor == vCarta).ToList();
                if (consulta.Count <= 0)
                {
                    Cartas cartaRepartida = new Cartas().AsignarValorCarta(vCarta, vPalo);
                    lista.Add(cartaRepartida);
                    lsitaTempRetorno.Add(cartaRepartida);
                    continue;
                }
                //en caso que existan dos iguales en la misma ronda repita el proceso
                i--;

            }
            BarajaRepartida = lista;
            return lsitaTempRetorno;
        }
        public int ValidarJugadaGanadora(List<Cartas> lista)
        {
            var validarAs = lista.Where(x => x.Valor == 14).Select(x => new Cartas()
            {
                Baraja = x.Baraja,
                Nombre = x.Nombre,
                Palo = x.Palo,
                PaloValor = x.PaloValor,
                Valor = x.Valor
            }).ToList();

            foreach (var item in validarAs)
            {
                item.Valor = 1;
                lista.Add(item);
            }
            lista = lista.OrderBy(x => x.Valor).ToList();
            List<int> total1 = new List<int>();
            List<int> temp1 = new List<int>();
            bool esColor = false;
            //Validar escalera color - flor imperial
            for (int i = 0; i < lista.Count; i++)
            {
                if (i >= 4 && total1.Count <= 0)
                {
                    break;
                }
                if (i < lista.Count - 1)
                {
                    if (lista[i].Valor == lista[i + 1].Valor - 1 && lista[i].PaloValor == lista[i + 1].PaloValor)
                    {
                        temp1.Add(lista[i].Valor);

                    }
                    if (total1.Count < temp1.Count)
                    {
                        total1 = temp1;
                    }
                    if (lista[i].Valor != lista[i + 1].Valor - 1 || lista[i].PaloValor != lista[i + 1].PaloValor)
                    {
                        temp1 = new List<int>();
                        if (total1.Count < 4)
                        {
                            esColor = false;
                        }
                    }
                }
            }
            if ((total1.Count >= 4 && esColor) && (total1[0] == 10))
            {
                return 9;
            }
            if (total1.Count >= 4 && esColor)
            {
                return 8;
            }
            var listaRepetidos = lista.Select(x => x.Valor).ToList();
            //listaRepetidos = new List<int>();
            //listaRepetidos.Add(1);
            //listaRepetidos.Add(21);
            //listaRepetidos.Add(3);
            //listaRepetidos.Add(4);
            //listaRepetidos.Add(5);
            //listaRepetidos.Add(6);
            //listaRepetidos.Add(15);

            //Poker - par- doble par - trio -full
            var query2 = listaRepetidos.GroupBy(x => x)
              .Where(g => g.Count() > 1)
              .Select(y => new { Element = y.Key, Counter = y.Count() })
              .ToList();
            if (query2.Count == 1)
            {
                //Par, trio o poker
                int contador = query2.Select(x => x.Counter).FirstOrDefault();

                if (contador == 4)
                {
                    //Poker
                    return 7;
                }
            }

            if (query2.Count == 2)
            {
                //Doble par o full
                int contador1 = query2[0].Counter;
                int contador2 = query2[1].Counter;

                if ((contador1 == 2 && contador2 == 3) || (contador1 == 3 && contador2 == 2) || (contador2 == 3 && contador1 == 3))
                {
                    //Full
                    return 6;
                }

            }

            //Validar el color
            var listaRepetidos2 = lista.Select(x => x.PaloValor).ToList();
            var query3 = listaRepetidos2.GroupBy(x => x)
            .Where(g => g.Count() > 1)
            .Select(y => new { Element = y.Key, Counter = y.Count() })
            .ToList();
            if (query3.Count == 1)
            {
                int color = query3.Select(x => x.Counter).FirstOrDefault();
                if (color >= 5)
                {
                    //Color
                    return 5;
                }
            }
            if (query3.Count == 2)
            {
                int contador1 = query3[0].Counter;
                int contador2 = query3[1].Counter;
                //Ya no hay mas combinatorias para obtener color
                if ((contador1 == 2 && contador2 == 5) || (contador2 == 2 && contador1 == 5))
                {
                    //Color
                    return 5;
                }
            }

            List<int> total = new List<int>();
            List<int> temp = new List<int>();
            //Validar escalera
            for (int i = 0; i < listaRepetidos.Count; i++)
            {
                if (i >= 4 && total.Count <= 0)
                {
                    break;
                }
                if (i < listaRepetidos.Count - 1)
                {
                    if (listaRepetidos[i] == listaRepetidos[i + 1] - 1)
                    {
                        temp.Add(listaRepetidos[i]);

                    }
                    if (total.Count < temp.Count)
                    {
                        total = temp;
                    }
                    if (listaRepetidos[i] != listaRepetidos[i + 1] - 1)
                    {

                        temp = new List<int>();
                    }
                }
            }
            if (total.Count >= 4)
            {
                //Escalera
                return 4;
            }

            if (query2.Count == 1)
            {
                //Par, trio o poker
                int contador = query2.Select(x => x.Counter).FirstOrDefault();
                if (contador == 3)
                {
                    //Trio
                    return 3;
                }
            }

            if (query2.Count == 2)
            {
                //Doble par o full
                int contador1 = query2[0].Counter;
                int contador2 = query2[1].Counter;
                if (contador1 == 2 && contador2 == 2)
                {
                    //Doble par
                    return 2;
                }
            }

            if (query2.Count == 1)
            {
                //Par, trio o poker
                int contador = query2.Select(x => x.Counter).FirstOrDefault();
                if (contador == 2)
                {
                    //Par
                    return 1;
                }

            }

            //Carta alta
            return 0;
        }
    }
}
