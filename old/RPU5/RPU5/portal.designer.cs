namespace portalesrcl
{
    partial class portal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblEpc = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.cantidad = new System.Windows.Forms.Label();
            this.botonstop = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.actualizate = new System.Windows.Forms.Button();
            this.advertencia = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.limpia = new System.Windows.Forms.Button();
            this.inventario = new System.Windows.Forms.Button();
            this.inventario2 = new System.Windows.Forms.Button();
            this.limpiasalida = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.actualizatesalida = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.totalna = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.totalna2 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.stopestaciones = new System.Windows.Forms.Button();
            this.startestaciones = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.herramientasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.signoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.ForeColor = System.Drawing.Color.Green;
            this.btnStart.Location = new System.Drawing.Point(475, 762);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(280, 95);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "START PORTAL";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblEpc
            // 
            this.lblEpc.AutoSize = true;
            this.lblEpc.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEpc.Location = new System.Drawing.Point(44, 94);
            this.lblEpc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEpc.Name = "lblEpc";
            this.lblEpc.Size = new System.Drawing.Size(308, 24);
            this.lblEpc.TabIndex = 1;
            this.lblEpc.Text = "ENTRADA DE MATERIA PRIMA";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(16, 127);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(381, 483);
            this.textBox1.TabIndex = 2;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(16, 682);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(74, 25);
            this.lblTotal.TabIndex = 3;
            this.lblTotal.Text = "Total :";
            // 
            // cantidad
            // 
            this.cantidad.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cantidad.Location = new System.Drawing.Point(103, 682);
            this.cantidad.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.cantidad.Name = "cantidad";
            this.cantidad.Size = new System.Drawing.Size(73, 25);
            this.cantidad.TabIndex = 4;
            this.cantidad.Click += new System.EventHandler(this.cantidad_Click);
            // 
            // botonstop
            // 
            this.botonstop.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botonstop.ForeColor = System.Drawing.Color.Red;
            this.botonstop.Location = new System.Drawing.Point(763, 762);
            this.botonstop.Margin = new System.Windows.Forms.Padding(4);
            this.botonstop.Name = "botonstop";
            this.botonstop.Size = new System.Drawing.Size(283, 95);
            this.botonstop.TabIndex = 5;
            this.botonstop.Text = "STOP PORTAL";
            this.botonstop.UseVisualStyleBackColor = true;
            this.botonstop.Click += new System.EventHandler(this.botonstop_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // actualizate
            // 
            this.actualizate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.actualizate.Location = new System.Drawing.Point(560, 201);
            this.actualizate.Margin = new System.Windows.Forms.Padding(4);
            this.actualizate.Name = "actualizate";
            this.actualizate.Size = new System.Drawing.Size(195, 59);
            this.actualizate.TabIndex = 6;
            this.actualizate.Text = "ACTUALIZAR PEDIDO";
            this.actualizate.UseVisualStyleBackColor = true;
            this.actualizate.Click += new System.EventHandler(this.actualizate_Click);
            // 
            // advertencia
            // 
            this.advertencia.Location = new System.Drawing.Point(411, 516);
            this.advertencia.Margin = new System.Windows.Forms.Padding(4);
            this.advertencia.Multiline = true;
            this.advertencia.Name = "advertencia";
            this.advertencia.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.advertencia.Size = new System.Drawing.Size(343, 94);
            this.advertencia.TabIndex = 7;
            this.advertencia.TextChanged += new System.EventHandler(this.advertencia_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkGreen;
            this.label1.Location = new System.Drawing.Point(67, 625);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(245, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "ELEMENTOS AUTORIZADO";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkRed;
            this.label2.Location = new System.Drawing.Point(413, 613);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(321, 40);
            this.label2.TabIndex = 9;
            this.label2.Text = "ADVERTENCIA: SE ENCONTRARON\r\n ELEMENTOS NO AUTORIZADOS";
            // 
            // limpia
            // 
            this.limpia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.limpia.Location = new System.Drawing.Point(655, 343);
            this.limpia.Margin = new System.Windows.Forms.Padding(4);
            this.limpia.Name = "limpia";
            this.limpia.Size = new System.Drawing.Size(100, 42);
            this.limpia.TabIndex = 10;
            this.limpia.Text = "CLEAR";
            this.limpia.UseVisualStyleBackColor = true;
            this.limpia.Click += new System.EventHandler(this.limpia_Click);
            // 
            // inventario
            // 
            this.inventario.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inventario.Location = new System.Drawing.Point(475, 267);
            this.inventario.Margin = new System.Windows.Forms.Padding(4);
            this.inventario.Name = "inventario";
            this.inventario.Size = new System.Drawing.Size(280, 69);
            this.inventario.TabIndex = 11;
            this.inventario.Text = "GUARDAR Y ACTUALIZAR INVENTARIO DE ENTRADA";
            this.inventario.UseVisualStyleBackColor = true;
            this.inventario.Click += new System.EventHandler(this.inventario_Click);
            // 
            // inventario2
            // 
            this.inventario2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inventario2.Location = new System.Drawing.Point(1364, 267);
            this.inventario2.Margin = new System.Windows.Forms.Padding(4);
            this.inventario2.Name = "inventario2";
            this.inventario2.Size = new System.Drawing.Size(277, 69);
            this.inventario2.TabIndex = 23;
            this.inventario2.Text = "GUARDAR Y ACTUALIZAR INVENTARIO DE SALIDA";
            this.inventario2.UseVisualStyleBackColor = true;
            this.inventario2.Click += new System.EventHandler(this.inventario2_Click);
            // 
            // limpiasalida
            // 
            this.limpiasalida.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.limpiasalida.Location = new System.Drawing.Point(1541, 343);
            this.limpiasalida.Margin = new System.Windows.Forms.Padding(4);
            this.limpiasalida.Name = "limpiasalida";
            this.limpiasalida.Size = new System.Drawing.Size(100, 42);
            this.limpiasalida.TabIndex = 22;
            this.limpiasalida.Text = "CLEAR";
            this.limpiasalida.UseVisualStyleBackColor = true;
            this.limpiasalida.Click += new System.EventHandler(this.limpiasalida_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkRed;
            this.label3.Location = new System.Drawing.Point(1347, 613);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(230, 40);
            this.label3.TabIndex = 21;
            this.label3.Text = "         ADVERTENCIA: \r\nSALIDA NO AUTORIZADA";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkGreen;
            this.label4.Location = new System.Drawing.Point(953, 625);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(249, 20);
            this.label4.TabIndex = 20;
            this.label4.Text = "PRODUCTOS AUTORIZADO";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(1297, 516);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox2.Size = new System.Drawing.Size(343, 94);
            this.textBox2.TabIndex = 19;
            // 
            // actualizatesalida
            // 
            this.actualizatesalida.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.actualizatesalida.Location = new System.Drawing.Point(1447, 201);
            this.actualizatesalida.Margin = new System.Windows.Forms.Padding(4);
            this.actualizatesalida.Name = "actualizatesalida";
            this.actualizatesalida.Size = new System.Drawing.Size(195, 59);
            this.actualizatesalida.TabIndex = 18;
            this.actualizatesalida.Text = "ACTUALIZAR PEDIDO";
            this.actualizatesalida.UseVisualStyleBackColor = true;
            this.actualizatesalida.Click += new System.EventHandler(this.actualizatesalida_Click);
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(989, 682);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 25);
            this.label5.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(903, 682);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 25);
            this.label6.TabIndex = 15;
            this.label6.Text = "Total :";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(903, 127);
            this.textBox3.Margin = new System.Windows.Forms.Padding(4);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox3.Size = new System.Drawing.Size(381, 483);
            this.textBox3.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(953, 94);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(248, 24);
            this.label7.TabIndex = 13;
            this.label7.Text = "SALIDA DE PRODUCTOS";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Navy;
            this.label8.Location = new System.Drawing.Point(1221, 28);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(168, 46);
            this.label8.TabIndex = 24;
            this.label8.Text = "SALIDA";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Navy;
            this.label9.Location = new System.Drawing.Point(278, 38);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(219, 46);
            this.label9.TabIndex = 25;
            this.label9.Text = "ENTRADA";
            // 
            // totalna
            // 
            this.totalna.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.totalna.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalna.Location = new System.Drawing.Point(499, 682);
            this.totalna.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.totalna.Name = "totalna";
            this.totalna.Size = new System.Drawing.Size(73, 25);
            this.totalna.TabIndex = 27;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(412, 682);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 25);
            this.label11.TabIndex = 26;
            this.label11.Text = "Total :";
            // 
            // totalna2
            // 
            this.totalna2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.totalna2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalna2.Location = new System.Drawing.Point(1387, 681);
            this.totalna2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.totalna2.Name = "totalna2";
            this.totalna2.Size = new System.Drawing.Size(73, 25);
            this.totalna2.TabIndex = 29;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(1300, 681);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(74, 25);
            this.label12.TabIndex = 28;
            this.label12.Text = "Total :";
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // stopestaciones
            // 
            this.stopestaciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stopestaciones.ForeColor = System.Drawing.Color.Red;
            this.stopestaciones.Location = new System.Drawing.Point(1341, 762);
            this.stopestaciones.Margin = new System.Windows.Forms.Padding(4);
            this.stopestaciones.Name = "stopestaciones";
            this.stopestaciones.Size = new System.Drawing.Size(283, 95);
            this.stopestaciones.TabIndex = 30;
            this.stopestaciones.Text = "STOP ESTACIONES";
            this.stopestaciones.UseVisualStyleBackColor = true;
            this.stopestaciones.Click += new System.EventHandler(this.stopestaciones_Click);
            // 
            // startestaciones
            // 
            this.startestaciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startestaciones.ForeColor = System.Drawing.Color.Green;
            this.startestaciones.Location = new System.Drawing.Point(1053, 762);
            this.startestaciones.Margin = new System.Windows.Forms.Padding(4);
            this.startestaciones.Name = "startestaciones";
            this.startestaciones.Size = new System.Drawing.Size(280, 95);
            this.startestaciones.TabIndex = 31;
            this.startestaciones.Text = "START ESTACIONES";
            this.startestaciones.UseVisualStyleBackColor = true;
            this.startestaciones.Click += new System.EventHandler(this.startestaciones_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(11, 731);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(312, 25);
            this.label13.TabIndex = 32;
            this.label13.Text = "Lectores Portal Desconectados";
            this.label13.Click += new System.EventHandler(this.label13_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(11, 777);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(362, 25);
            this.label15.TabIndex = 34;
            this.label15.Text = "Lectores Estaciones Desconectados";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Silver;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.herramientasToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1816, 31);
            this.menuStrip1.TabIndex = 35;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // herramientasToolStripMenuItem
            // 
            this.herramientasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.signoutToolStripMenuItem,
            this.salirToolStripMenuItem});
            this.herramientasToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.herramientasToolStripMenuItem.Name = "herramientasToolStripMenuItem";
            this.herramientasToolStripMenuItem.Size = new System.Drawing.Size(130, 27);
            this.herramientasToolStripMenuItem.Text = "Herramientas";
            // 
            // signoutToolStripMenuItem
            // 
            this.signoutToolStripMenuItem.Name = "signoutToolStripMenuItem";
            this.signoutToolStripMenuItem.Size = new System.Drawing.Size(192, 28);
            this.signoutToolStripMenuItem.Text = "Cerrar Sesión";
            this.signoutToolStripMenuItem.Click += new System.EventHandler(this.signoutToolStripMenuItem_Click);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(192, 28);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // portal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Silver;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1816, 859);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.startestaciones);
            this.Controls.Add(this.stopestaciones);
            this.Controls.Add(this.totalna2);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.totalna);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.inventario2);
            this.Controls.Add(this.limpiasalida);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.actualizatesalida);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.inventario);
            this.Controls.Add(this.limpia);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.advertencia);
            this.Controls.Add(this.actualizate);
            this.Controls.Add(this.botonstop);
            this.Controls.Add(this.cantidad);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lblEpc);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "portal";
            this.Text = "Portales GUI";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.portal_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblEpc;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label cantidad;
        private System.Windows.Forms.Button botonstop;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button actualizate;
        private System.Windows.Forms.TextBox advertencia;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button limpia;
        private System.Windows.Forms.Button inventario;
        private System.Windows.Forms.Button inventario2;
        private System.Windows.Forms.Button limpiasalida;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button actualizatesalida;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label totalna;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label totalna2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Button stopestaciones;
        private System.Windows.Forms.Button startestaciones;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem herramientasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem signoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
    }
}

