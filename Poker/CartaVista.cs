using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Poker
{
    public partial class CartaVista : UserControl
    {
        public string Nombre
        {
            get
            {
                return string.Empty;
            }

            set
            {
                lblNumero.Text = value;
                lblNumero2.Text = value;
            }
        }
        public string Palo
        {
            get { return string.Empty; }
            set
            {
                lblNumero.ForeColor = Color.Black;
                lblPalo.ForeColor = Color.Black;
                lblNumero2.ForeColor = Color.Black;
                if (value == "♥" || value == "♦")
                {
                    lblNumero.ForeColor = Color.Red;
                    lblPalo.ForeColor = Color.Red;
                    lblNumero2.ForeColor = Color.Red;
                }
                lblPalo.Text = value;
            }
        }
        public CartaVista()
        {
            InitializeComponent();
        }
        public CartaVista(string numero, string palo)
        {
            InitializeComponent();
            lblNumero.Text = numero;
            lblNumero2.Text = numero;
            lblPalo.Text = palo;
        }
    }
}
