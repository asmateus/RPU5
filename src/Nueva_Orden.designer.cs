namespace RPU5
{
    partial class Nueva_Orden
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.Cant_Avion = new System.Windows.Forms.NumericUpDown();
            this.Cant_Carro = new System.Windows.Forms.NumericUpDown();
            this.Cant_Dinosaurio = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.Cant_Avion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cant_Carro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cant_Dinosaurio)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Avión";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Carro";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Dinosaurio";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(100, 90);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(74, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Guardar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Guardar_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(12, 90);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(68, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "Salir";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Cancelar_Click);
            // 
            // Cant_Avion
            // 
            this.Cant_Avion.Location = new System.Drawing.Point(108, 7);
            this.Cant_Avion.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.Cant_Avion.Name = "Cant_Avion";
            this.Cant_Avion.Size = new System.Drawing.Size(46, 20);
            this.Cant_Avion.TabIndex = 11;
            this.Cant_Avion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Cant_Carro
            // 
            this.Cant_Carro.Location = new System.Drawing.Point(108, 33);
            this.Cant_Carro.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.Cant_Carro.Name = "Cant_Carro";
            this.Cant_Carro.Size = new System.Drawing.Size(46, 20);
            this.Cant_Carro.TabIndex = 11;
            this.Cant_Carro.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Cant_Dinosaurio
            // 
            this.Cant_Dinosaurio.Location = new System.Drawing.Point(108, 59);
            this.Cant_Dinosaurio.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.Cant_Dinosaurio.Name = "Cant_Dinosaurio";
            this.Cant_Dinosaurio.Size = new System.Drawing.Size(46, 20);
            this.Cant_Dinosaurio.TabIndex = 11;
            this.Cant_Dinosaurio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Nueva_Orden
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(194, 125);
            this.Controls.Add(this.Cant_Dinosaurio);
            this.Controls.Add(this.Cant_Carro);
            this.Controls.Add(this.Cant_Avion);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "Nueva_Orden";
            this.Text = "Nueva Orden";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.Cant_Avion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cant_Carro)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cant_Dinosaurio)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.NumericUpDown Cant_Avion;
        private System.Windows.Forms.NumericUpDown Cant_Carro;
        private System.Windows.Forms.NumericUpDown Cant_Dinosaurio;
    }
}