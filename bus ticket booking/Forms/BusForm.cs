using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using bus_ticket_booking.Data;
using bus_ticket_booking.Models;
using bus_ticket_booking.Services;

namespace bus_ticket_booking.Forms
{
    public partial class BusForm : Form
    {
        private readonly BusService _busService;
        private int selectedBusId = 0;

        public BusForm()
        {
            InitializeComponent();
            var context = new AppDbContext(); // langsung buat context di sini
            _busService = new BusService(context);

        }

        private async void BusForm_Load(object sender, EventArgs e)
        {
            await LoadBusDataAsync();

        }
        private async Task LoadBusDataAsync()
        {
            var buses = await _busService.GetAllAsync();
            dataGridViewBus.DataSource = buses.Select(b => new
            {
                b.BusId,
                b.BusName,
                b.FromCity,
                b.ToCity,
                b.TicketPrice,
                b.TotalSeats,
                b.AvailableSeats
            }).ToList();
        }
        private void ClearForm()
        {
            txtBusName.Text = "";
            txtAsalKota.Text = "";
            txtKotaTujuan.Text = "";
            txtHargaTiket.Text = "";
            txtJumlahKursi.Text = "";
            selectedBusId = 0;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dataGridViewBus.Rows[e.RowIndex];
                selectedBusId = Convert.ToInt32(row.Cells["BusId"].Value);
                txtBusName.Text = row.Cells["BusName"].Value.ToString();
                txtAsalKota.Text = row.Cells["FromCity"].Value.ToString();
                txtKotaTujuan.Text = row.Cells["ToCity"].Value.ToString();
                txtHargaTiket.Text = row.Cells["TicketPrice"].Value.ToString();
                txtJumlahKursi.Text = row.Cells["TotalSeats"].Value.ToString();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtBusName.Text) ||
                    string.IsNullOrWhiteSpace(txtAsalKota.Text) ||
                    string.IsNullOrWhiteSpace(txtKotaTujuan.Text) ||
                    string.IsNullOrWhiteSpace(txtHargaTiket.Text) ||
                    string.IsNullOrWhiteSpace(txtJumlahKursi.Text))
                {
                    MessageBox.Show("Harap isi semua field!");
                    return;
                }

                var bus = new Bus
                {
                    BusName = txtBusName.Text,
                    FromCity = txtAsalKota.Text,
                    ToCity = txtKotaTujuan.Text,
                    TicketPrice = decimal.Parse(txtHargaTiket.Text),
                    TotalSeats = int.Parse(txtJumlahKursi.Text),
                    AvailableSeats = int.Parse(txtJumlahKursi.Text)
                };

                await _busService.AddAsync(bus);
                MessageBox.Show("Bus berhasil ditambahkan!");
                await LoadBusDataAsync();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan: {ex.Message}");
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedBusId == 0)
            {
                MessageBox.Show("Pilih data bus terlebih dahulu!");
                return;
            }

            try
            {
                var bus = await _busService.GetByIdAsync(selectedBusId);
                if (bus == null)
                {
                    MessageBox.Show("Data bus tidak ditemukan.");
                    return;
                }

                bus.BusName = txtBusName.Text;
                bus.FromCity = txtAsalKota.Text;
                bus.ToCity = txtKotaTujuan.Text;
                bus.TicketPrice = decimal.Parse(txtHargaTiket.Text);
                bus.TotalSeats = int.Parse(txtJumlahKursi.Text);
                bus.AvailableSeats = int.Parse(txtJumlahKursi.Text);

                await _busService.UpdateAsync(bus);
                MessageBox.Show("Bus berhasil diperbarui!");
                await LoadBusDataAsync();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan: {ex.Message}");
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedBusId == 0)
            {
                MessageBox.Show("Pilih data bus yang ingin dihapus!");
                return;
            }

            var confirm = MessageBox.Show("Apakah yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                await _busService.DeleteAsync(selectedBusId);
                MessageBox.Show("Bus berhasil dihapus!");
                await LoadBusDataAsync();
                ClearForm();
            }
        }

        private void txtBusName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
