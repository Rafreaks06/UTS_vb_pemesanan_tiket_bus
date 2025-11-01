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
        private bool _isInitializing;

        public SaleForm()
        {
            InitializeComponent();

            btnAdd.Click += BtnAdd_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDelete.Click += BtnDelete_Click;
            btnClear.Click += BtnClear_Click;

            comboPassenger.SelectedIndexChanged += ComboPassenger_SelectedIndexChanged;
            comboBus.SelectedIndexChanged += ComboBus_SelectedIndexChanged;

            dataGridView1.CellClick += DataGridView1_CellClick;

            _ = InitializeDataAsync();
        }

        private async Task InitializeDataAsync()
        {
            _isInitializing = true;
            await LoadPassengerList();
            await LoadBusList();
            await LoadSalesList();
            _isInitializing = false;
        }

        private async Task LoadPassengerList()
        {
            using var context = new AppDbContext();
            var passengers = await context.Passengers.OrderBy(p => p.FullName).ToListAsync();
            comboPassenger.DataSource = passengers;
            comboPassenger.DisplayMember = "FullName";
            comboPassenger.ValueMember = "PassengerId";
            comboPassenger.SelectedIndex = -1;
        }

        private async Task LoadBusList()
        {
            using var context = new AppDbContext();
            var buses = await context.Buses.OrderBy(b => b.BusName).ToListAsync();
            comboBus.DataSource = buses;
            comboBus.DisplayMember = "BusName";
            comboBus.ValueMember = "BusId";
            comboBus.SelectedIndex = -1;
        }

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
                dataGridView1.Columns["SaleId"].Visible = false;
        }

        private void ComboPassenger_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isInitializing) return;
        }

        private async void ComboBus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isInitializing) return;
            if (comboBus.SelectedValue == null) return;

            int busId = (int)comboBus.SelectedValue;

            using var context = new AppDbContext();
            var bus = await context.Buses.FirstOrDefaultAsync(b => b.BusId == busId);

            if (bus != null)
                txtTotalPrice.Text = bus.TicketPrice.ToString("N0");
        }

        private bool ValidateForm()
        {
            if (comboPassenger.SelectedIndex == -1)
            {
                MessageBox.Show("Pilih penumpang terlebih dahulu.");
                return false;
            }

            if (comboBus.SelectedIndex == -1)
            {
                MessageBox.Show("Pilih bus terlebih dahulu.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTotalPrice.Text))
            {
                MessageBox.Show("Harga tidak boleh kosong.");
                return false;
            }

            if (!decimal.TryParse(txtTotalPrice.Text, out _))
            {
                MessageBox.Show("Harga harus berupa angka.");
                return false;
            }

            return true;
        }

        private async void BtnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            try
            {
                using var context = new AppDbContext();
                var sale = new Sale
                {
                    PassengerId = (int)comboPassenger.SelectedValue,
                    BusId = (int)comboBus.SelectedValue,
                    SaleDate = DateTime.SpecifyKind(dtSaleDate.Value, DateTimeKind.Utc),
                    Quantity = 1,
                    TotalPrice = decimal.Parse(txtTotalPrice.Text)
                };

                context.Sales.Add(sale);
                await context.SaveChangesAsync();

                await LoadSalesList();
                ClearForm();

                MessageBox.Show("Transaksi berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Pilih data yang akan diubah.");
                return;
            }

            if (!ValidateForm()) return;

            if (!int.TryParse(dataGridView1.CurrentRow.Cells["SaleId"].Value?.ToString(), out int saleId))
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
                    MessageBox.Show("Data tidak ditemukan di database.");
                    return;
                }

                if (comboPassenger.SelectedValue == null || comboBus.SelectedValue == null)
                {
                    MessageBox.Show("Pastikan penumpang dan bus dipilih.");
                    return;
                }

                sale.PassengerId = (int)comboPassenger.SelectedValue;
                sale.BusId = (int)comboBus.SelectedValue;
                sale.SaleDate = DateTime.SpecifyKind(dtSaleDate.Value, DateTimeKind.Utc);
                sale.TotalPrice = decimal.Parse(txtTotalPrice.Text);

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
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Pilih data yang akan dihapus.");
                return;
            }

            int saleId = (int)dataGridView1.CurrentRow.Cells["SaleId"].Value;

            if (MessageBox.Show("Yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            try
            {
                using var context = new AppDbContext();
                var sale = await context.Sales.FindAsync(saleId);
                if (sale == null) return;

                context.Sales.Remove(sale);
                await context.SaveChangesAsync();

                await LoadSalesList();
                ClearForm();

                MessageBox.Show("Data berhasil dihapus.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            comboPassenger.SelectedIndex = -1;
            comboBus.SelectedIndex = -1;
            txtTotalPrice.Clear();
            dtSaleDate.Value = DateTime.Now;
        }

        private async void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dataGridView1.Rows[e.RowIndex];
            if (row == null) return;

            if (!int.TryParse(row.Cells["SaleId"].Value?.ToString(), out int saleId))
                return;

            using var context = new AppDbContext();
            var sale = await context.Sales
                .Include(s => s.Passenger)
                .Include(s => s.Bus)
                .FirstOrDefaultAsync(s => s.SaleId == saleId);

            if (sale == null) return;

            _isInitializing = true;

            if (comboPassenger.DataSource == null || comboBus.DataSource == null)
            {
                await LoadPassengerList();
                await LoadBusList();
            }

            var passengers = (comboPassenger.DataSource as System.Collections.IEnumerable)?.Cast<Passenger>().ToList();
            var buses = (comboBus.DataSource as System.Collections.IEnumerable)?.Cast<Bus>().ToList();

            if (passengers != null && passengers.Any(p => p.PassengerId == sale.PassengerId))
                comboPassenger.SelectedValue = sale.PassengerId;
            else
                comboPassenger.SelectedIndex = -1;

            if (buses != null && buses.Any(b => b.BusId == sale.BusId))
                comboBus.SelectedValue = sale.BusId;
            else
                comboBus.SelectedIndex = -1;

            txtTotalPrice.Text = sale.TotalPrice.ToString("N0");
            dtSaleDate.Value = sale.SaleDate.ToLocalTime();

            _isInitializing = false;
        }
    }
}
