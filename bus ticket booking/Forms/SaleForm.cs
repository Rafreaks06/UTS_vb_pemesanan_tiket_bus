using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bus_ticket_booking.Forms
{
    public partial class SaleForm : Form
    {
        public SaleForm()
        {
            InitializeComponent();

            btnAdd.Click += BtnAdd_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDelete.Click += BtnDelete_Click;
            btnClear.Click += BtnClear_Click;
            dataGridView1.CellClick += DataGridView1_CellClick;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            MessageBox.Show(
                $"Pesanan berhasil dibuat!\n\n" +
                $"Penumpang: {comboPassenger.Text}\n" +
                $"Bus: {comboBus.Text}\n" +
                $"Harga: Rp {txtTotalPrice.Text}\n" +
                $"Tanggal: {dtSaleDate.Value.ToShortDateString()}",
                "Pesanan Baru",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            ClearForm();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Pilih data yang ingin diupdate terlebih dahulu.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!ValidateForm())
                return;

            MessageBox.Show("Data tiket berhasil diperbarui (simulasi).", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Pilih data yang ingin dihapus.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show("Yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                MessageBox.Show("Data berhasil dihapus (simulasi).", "Hapus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
            MessageBox.Show("Form dikosongkan.", "Clear", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dataGridView1.Rows[e.RowIndex];

            comboPassenger.Text = row.Cells["Penumpang"].Value?.ToString();
            comboBus.Text = row.Cells["Bus"].Value?.ToString();
            txtTotalPrice.Text = row.Cells["Harga"].Value?.ToString();

            if (DateTime.TryParse(row.Cells["Tanggal"].Value?.ToString(), out DateTime tgl))
            {
                dtSaleDate.Value = tgl;
            }
        }

        private bool ValidateForm()
        {
            if (comboPassenger.SelectedIndex == -1)
            {
                MessageBox.Show("Silakan pilih penumpang.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboPassenger.Focus();
                return false;
            }

            if (comboBus.SelectedIndex == -1)
            {
                MessageBox.Show("Silakan pilih bus.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBus.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTotalPrice.Text))
            {
                MessageBox.Show("Harga total tidak boleh kosong.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTotalPrice.Focus();
                return false;
            }

            if (!decimal.TryParse(txtTotalPrice.Text, out decimal harga) || harga <= 0)
            {
                MessageBox.Show("Harga harus berupa angka lebih dari 0.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTotalPrice.Focus();
                return false;
            }

            if (dtSaleDate.Value.Date < DateTime.Now.Date)
            {
                MessageBox.Show("Tanggal penjualan tidak boleh sebelum hari ini.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtSaleDate.Focus();
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            comboPassenger.SelectedIndex = -1;
            comboBus.SelectedIndex = -1;
            txtTotalPrice.Text = "";
            dtSaleDate.Value = DateTime.Now;
        }
    }
}