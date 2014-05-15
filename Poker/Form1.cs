using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Logica;
using System.Linq;
namespace Poker
{
    public partial class Form1 : Form
    {
        private Jugador jugDealer;
        int ronda = 0;
        private string oculto = "X";
        List<Cartas> cartasJ1 = new List<Cartas>();
        List<Cartas> cartasJ2 = new List<Cartas>();
        List<Cartas> cartasDealer = new List<Cartas>();
        private void VolverJugar()
        {
            cartaVista6.Nombre = string.Empty;
            cartaVista6.Palo = string.Empty;
            cartaVista7.Nombre = string.Empty;
            cartaVista7.Palo = string.Empty;
            ronda = 0;
            List<Cartas> listaAlmacena = new List<Cartas>();
            for (int i = 0; i < 2; i++)
            {
                Jugador jug = new Jugador("Jugador " + (i + 1), (i + 1));
                jug.BarajaRepartida = listaAlmacena;
                List<Cartas> listaPrimerRonda = jug.RepartirCartasRonda(2);
                listaAlmacena = jug.BarajaRepartida;
                for (int j = 0; j < listaPrimerRonda.Count; j++)
                {
                    if (i == 0)
                    {
                        label2.Text = jug.Nombre;
                        if (j == 0)
                        {
                            cartaVista1.Nombre = listaPrimerRonda[j].Nombre;
                            cartaVista1.Palo = listaPrimerRonda[j].Palo;
                        }
                        if (j == 1)
                        {
                            cartaVista2.Nombre = listaPrimerRonda[j].Nombre;
                            cartaVista2.Palo = listaPrimerRonda[j].Palo;
                            cartasJ1 = listaPrimerRonda;
                        }
                    }
                    if (i == 1)
                    {
                        label1.Text = jug.Nombre;
                        if (j == 0)
                        {
                            cartaVista8.Nombre = oculto;
                            cartaVista8.Palo = oculto;
                        }
                        if (j == 1)
                        {
                            cartaVista9.Nombre = oculto;
                            cartaVista9.Palo = oculto;
                            cartasJ2 = listaPrimerRonda;
                        }
                    }
                }
            }




            Jugador dealer = new Jugador("Dealer", 2);
            jugDealer = dealer;
            dealer.BarajaRepartida = listaAlmacena;
            List<Cartas> listaPrimerRondaDealer = dealer.RepartirCartasRonda(3);
            cartasDealer = listaPrimerRondaDealer;
            for (int i = 0; i < listaPrimerRondaDealer.Count; i++)
            {
                if (i == 0)
                {
                    cartaVista3.Nombre = listaPrimerRondaDealer[i].Nombre;
                    cartaVista3.Palo = listaPrimerRondaDealer[i].Palo;
                }
                if (i == 1)
                {
                    cartaVista4.Nombre = listaPrimerRondaDealer[i].Nombre;
                    cartaVista4.Palo = listaPrimerRondaDealer[i].Palo;
                }
                if (i == 2)
                {
                    cartaVista5.Nombre = listaPrimerRondaDealer[i].Nombre;
                    cartaVista5.Palo = listaPrimerRondaDealer[i].Palo;
                }
                ronda = 1;
            }


        }
        public string MostrarJugada(int indice)
        {
            List<string> jugadas = new List<string>();
            jugadas.Add("Sin juego");
            jugadas.Add("Par");
            jugadas.Add("Doble Par");
            jugadas.Add("Trio");
            jugadas.Add("Escalera");
            jugadas.Add("Color");
            jugadas.Add("Full");
            jugadas.Add("Poquer");
            jugadas.Add("Escalera Color");
            jugadas.Add("Flor Imperial");
            return jugadas[indice];
        }
        public void SegundaRonda(Jugador dealer)
        {
            //Mostrar en interface las imagenes de las primeras 3 cartas
            List<Cartas> listaSegundaRondaDealer = dealer.RepartirCartasRonda(1);
            cartasDealer = cartasDealer.Concat(listaSegundaRondaDealer).ToList();
            for (int i = 0; i < listaSegundaRondaDealer.Count; i++)
            {
                cartaVista6.Nombre = listaSegundaRondaDealer[i].Nombre;
                cartaVista6.Palo = listaSegundaRondaDealer[i].Palo;
            }
            ronda = 2;
            jugDealer = dealer;
        }
        public void TercerRonda(Jugador dealer)
        {
            List<Cartas> listaTercerRondaDealer = dealer.RepartirCartasRonda(1);
            cartasDealer = cartasDealer.Concat(listaTercerRondaDealer).ToList();

            for (int i = 0; i < listaTercerRondaDealer.Count; i++)
            {
                cartaVista7.Nombre = listaTercerRondaDealer[i].Nombre;
                cartaVista7.Palo = listaTercerRondaDealer[i].Palo;
            }
            ronda = 3;
            jugDealer = dealer;
            //sACAR LALISTA DE CARTAS DEL JUGADOR 1
            List<Cartas> cartasJug1Total = cartasDealer.Concat(cartasJ1).ToList();
            //Sacar la lista de las cartas del jugador 2
            List<Cartas> cartasJug2Total = cartasDealer.Concat(cartasJ2).ToList();
            int jugada1 = new Jugador().ValidarJugadaGanadora(cartasJug1Total.OrderBy(x => x.Valor).ToList());
            int jugada2 = new Jugador().ValidarJugadaGanadora(cartasJug2Total.OrderBy(x => x.Valor).ToList());
            for (int i = 0; i < cartasJ2.Count; i++)
            {
                if (i == 0)
                {
                    cartaVista8.Nombre = cartasJ2[i].Nombre;
                    cartaVista8.Palo = cartasJ2[i].Palo;
                }
                if (i == 1)
                {
                    cartaVista9.Nombre = cartasJ2[i].Nombre;
                    cartaVista9.Palo = cartasJ2[i].Palo;
                }
            }

            MessageBox.Show("Jugador 1:" + MostrarJugada(jugada1) + Environment.NewLine + Environment.NewLine + " Jugador 2:" + MostrarJugada(jugada2));

        }
        public Form1()
        {
            InitializeComponent();
            VolverJugar();
        }

        private void cartaVista3_Load(object sender, EventArgs e)
        {

        }

        private void cartaVista1_Load(object sender, EventArgs e)
        {

        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            VolverJugar();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ronda == 1)
            {
                SegundaRonda(jugDealer);
                return;
            }
            if (ronda == 2)
            {
                TercerRonda(jugDealer);
                return;
            }
        }
    }
}
