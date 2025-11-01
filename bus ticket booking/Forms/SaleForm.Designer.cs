namespace bus_ticket_booking.Forms
{
    partial class SaleForm
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
            comboPassenger = new System.Windows.Forms.ComboBox();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            comboBus = new System.Windows.Forms.ComboBox();
            txtTotalPrice = new System.Windows.Forms.TextBox();
            dtSaleDate = new System.Windows.Forms.DateTimePicker();
            btnAdd = new System.Windows.Forms.Button();
            btnUpdate = new System.Windows.Forms.Button();
            btnDelete = new System.Windows.Forms.Button();
            btnClear = new System.Windows.Forms.Button();
            label6 = new System.Windows.Forms.Label();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // comboPassenger
            // 
            comboPassenger.FormattingEnabled = true;
            comboPassenger.Location = new System.Drawing.Point(100, 50);
            comboPassenger.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboPassenger.Name = "comboPassenger";
            comboPassenger.Size = new System.Drawing.Size(206, 23);
            comboPassenger.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(10, 50);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(72, 15);
            label2.TabIndex = 2;
            label2.Text = "Penumpang";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(10, 100);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(26, 15);
            label3.TabIndex = 3;
            label3.Text = "Bus";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(10, 150);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(49, 15);
            label4.TabIndex = 4;
            label4.Text = "Tanggal";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(10, 200);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(68, 15);
            label5.TabIndex = 5;
            label5.Text = "Total Harga";
            // 
            // comboBus
            // 
            comboBus.FormattingEnabled = true;
            comboBus.Location = new System.Drawing.Point(100, 100);
            comboBus.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBus.Name = "comboBus";
            comboBus.Size = new System.Drawing.Size(206, 23);
            comboBus.TabIndex = 6;
            // 
            // txtTotalPrice
            // 
            txtTotalPrice.Location = new System.Drawing.Point(100, 200);
            txtTotalPrice.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtTotalPrice.Name = "txtTotalPrice";
            txtTotalPrice.ReadOnly = true;
            txtTotalPrice.Size = new System.Drawing.Size(206, 23);
            txtTotalPrice.TabIndex = 7;
            // 
            // dtSaleDate
            // 
            dtSaleDate.Location = new System.Drawing.Point(100, 150);
            dtSaleDate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            dtSaleDate.Name = "dtSaleDate";
            dtSaleDate.Size = new System.Drawing.Size(206, 23);
            dtSaleDate.TabIndex = 8;
            // 
            // btnAdd
            // 
            btnAdd.Location = new System.Drawing.Point(10, 250);
            btnAdd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new System.Drawing.Size(286, 27);
            btnAdd.TabIndex = 9;
            btnAdd.Text = "Pesan Tiket";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new System.Drawing.Point(10, 300);
            btnUpdate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new System.Drawing.Size(286, 27);
            btnUpdate.TabIndex = 10;
            btnUpdate.Text = "Update Data";
            btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Location = new System.Drawing.Point(10, 350);
            btnDelete.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new System.Drawing.Size(286, 27);
            btnDelete.TabIndex = 11;
            btnDelete.Text = "Hapus Data";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            btnClear.Location = new System.Drawing.Point(10, 400);
            btnClear.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnClear.Name = "btnClear";
            btnClear.Size = new System.Drawing.Size(286, 27);
            btnClear.TabIndex = 12;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label6.Location = new System.Drawing.Point(475, 10);
            label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(245, 25);
            label6.TabIndex = 13;
            label6.Text = "Daftar Transaksi Tiket";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new System.Drawing.Point(325, 50);
            dataGridView1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new System.Drawing.Size(575, 375);
            dataGridView1.TabIndex = 14;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label1.Location = new System.Drawing.Point(10, 10);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(286, 25);
            label1.TabIndex = 0;
            label1.Text = "Ticket / Sale Management";
            // 
            // SaleForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(909, 446);
            Controls.Add(dataGridView1);
            Controls.Add(label6);
            Controls.Add(btnClear);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnAdd);
            Controls.Add(dtSaleDate);
            Controls.Add(txtTotalPrice);
            Controls.Add(comboBus);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(comboPassenger);
            Controls.Add(label1);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "SaleForm";
            Text = "SaleForm";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboPassenger;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBus;
        private System.Windows.Forms.TextBox txtTotalPrice;
        private System.Windows.Forms.DateTimePicker dtSaleDate;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
    }
}