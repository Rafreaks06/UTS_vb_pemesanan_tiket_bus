namespace bus_ticket_booking.Forms
{
    partial class HomePage
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
            components = new System.ComponentModel.Container();
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            fToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            menuPelanggan = new System.Windows.Forms.ToolStripMenuItem();
            menuBus = new System.Windows.Forms.ToolStripMenuItem();
            menuSale = new System.Windows.Forms.ToolStripMenuItem();
            menuLaporan = new System.Windows.Forms.ToolStripMenuItem();
            contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
            panel1 = new System.Windows.Forms.Panel();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fToolStripMenuItem, menuPelanggan, menuBus, menuSale, menuLaporan });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            menuStrip1.Size = new System.Drawing.Size(933, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fToolStripMenuItem
            // 
            fToolStripMenuItem.Name = "fToolStripMenuItem";
            fToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            fToolStripMenuItem.Text = "File";
            // 
            // menuPelanggan
            // 
            menuPelanggan.Name = "menuPelanggan";
            menuPelanggan.Size = new System.Drawing.Size(99, 20);
            menuPelanggan.Text = "Data Passenger";
            menuPelanggan.Click += menuPelanggan_Click;
            // 
            // menuBus
            // 
            menuBus.Name = "menuBus";
            menuBus.Size = new System.Drawing.Size(68, 20);
            menuBus.Text = "Data Bus ";
            menuBus.Click += menuBus_Click;
            // 
            // menuSale
            // 
            menuSale.Name = "menuSale";
            menuSale.Size = new System.Drawing.Size(40, 20);
            menuSale.Text = "Sale";
            menuSale.Click += menuSale_Click;
            // 
            // menuLaporan
            // 
            menuLaporan.Name = "menuLaporan";
            menuLaporan.Size = new System.Drawing.Size(62, 20);
            menuLaporan.Text = "Laporan";
            menuLaporan.Click += menuLaporan_Click;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // panel1
            // 
            panel1.Location = new System.Drawing.Point(0, 31);
            panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(933, 488);
            panel1.TabIndex = 2;
            panel1.Paint += panel1_Paint;
            // 
            // HomePage
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(933, 519);
            Controls.Add(panel1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "HomePage";
            Text = "HomePage";
            Load += HomePage_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuPelanggan;
        private System.Windows.Forms.ToolStripMenuItem menuBus;
        private System.Windows.Forms.ToolStripMenuItem menuSale;
        private System.Windows.Forms.ToolStripMenuItem menuLaporan;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Panel panel1;
    }
}