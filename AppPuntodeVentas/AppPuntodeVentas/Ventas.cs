using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppPuntodeVentas
{
    public partial class Ventas : Form
    {
        string id, categoria, descripcion, precio, stock;
        public Ventas()
        {
            InitializeComponent();
            CargarProductos();
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem itemSeleccionado = listView1.SelectedItems[0];

                string descripcion = itemSeleccionado.SubItems[1].Text; 
                string precio = itemSeleccionado.SubItems[3].Text;

                txtCantidad.Text = "1";
                lblProducto.Text = $"Producto:${descripcion}";
                label1.Tag = precio; 
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Seleccione un producto primero.");
                return;
            }
            ListViewItem itemSeleccionado = listView1.SelectedItems[0];

            string descripcion = itemSeleccionado.SubItems[1].Text;
            decimal precio = Convert.ToDecimal(itemSeleccionado.SubItems[3].Text);
            int cantidad = Convert.ToInt32(txtCantidad.Text);
            decimal total = cantidad * precio;

            ListViewItem nuevoItem = new ListViewItem(descripcion);
            nuevoItem.SubItems.Add(cantidad.ToString());
            nuevoItem.SubItems.Add(total.ToString("0.00"));
            
            listView2.Items.Add(nuevoItem);

            ActualizarTotal();

        }
        private void btnVender_Click(object sender, EventArgs e)
        {
            if (listView2.Items.Count == 0)
            {
                MessageBox.Show("No hay productos en la venta.", "Venta Vacía", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal totalVenta = CalcularTotalDesdeListView();
            decimal pago = PagoTarjeta.Checked ? totalVenta : 0;

            if (!PagoTarjeta.Checked && !decimal.TryParse(txtPago.Text, out pago))
            {
                MessageBox.Show("Ingrese un monto válido en el pago.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (pago < totalVenta)
            {
                MessageBox.Show("El pago es insuficiente. Por favor, ingrese un monto mayor.", "Pago Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal cambio = PagoTarjeta.Checked ? 0 : pago - totalVenta;
            string fecha = dateTimePicker1.Value.ToString("yyyy-MM-dd");

            try
            {
                using (StreamWriter writer = new StreamWriter("Ventas.txt", true))
                {
                    writer.WriteLine($"Fecha: {fecha}");
                    writer.WriteLine("Productos:");

                    foreach (ListViewItem item in listView2.Items)
                    {
                        string descripcion = item.SubItems[0].Text;
                        int cantidad = Convert.ToInt32(item.SubItems[1].Text);
                        decimal total = Convert.ToDecimal(item.SubItems[2].Text);

                        writer.WriteLine($"- {descripcion} x{cantidad} = ${total:0.00}");
                    }

                    writer.WriteLine($"TOTAL: ${totalVenta:0.00}");
                    writer.WriteLine($"PAGO: ${pago:0.00}");
                    writer.WriteLine($"CAMBIO: ${cambio:0.00}");
                    writer.WriteLine("----------------------------------");
                }

                MessageBox.Show("Venta guardada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                listView2.Items.Clear();
                txtPago.Clear();
                lblResultado.Text = "Total: $0";
                lblFeria.Text = "Cambio: $0";

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar la venta: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                listView2.Items.Remove(listView2.SelectedItems[0]);
                ActualizarTotal(); 
            }
            else
            {
                MessageBox.Show("Seleccione un producto para eliminar.", "Eliminar Producto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtCantidad.Text, out int cantidad) && cantidad > 0)
            {
                if (label1.Tag != null)
                {
                    float precio = float.Parse(label1.Tag.ToString());
                    float total = cantidad * precio; 
                }
            }
        }
        private void txtPago_TextChanged(object sender, EventArgs e)
        {
            decimal total = CalcularTotalDesdeListView();

            if (PagoTarjeta.Checked) 
            {
                txtPago.Text = total.ToString("0.00");
                lblFeria.Text = "Pago con tarjeta";
                lblFeria.ForeColor = Color.Blue;
                return;
            }

            if (decimal.TryParse(txtPago.Text, out decimal pago))
            {
                decimal cambio = pago - total;

                if (cambio < 0)
                {
                    lblFeria.Text = "Pago insuficiente";
                    lblFeria.ForeColor = Color.Red;
                }
                else
                {
                    lblFeria.Text = $"Cambio: ${cambio:F2}";
                    lblFeria.ForeColor = Color.Black;
                }
            }
            else
            {
                lblFeria.Text = "Ingrese un monto válido";
                lblFeria.ForeColor = Color.Red;
            }
        }


        #region Metodo

        private void PagoTarjeta_CheckedChanged(object sender, EventArgs e)
        {
            if (PagoTarjeta.Checked)
            {
                txtPago.Text = CalcularTotalDesdeListView().ToString("0.00");
                txtPago.Enabled = false; 
            }
            else
            {
                txtPago.Clear();
                txtPago.Enabled = true;
            }
        }

        private void CargarProductos()
        {
            listView1.Items.Clear();
            try
            {
                StreamReader sr = new StreamReader("Productos.txt");
                Lectura(sr);
                while (id != null)
                {
                    AgregarLv();
                    Lectura(sr);
                }
                sr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al leer el archivo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Ventas_Load(object sender, EventArgs e)
        {
            CargarProductos();
        }
        private void Lectura(StreamReader sr)
        {
            id = sr.ReadLine();
            descripcion = sr.ReadLine();
            categoria = sr.ReadLine();
            precio = sr.ReadLine();
            stock = sr.ReadLine();
        }
        private void AgregarLv()
        {
            ListViewItem Datos = new ListViewItem(id);
            Datos.SubItems.Add(descripcion);
            Datos.SubItems.Add(categoria);
            Datos.SubItems.Add(precio);
            Datos.SubItems.Add(stock);
            listView1.Items.Add(Datos);
        }
        private void ActualizarTotal()
        {
            decimal totalVenta = 0;

            foreach (ListViewItem item in listView2.Items)
            {
                totalVenta += Convert.ToDecimal(item.SubItems[2].Text);
            }
            lblResultado.Text = "Total: $" + totalVenta.ToString("0.00");
        }
        private decimal CalcularTotalDesdeListView()
        {
            decimal total = 0;

            foreach (ListViewItem item in listView2.Items)
            {
                if (decimal.TryParse(item.SubItems[2].Text, out decimal subtotal)) 
                {
                    total += subtotal;
                }
            }

            return total;
        }
        #endregion
    }
}
