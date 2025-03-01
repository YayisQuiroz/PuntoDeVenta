namespace AppPuntodeVentas
{
    partial class Busquedas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBusqueda = new System.Windows.Forms.TextBox();
            this.btnRegresar = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.cbbCategorias = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // textBusqueda
            // 
            this.textBusqueda.Location = new System.Drawing.Point(105, 55);
            this.textBusqueda.Name = "textBusqueda";
            this.textBusqueda.Size = new System.Drawing.Size(173, 22);
            this.textBusqueda.TabIndex = 1;
            // 
            // btnRegresar
            // 
            this.btnRegresar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnRegresar.Location = new System.Drawing.Point(39, 131);
            this.btnRegresar.Name = "btnRegresar";
            this.btnRegresar.Size = new System.Drawing.Size(75, 23);
            this.btnRegresar.TabIndex = 2;
            this.btnRegresar.Text = "Regresar";
            this.btnRegresar.UseVisualStyleBackColor = true;
            // 
            // btnBuscar
            // 
            this.btnBuscar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnBuscar.Location = new System.Drawing.Point(229, 131);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 3;
            this.btnBuscar.Text = "buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            // 
            // cbbCategorias
            // 
            this.cbbCategorias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbCategorias.FormattingEnabled = true;
            this.cbbCategorias.Items.AddRange(new object[] {
            "Miscelanea",
            "Dulces y Botana",
            "Bebidas",
            "Despensa Básica",
            "Higiene y Cuidado Personal"});
            this.cbbCategorias.Location = new System.Drawing.Point(105, 58);
            this.cbbCategorias.Name = "cbbCategorias";
            this.cbbCategorias.Size = new System.Drawing.Size(173, 24);
            this.cbbCategorias.TabIndex = 4;
            // 
            // Busquedas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 192);
            this.ControlBox = false;
            this.Controls.Add(this.cbbCategorias);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.btnRegresar);
            this.Controls.Add(this.textBusqueda);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Busquedas";
            this.Text = "Busquedas";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBusqueda;
        private System.Windows.Forms.Button btnRegresar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.ComboBox cbbCategorias;
    }
}