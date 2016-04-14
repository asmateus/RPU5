namespace portalesrcl
{
    partial class login
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
            this.txtuser = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtpass = new System.Windows.Forms.TextBox();
            this.showpass = new System.Windows.Forms.CheckBox();
            this.ingresar = new System.Windows.Forms.Button();
            this.borrar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(159, 41);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Usuario:";
            // 
            // txtuser
            // 
            this.txtuser.Location = new System.Drawing.Point(132, 65);
            this.txtuser.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtuser.Name = "txtuser";
            this.txtuser.Size = new System.Drawing.Size(106, 20);
            this.txtuser.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(153, 94);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Contraseña:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // txtpass
            // 
            this.txtpass.Location = new System.Drawing.Point(132, 109);
            this.txtpass.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtpass.Name = "txtpass";
            this.txtpass.Size = new System.Drawing.Size(106, 20);
            this.txtpass.TabIndex = 3;
            this.txtpass.UseSystemPasswordChar = true;
            this.txtpass.TextChanged += new System.EventHandler(this.txtpass_TextChanged);
            // 
            // showpass
            // 
            this.showpass.AutoSize = true;
            this.showpass.Location = new System.Drawing.Point(120, 150);
            this.showpass.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.showpass.Name = "showpass";
            this.showpass.Size = new System.Drawing.Size(118, 17);
            this.showpass.TabIndex = 4;
            this.showpass.Text = "Mostrar Contraseña";
            this.showpass.UseVisualStyleBackColor = true;
            this.showpass.CheckedChanged += new System.EventHandler(this.showpass_CheckedChanged);
            // 
            // ingresar
            // 
            this.ingresar.Location = new System.Drawing.Point(95, 180);
            this.ingresar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ingresar.Name = "ingresar";
            this.ingresar.Size = new System.Drawing.Size(62, 32);
            this.ingresar.TabIndex = 5;
            this.ingresar.Text = "Ingresar";
            this.ingresar.UseVisualStyleBackColor = true;
            this.ingresar.Click += new System.EventHandler(this.ingresar_Click);
            // 
            // borrar
            // 
            this.borrar.Location = new System.Drawing.Point(209, 180);
            this.borrar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.borrar.Name = "borrar";
            this.borrar.Size = new System.Drawing.Size(62, 32);
            this.borrar.TabIndex = 6;
            this.borrar.Text = "Borrar";
            this.borrar.UseVisualStyleBackColor = true;
            this.borrar.Click += new System.EventHandler(this.borrar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(32, 9);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(294, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "RFID Project Unit 5.2 (Te queremos luis)";
            // 
            // cancelar
            // 
            this.cancelar.Location = new System.Drawing.Point(155, 216);
            this.cancelar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cancelar.Name = "cancelar";
            this.cancelar.Size = new System.Drawing.Size(62, 32);
            this.cancelar.TabIndex = 9;
            this.cancelar.Text = "Cancelar";
            this.cancelar.UseVisualStyleBackColor = true;
            this.cancelar.Click += new System.EventHandler(this.cancelar_Click);
            // 
            // login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(357, 252);
            this.Controls.Add(this.cancelar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.borrar);
            this.Controls.Add(this.ingresar);
            this.Controls.Add(this.showpass);
            this.Controls.Add(this.txtpass);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtuser);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "login";
            this.Text = "Portales";
            this.Load += new System.EventHandler(this.login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtuser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtpass;
        private System.Windows.Forms.CheckBox showpass;
        private System.Windows.Forms.Button ingresar;
        private System.Windows.Forms.Button borrar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button cancelar;
    }
}