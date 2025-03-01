using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppPuntodeVentas
{
    public partial class AltasProducto : Form
    {
        private bool Numero = false;
        public AltasProducto()
        {
            InitializeComponent();
        }

        public string ID
        {
            get { return txtID.Text; }
            set { txtID.Text = value; }
        }
        public string Categoria
        {
            get { return cbbCategorias.Text; }
            set {  cbbCategorias.Text = value;}
        }
        public string Descripcion
        {
            get { return txtDescripcion.Text; }
            set { txtDescripcion.Text = value; }
        }
        public float Precio
        {
            get { return float.Parse(txtPrecio.Text); }
            set { txtPrecio.Text=value.ToString(); }
        }
        public int Stock
        {
            get { return int.Parse(txtStock.Text); }
            set { txtStock.Text = value.ToString(); }
        }
        public bool SoloNumeros
        {
            get { return Numero; }
            set { Numero = value; }
        }
        private void TextBox_SoloNumeros(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
