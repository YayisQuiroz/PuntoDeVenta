using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppPuntodeVentas
{
    public partial class Form1 : Form
    {
        //pantalla Punto de Venta, que cree(Altas) el producto, elimine(bajas) (o en cosecuencial esten inactivos), modifique el producto, Consultas
        //1.Pantalla Venta
        //boton agg producto(button)*
        //quitar producto(button)*
        //Colocar la cantidad de producto que sea necesario
        //calcular suma total de la venta "individual" (Label)
        //calcular feria si es en efectivo(txt)
        //total de la feria que se va a dar (label)

        string id, categoria, descripcion, precio, stock;
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

            StreamWriter sw = new StreamWriter("Productos.txt", true, Encoding.ASCII);
            AltasProducto Altas = new AltasProducto();
            if (Altas.ShowDialog() == DialogResult.OK)
            {
                sw.WriteLine(Altas.ID);
                sw.WriteLine(Altas.Descripcion);
                sw.WriteLine(Altas.Categoria);
                sw.WriteLine(Altas.Precio);
                sw.WriteLine(Altas.Stock);
                MessageBox.Show("Los productos se guardaron correctamente", "altas de productos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                sw.Close();
                listView1.Items.Clear();
                using (StreamReader sr = new StreamReader("Productos.txt"))
                {
                    while (!sr.EndOfStream)
                    {
                        Lectura(sr);
                        AgregarLv();

                    }
                }
            }
        }

        private void porIDToolStripMenuItem_Click(object sender, EventArgs e)
        {

            listView1.Items.Clear();
            StreamReader sr = new StreamReader("Productos.txt");
            Busquedas busquedas = new Busquedas();
            busquedas.Text = "Busquedas por ID";
            busquedas.Categoria = false;
            busquedas.TextoLabel = "ID:";
            if (busquedas.ShowDialog() == DialogResult.OK)
            {
                bool bandera = true;
                Lectura(sr);
                id.ToLower();
                while (id != null && bandera == true)
                {
                    if (id == busquedas.Parametro.ToLower())
                    {
                        bandera = false;
                        AgregarLv();

                    }
                    Lectura(sr);
                }
                sr.Close();
                if (bandera == true)
                {
                    MessageBox.Show("La ID del producto no existe", "Problema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ventasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide(); 

            Ventas Ventanueva = new Ventas();

            if (Ventanueva.ShowDialog() == DialogResult.Cancel)
            {
                this.Show();
            }
        }

        private void TodosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            StreamReader sr = new StreamReader("Productos.txt");
            Lectura(sr);

            while (id != null)
            {
                AgregarLv();
                Lectura(sr);
            }
            sr.Close();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string archivoOriginal = "Productos.txt";
            string archivoAuxiliar = "Auxiliar.txt";

            Busquedas eliminar = new Busquedas();
            eliminar.Text = "Eliminar";
            eliminar.TextoLabel = "ID:";
            eliminar.Categoria = false;
            eliminar.TextoBoton = "Confirmar";

            if (eliminar.ShowDialog() == DialogResult.OK)
            {
                string IdAEliminar = eliminar.Parametro;

                try
                {
                    using (StreamReader sr = new StreamReader(archivoOriginal))
                    using (StreamWriter sw = new StreamWriter(archivoAuxiliar))
                    {
                        while (!sr.EndOfStream)
                        {
                            Lectura(sr);
                            if (id.Trim().ToLower() == IdAEliminar.Trim().ToLower())
                                continue;
                            Escritura(sw);
                        }
                        sr.Close();
                        sw.Close();
                    }
                    File.Delete(archivoOriginal);
                    File.Move(archivoAuxiliar, archivoOriginal);

                    MessageBox.Show("Registro eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    listView1.Items.Clear();
                    using (StreamReader sr = new StreamReader("Productos.txt"))
                    {
                        while (!sr.EndOfStream)
                        {
                            Lectura(sr);
                            AgregarLv();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar el registro: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string archivoOriginal = "Productos.txt";
            string archivoAuxiliar = "Auxiliar.txt";

            Busquedas modificar = new Busquedas();
            modificar.Text = "Modificar";
            modificar.TextoLabel = "ID:";
            modificar.Categoria = false;
            modificar.TextoBoton = "Confirmar";
            if (modificar.ShowDialog() == DialogResult.OK)
            {
                string IdAmodificar = modificar.Parametro;
                bool Modificado = true;
                bool encontrado = false;

                try
                {
                    using (StreamReader sr = new StreamReader(archivoOriginal))
                    using (StreamWriter sw = new StreamWriter(archivoAuxiliar))
                    {
                        while (!sr.EndOfStream)
                        {
                            Lectura(sr);

                            if (id.Trim().ToLower() == IdAmodificar.Trim().ToLower())
                            {
                                encontrado = true;
                                AltasProducto modificacion = new AltasProducto();
                                modificacion.Text = "Producto a Modificar";
                                modificacion.ID = id;
                                modificacion.Descripcion = descripcion;
                                modificacion.Categoria = categoria;
                                modificacion.Precio = float.Parse(precio);
                                modificacion.Stock = int.Parse(stock);

                                if (modificacion.ShowDialog() == DialogResult.OK)
                                {
                                    sw.WriteLine(modificacion.ID);
                                    sw.WriteLine(modificacion.Descripcion);
                                    sw.WriteLine(modificacion.Categoria);
                                    sw.WriteLine(modificacion.Precio);
                                    sw.WriteLine(modificacion.Stock);
                                    listView1.Items.Clear();
                                }
                                else
                                {
                                    Escritura(sw);
                                }
                            }
                            else
                            {
                                Escritura(sw);
                            }

                        }
                        sr.Close();
                        sw.Close();
                    }
                    File.Delete(archivoOriginal);
                    File.Move(archivoAuxiliar, archivoOriginal);

                    if (encontrado && Modificado)
                    {
                        MessageBox.Show("Registro modificado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        listView1.Items.Clear();
                        using (StreamReader sr = new StreamReader("Productos.txt"))
                        {
                            while (!sr.EndOfStream)
                            {
                                Lectura(sr);
                                AgregarLv();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("ID no encontrada.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al modificar el registro: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void porCategoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            StreamReader sr = new StreamReader("Productos.txt");
            Busquedas busquedas = new Busquedas();
            busquedas.Text = "Busquedas por Categoria";
            busquedas.Categoria = true;
            busquedas.TextoLabel = "Categoria:";
            if (busquedas.ShowDialog() == DialogResult.OK)
            {
                bool encontrado = false;
                Lectura(sr);
                while (id != null)
                {
                    if (categoria.ToLower() == busquedas.Parametro.ToLower())
                    {
                        encontrado = true;
                        AgregarLv();
                    }
                    Lectura(sr);
                }
                if (!encontrado)
                {
                    MessageBox.Show("categoria no encontrada", "Problema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                sr.Close();
            }
        }

        #region Metodos
        private void Lectura(StreamReader sr)
        {
            id = sr.ReadLine();
            descripcion = sr.ReadLine();
            categoria = sr.ReadLine();
            precio = sr.ReadLine();
            stock = sr.ReadLine();
        }
        private void Escritura(StreamWriter sw)
        {
            sw.WriteLine(id);
            sw.WriteLine(descripcion);
            sw.WriteLine(categoria);
            sw.WriteLine(precio);
            sw.WriteLine(stock);
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

        #endregion
    }
}