using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using bus_ticket_booking.Data;
using bus_ticket_booking.Models;
using bus_ticket_booking.Services;

namespace bus_ticket_booking.Forms
{
    public partial class BusForm : Form
    {
        // Service untuk mengelola data Bus
        private readonly BusService _busService;

        // Menyimpan ID bus yang sedang dipilih di DataGridView
        private int selectedBusId = 0;

        public BusForm()
        {
            InitializeComponent();

            // Membuat instance context dan service bus
            var context = new AppDbContext();
            _busService = new BusService(context);

            // Event untuk menangani klik pada tabel bus
            dataGridViewBus.CellClick += DataGridViewBus_CellClick;
        }

        // Ketika form pertama kali dimuat, data bus diambil dari database
        private async void BusForm_Load(object sender, EventArgs e)
        {
            await LoadBusDataAsync();
        }

        // Fungsi untuk memuat semua data bus ke tabel (DataGridView)
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

        // Saat baris di tabel diklik, data bus ditampilkan ke textbox
        private void DataGridViewBus_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dataGridViewBus.Rows[e.RowIndex];
            selectedBusId = Convert.ToInt32(row.Cells["BusId"].Value);
            txtBusName.Text = row.Cells["BusName"].Value?.ToString();
            txtAsalKota.Text = row.Cells["FromCity"].Value?.ToString();
            txtKotaTujuan.Text = row.Cells["ToCity"].Value?.ToString();
            txtHargaTiket.Text = row.Cells["TicketPrice"].Value?.ToString();
            txtJumlahKursi.Text = row.Cells["TotalSeats"].Value?.ToString();
        }

        // Menghapus isi form dan reset pilihan
        private void ClearForm()
        {
            txtBusName.Clear();
            txtAsalKota.Clear();
            txtKotaTujuan.Clear();
            txtHargaTiket.Clear();
            txtJumlahKursi.Clear();
            selectedBusId = 0;
            dataGridViewBus.ClearSelection();
        }

        // Tombol "Clear" diklik → hapus semua input
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        // Validasi data input sebelum disimpan
        private bool ValidateInput()
        {
            // Nama bus tidak boleh kosong
            if (string.IsNullOrWhiteSpace(txtBusName.Text))
            {
                MessageBox.Show("Nama bus wajib diisi!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBusName.Focus();
                return false;
            }

            // Minimal 3 karakter
            if (txtBusName.Text.Length < 3)
            {
                MessageBox.Show("Nama bus minimal 3 huruf.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBusName.Focus();
                return false;
            }

            // Kota asal wajib diisi
            if (string.IsNullOrWhiteSpace(txtAsalKota.Text))
            {
                MessageBox.Show("Kota asal wajib diisi!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAsalKota.Focus();
                return false;
            }

            // Kota tujuan wajib diisi
            if (string.IsNullOrWhiteSpace(txtKotaTujuan.Text))
            {
                MessageBox.Show("Kota tujuan wajib diisi!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtKotaTujuan.Focus();
                return false;
            }

            // Kota asal dan tujuan tidak boleh sama
            if (txtAsalKota.Text.Trim().Equals(txtKotaTujuan.Text.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Kota asal dan kota tujuan tidak boleh sama.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtKotaTujuan.Focus();
                return false;
            }

            // Harga tiket wajib diisi dan harus angka positif
            if (string.IsNullOrWhiteSpace(txtHargaTiket.Text))
            {
                MessageBox.Show("Harga tiket wajib diisi!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHargaTiket.Focus();
                return false;
            }

            if (!decimal.TryParse(txtHargaTiket.Text, out decimal harga) || harga <= 0)
            {
                MessageBox.Show("Harga tiket harus berupa angka positif.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHargaTiket.Focus();
                return false;
            }

            // Jumlah kursi wajib diisi dan minimal 5
            if (string.IsNullOrWhiteSpace(txtJumlahKursi.Text))
            {
                MessageBox.Show("Jumlah kursi wajib diisi!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtJumlahKursi.Focus();
                return false;
            }

            if (!int.TryParse(txtJumlahKursi.Text, out int kursi) || kursi < 5)
            {
                MessageBox.Show("Jumlah kursi harus berupa angka minimal 5.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtJumlahKursi.Focus();
                return false;
            }

            return true;
        }

        // Mengecek apakah bus dengan nama dan rute yang sama sudah ada
        private async Task<bool> IsDuplicateBusAsync(string name, string fromCity, string toCity, int excludeId = 0)
        {
            var allBuses = await _busService.GetAllAsync();
            return allBuses.Any(b =>
                b.BusId != excludeId &&
                b.BusName.Trim().ToLower() == name.Trim().ToLower() &&
                b.FromCity.Trim().ToLower() == fromCity.Trim().ToLower() &&
                b.ToCity.Trim().ToLower() == toCity.Trim().ToLower());
        }

        // Tombol "Tambah" diklik → tambah data bus baru
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput()) return;

                // Cek duplikasi bus
                if (await IsDuplicateBusAsync(txtBusName.Text, txtAsalKota.Text, txtKotaTujuan.Text))
                {
                    MessageBox.Show("Bus dengan nama dan rute tersebut sudah ada.", "Duplikasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Buat objek bus baru
                var bus = new Bus
                {
                    BusName = txtBusName.Text.Trim(),
                    FromCity = txtAsalKota.Text.Trim(),
                    ToCity = txtKotaTujuan.Text.Trim(),
                    TicketPrice = decimal.Parse(txtHargaTiket.Text),
                    TotalSeats = int.Parse(txtJumlahKursi.Text),
                    AvailableSeats = int.Parse(txtJumlahKursi.Text)
                };

                // Simpan ke database
                await _busService.AddAsync(bus);

                MessageBox.Show("Bus berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadBusDataAsync();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Tombol "Update" diklik → ubah data bus yang dipilih
        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedBusId == 0)
            {
                MessageBox.Show("Pilih data bus terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (!ValidateInput()) return;

                // Cek duplikasi (kecuali bus yang sedang diedit)
                if (await IsDuplicateBusAsync(txtBusName.Text, txtAsalKota.Text, txtKotaTujuan.Text, selectedBusId))
                {
                    MessageBox.Show("Bus dengan nama dan rute tersebut sudah ada.", "Duplikasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Ambil data bus dari database
                var bus = await _busService.GetByIdAsync(selectedBusId);
                if (bus == null)
                {
                    MessageBox.Show("Data bus tidak ditemukan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Perbarui data bus
                bus.BusName = txtBusName.Text.Trim();
                bus.FromCity = txtAsalKota.Text.Trim();
                bus.ToCity = txtKotaTujuan.Text.Trim();
                bus.TicketPrice = decimal.Parse(txtHargaTiket.Text);
                bus.TotalSeats = int.Parse(txtJumlahKursi.Text);
                bus.AvailableSeats = int.Parse(txtJumlahKursi.Text);

                await _busService.UpdateAsync(bus);

                MessageBox.Show("Data bus berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadBusDataAsync();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Tombol "Hapus" diklik → hapus data bus yang dipilih
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedBusId == 0)
            {
                MessageBox.Show("Pilih data bus yang ingin dihapus!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Konfirmasi sebelum hapus
            var confirm = MessageBox.Show("Apakah yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                await _busService.DeleteAsync(selectedBusId);
                MessageBox.Show("Data bus berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadBusDataAsync();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
