using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using bus_ticket_booking.Data;
using bus_ticket_booking.Models;
using Microsoft.EntityFrameworkCore;

namespace bus_ticket_booking.Forms
{
    public partial class SaleForm : Form
    {
        private bool _isInitializing; // digunakan agar event comboBox tidak berjalan saat data masih di-load

        public SaleForm()
        {
            InitializeComponent();

            // --- Inisialisasi event tombol CRUD ---
            btnAdd.Click += BtnAdd_Click;       // tombol tambah data
            btnUpdate.Click += BtnUpdate_Click; // tombol update data
            btnDelete.Click += BtnDelete_Click; // tombol hapus data
            btnClear.Click += BtnClear_Click;   // tombol bersihkan form

            // --- Event comboBox dan DataGridView ---
            comboPassenger.SelectedIndexChanged += ComboPassenger_SelectedIndexChanged;
            comboBus.SelectedIndexChanged += ComboBus_SelectedIndexChanged;
            dataGridView1.CellClick += DataGridView1_CellClick;

            // --- Memulai load data dari database secara async ---
            _ = InitializeDataAsync();
        }

        // --- Load semua data awal (penumpang, bus, penjualan) ---
        private async Task InitializeDataAsync()
        {
            _isInitializing = true; // mencegah event combo terpicu saat loading
            await LoadPassengerList();
            await LoadBusList();
            await LoadSalesList();
            _isInitializing = false;
        }

        // --- Load daftar penumpang ke combo box ---
        private async Task LoadPassengerList()
        {
            using var context = new AppDbContext();
            var passengers = await context.Passengers.OrderBy(p => p.FullName).ToListAsync();
            comboPassenger.DataSource = passengers;
            comboPassenger.DisplayMember = "FullName";   // tampilkan nama penumpang
            comboPassenger.ValueMember = "PassengerId";  // ambil id penumpang
            comboPassenger.SelectedIndex = -1;           // belum ada yang dipilih
        }

        // --- Load daftar bus ke combo box ---
        private async Task LoadBusList()
        {
            using var context = new AppDbContext();
            var buses = await context.Buses.OrderBy(b => b.BusName).ToListAsync();
            comboBus.DataSource = buses;
            comboBus.DisplayMember = "BusName"; // tampilkan nama bus
            comboBus.ValueMember = "BusId";     // ambil id bus
            comboBus.SelectedIndex = -1;
        }

        // --- Load daftar penjualan ke DataGridView ---
        private async Task LoadSalesList()
        {
            using var context = new AppDbContext();
            var sales = await context.Sales
                .Include(s => s.Passenger)
                .Include(s => s.Bus)
                .Select(s => new
                {
                    s.SaleId,
                    Penumpang = s.Passenger.FullName,
                    Bus = s.Bus.BusName,
                    s.SaleDate,
                    s.Quantity,
                    s.TotalPrice
                })
                .ToListAsync();

            dataGridView1.DataSource = sales;
            if (dataGridView1.Columns.Contains("SaleId"))
                dataGridView1.Columns["SaleId"].Visible = false; // sembunyikan kolom ID dari tampilan
        }

        private void ComboPassenger_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isInitializing) return;
        }

        // --- Saat comboBus diubah, tampilkan harga tiket otomatis ---
        private async void ComboBus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isInitializing) return;
            if (comboBus.SelectedValue == null) return;
            if (!int.TryParse(comboBus.SelectedValue.ToString(), out int busId)) return;

            using var context = new AppDbContext();
            var bus = await context.Buses.FirstOrDefaultAsync(b => b.BusId == busId);

            if (bus != null)
                txtTotalPrice.Text = bus.TicketPrice.ToString("N0"); // tampilkan harga
            else
                txtTotalPrice.Clear();
        }

        // --- Bagian VALIDASI FORM sebelum menyimpan ---
        private bool ValidateForm()
        {
            // Validasi penumpang
            if (comboPassenger.SelectedIndex == -1 || comboPassenger.SelectedValue == null)
            {
                MessageBox.Show("Pilih penumpang terlebih dahulu.");
                return false;
            }

            // Validasi bus
            if (comboBus.SelectedIndex == -1 || comboBus.SelectedValue == null)
            {
                MessageBox.Show("Pilih bus terlebih dahulu.");
                return false;
            }

            // Validasi harga
            if (string.IsNullOrWhiteSpace(txtTotalPrice.Text))
            {
                MessageBox.Show("Harga tidak boleh kosong.");
                return false;
            }

            // Validasi nilai harga
            if (!decimal.TryParse(txtTotalPrice.Text.Replace(".", "").Replace(",", ""), out decimal price) || price <= 0)
            {
                MessageBox.Show("Harga harus berupa angka dan lebih dari 0.");
                return false;
            }

            // Validasi tanggal
            if (dtSaleDate.Value == DateTime.MinValue)
            {
                MessageBox.Show("Tanggal tidak valid.");
                return false;
            }

            return true; // semua valid
        }

        // --- TOMBOL TAMBAH DATA PENJUALAN ---
        private async void BtnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return; // pastikan data valid

            try
            {
                using var context = new AppDbContext();

                // Ambil nilai dari input
                int passengerId = (int)comboPassenger.SelectedValue;
                int busId = (int)comboBus.SelectedValue;
                decimal totalPrice = decimal.Parse(txtTotalPrice.Text.Replace(".", "").Replace(",", ""));

                // Pastikan data benar-benar ada di DB
                var passengerExists = await context.Passengers.AnyAsync(p => p.PassengerId == passengerId);
                var busExists = await context.Buses.AnyAsync(b => b.BusId == busId);

                if (!passengerExists || !busExists)
                {
                    MessageBox.Show("Data penumpang atau bus tidak valid.");
                    return;
                }

                // --- INPUT DATA BARU ---
                var sale = new Sale
                {
                    PassengerId = passengerId,
                    BusId = busId,
                    SaleDate = DateTime.SpecifyKind(dtSaleDate.Value, DateTimeKind.Utc),
                    Quantity = 1,
                    TotalPrice = totalPrice
                };

                context.Sales.Add(sale); // tambahkan ke database
                await context.SaveChangesAsync();

                await LoadSalesList(); // refresh tampilan tabel
                ClearForm();

                MessageBox.Show("Transaksi berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kesalahan: " + (ex.InnerException?.Message ?? ex.Message));
            }
        }

        // --- TOMBOL UPDATE DATA PENJUALAN ---
        private async void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Pilih data yang akan diubah.");
                return;
            }

            if (!ValidateForm()) return;

            if (!int.TryParse(dataGridView1.CurrentRow.Cells["SaleId"].Value?.ToString(), out int saleId) || saleId <= 0)
            {
                MessageBox.Show("ID penjualan tidak valid.");
                return;
            }

            try
            {
                using var context = new AppDbContext();
                var sale = await context.Sales.FindAsync(saleId);
                if (sale == null)
                {
                    MessageBox.Show("Data tidak ditemukan.");
                    return;
                }

                // --- EDIT DATA ---
                int passengerId = (int)comboPassenger.SelectedValue;
                int busId = (int)comboBus.SelectedValue;
                decimal totalPrice = decimal.Parse(txtTotalPrice.Text.Replace(".", "").Replace(",", ""));

                sale.PassengerId = passengerId;
                sale.BusId = busId;
                sale.SaleDate = DateTime.SpecifyKind(dtSaleDate.Value, DateTimeKind.Utc);
                sale.TotalPrice = totalPrice;

                context.Sales.Update(sale);
                await context.SaveChangesAsync();

                await LoadSalesList();
                await LoadPassengerList();
                await LoadBusList();

                ClearForm();
                MessageBox.Show("Data berhasil diperbarui!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kesalahan: " + (ex.InnerException?.Message ?? ex.Message));
            }
        }

        // --- TOMBOL HAPUS DATA PENJUALAN ---
        private async void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Pilih data yang akan dihapus.");
                return;
            }

            if (!int.TryParse(dataGridView1.CurrentRow.Cells["SaleId"].Value?.ToString(), out int saleId) || saleId <= 0)
            {
                MessageBox.Show("ID penjualan tidak valid.");
                return;
            }

            // Konfirmasi penghapusan
            if (MessageBox.Show("Yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            try
            {
                using var context = new AppDbContext();
                var sale = await context.Sales.FindAsync(saleId);
                if (sale == null)
                {
                    MessageBox.Show("Data tidak ditemukan.");
                    return;
                }

                context.Sales.Remove(sale); // Hapus data
                await context.SaveChangesAsync();

                await LoadSalesList();
                ClearForm();

                MessageBox.Show("Data berhasil dihapus.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kesalahan: " + (ex.InnerException?.Message ?? ex.Message));
            }
        }

        // --- Tombol CLEAR FORM ---
        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        // --- Membersihkan seluruh input ---
        private void ClearForm()
        {
            comboPassenger.SelectedIndex = -1;
            comboBus.SelectedIndex = -1;
            txtTotalPrice.Clear();
            dtSaleDate.Value = DateTime.Now;
        }

        // --- Saat user klik baris di DataGridView, isi form otomatis ---
        private async void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dataGridView1.Rows.Count == 0) return;

            var row = dataGridView1.Rows[e.RowIndex];
            if (row == null || row.Cells["SaleId"]?.Value == null) return;

            if (!int.TryParse(row.Cells["SaleId"].Value.ToString(), out int saleId) || saleId <= 0)
                return;

            using var context = new AppDbContext();
            var sale = await context.Sales
                .Include(s => s.Passenger)
                .Include(s => s.Bus)
                .FirstOrDefaultAsync(s => s.SaleId == saleId);

            if (sale == null) return;

            _isInitializing = true; // supaya comboBox tidak trigger event saat isi ulang

            // pastikan list sudah terisi
            if (comboPassenger.DataSource == null || comboBus.DataSource == null)
            {
                await LoadPassengerList();
                await LoadBusList();
            }

            // --- ISI ULANG DATA KE FORM ---
            comboPassenger.SelectedValue = sale.PassengerId;
            comboBus.SelectedValue = sale.BusId;
            txtTotalPrice.Text = sale.TotalPrice.ToString("N0");
            dtSaleDate.Value = sale.SaleDate == DateTime.MinValue ? DateTime.Now : sale.SaleDate.ToLocalTime();

            _isInitializing = false;
        }
    }
}
