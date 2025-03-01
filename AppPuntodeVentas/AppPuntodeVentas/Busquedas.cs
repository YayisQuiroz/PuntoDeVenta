using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AppPuntodeVentas
{
    public partial class Busquedas : Form
    {
        private bool EsCategoria=false;
        private bool Numero = false;
        public Busquedas()
        {
            InitializeComponent();
        }
        public string Parametro
        {
            get {return EsCategoria?cbbCategorias.SelectedItem?.ToString():textBusqueda.Text; }
        }

        public string TextoLabel
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }
        public bool SoloNumeros
        {
            get { return Numero; }
            set { Numero = value; }
        }
        public bool Categoria
        {
            set
            {
                EsCategoria=value;
                textBusqueda.Visible = !value;
                cbbCategorias.Visible = value;
            }
        }
        public void TextBox1(object sender, KeyPressEventArgs e)
        {
            if (SoloNumeros == true)
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        public string TextoBoton
        {
            get { return btnBuscar.Text; }
            set { btnBuscar.Text = value; }
        }
    }
}
