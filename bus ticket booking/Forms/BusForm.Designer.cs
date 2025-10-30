namespace bus_ticket_booking.Forms
{
    partial class BusForm
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
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            txtBusName = new System.Windows.Forms.TextBox();
            txtAsalKota = new System.Windows.Forms.TextBox();
            txtJumlahKursi = new System.Windows.Forms.TextBox();
            txtKotaTujuan = new System.Windows.Forms.TextBox();
            txtHargaTiket = new System.Windows.Forms.TextBox();
            btnAdd = new System.Windows.Forms.Button();
            btnUpdate = new System.Windows.Forms.Button();
            btnDelete = new System.Windows.Forms.Button();
            btnClear = new System.Windows.Forms.Button();
            label7 = new System.Windows.Forms.Label();
            dataGridViewBus = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridViewBus).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label1.Location = new System.Drawing.Point(304, 10);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(254, 25);
            label1.TabIndex = 0;
            label1.Text = "Bus Management Form";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(24, 74);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(61, 15);
            label2.TabIndex = 1;
            label2.Text = "Nama Bus";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(24, 120);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(56, 15);
            label3.TabIndex = 2;
            label3.Text = "Asal Kota";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(24, 165);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(68, 15);
            label4.TabIndex = 3;
            label4.Text = "Harga Tiket";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(322, 77);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(71, 15);
            label5.TabIndex = 4;
            label5.Text = "Kota Tujuan";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(322, 122);
            label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(77, 15);
            label6.TabIndex = 5;
            label6.Text = "Jumlah Kursi ";
            // 
            // txtBusName
            // 
            txtBusName.Location = new System.Drawing.Point(133, 74);
            txtBusName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtBusName.Name = "txtBusName";
            txtBusName.Size = new System.Drawing.Size(166, 23);
            txtBusName.TabIndex = 6;
            txtBusName.TextChanged += txtBusName_TextChanged;
            // 
            // txtAsalKota
            // 
            txtAsalKota.Location = new System.Drawing.Point(133, 120);
            txtAsalKota.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtAsalKota.Name = "txtAsalKota";
            txtAsalKota.Size = new System.Drawing.Size(166, 23);
            txtAsalKota.TabIndex = 7;
            // 
            // txtJumlahKursi
            // 
            txtJumlahKursi.Location = new System.Drawing.Point(418, 122);
            txtJumlahKursi.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtJumlahKursi.Name = "txtJumlahKursi";
            txtJumlahKursi.Size = new System.Drawing.Size(206, 23);
            txtJumlahKursi.TabIndex = 8;
            // 
            // txtKotaTujuan
            // 
            txtKotaTujuan.Location = new System.Drawing.Point(418, 77);
            txtKotaTujuan.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtKotaTujuan.Name = "txtKotaTujuan";
            txtKotaTujuan.Size = new System.Drawing.Size(206, 23);
            txtKotaTujuan.TabIndex = 9;
            // 
            // txtHargaTiket
            // 
            txtHargaTiket.Location = new System.Drawing.Point(133, 157);
            txtHargaTiket.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtHargaTiket.Name = "txtHargaTiket";
            txtHargaTiket.Size = new System.Drawing.Size(166, 23);
            txtHargaTiket.TabIndex = 10;
            // 
            // btnAdd
            // 
            btnAdd.Location = new System.Drawing.Point(326, 159);
            btnAdd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new System.Drawing.Size(88, 27);
            btnAdd.TabIndex = 11;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new System.Drawing.Point(424, 159);
            btnUpdate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new System.Drawing.Size(88, 27);
            btnUpdate.TabIndex = 12;
            btnUpdate.Text = "Update ";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new System.Drawing.Point(537, 159);
            btnDelete.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new System.Drawing.Size(88, 27);
            btnDelete.TabIndex = 13;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new System.Drawing.Point(654, 159);
            btnClear.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnClear.Name = "btnClear";
            btnClear.Size = new System.Drawing.Size(88, 27);
            btnClear.TabIndex = 14;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label7.Location = new System.Drawing.Point(412, 231);
            label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(123, 25);
            label7.TabIndex = 15;
            label7.Text = "Daftar Bus";
            label7.Click += label7_Click;
            // 
            // dataGridViewBus
            // 
            dataGridViewBus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewBus.Location = new System.Drawing.Point(0, 263);
            dataGridViewBus.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            dataGridViewBus.Name = "dataGridViewBus";
            dataGridViewBus.Size = new System.Drawing.Size(1029, 347);
            dataGridViewBus.TabIndex = 16;
            dataGridViewBus.CellContentClick += dataGridView1_CellContentClick;
            // 
            // BusForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1032, 637);
            Controls.Add(dataGridViewBus);
            Controls.Add(label7);
            Controls.Add(btnClear);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnAdd);
            Controls.Add(txtHargaTiket);
            Controls.Add(txtKotaTujuan);
            Controls.Add(txtJumlahKursi);
            Controls.Add(txtAsalKota);
            Controls.Add(txtBusName);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "BusForm";
            Text = "BusForm";
            Load += BusForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewBus).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBusName;
        private System.Windows.Forms.TextBox txtAsalKota;
        private System.Windows.Forms.TextBox txtJumlahKursi;
        private System.Windows.Forms.TextBox txtKotaTujuan;
        private System.Windows.Forms.TextBox txtHargaTiket;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dataGridViewBus;
    }
}