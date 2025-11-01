using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using bus_ticket_booking.Data;
using bus_ticket_booking.Models;
using Microsoft.EntityFrameworkCore;

namespace bus_ticket_booking.Forms
{
    public partial class PassengerForm : Form
    {
        private readonly AppDbContext _context;
        private int selectedPassengerId = 0;

        public PassengerForm()
        {
            InitializeComponent();
            _context = new AppDbContext();
            dataGridViewPassenger.CellClick += DataGridViewPassenger_CellClick;
        }

        private async void PassengerForm_Load(object sender, EventArgs e)
        {
            await LoadPassengerDataAsync();
        }

        private async Task LoadPassengerDataAsync()
        {
            var passengers = await _context.Passengers
                .OrderBy(p => p.FullName)
                .Select(p => new
                {
                    p.PassengerId,
                    p.FullName,
                    p.PhoneNumber,
                    p.Email
                })
                .ToListAsync();

            dataGridViewPassenger.DataSource = passengers;
        }

        private void DataGridViewPassenger_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dataGridViewPassenger.Rows[e.RowIndex];
            selectedPassengerId = Convert.ToInt32(row.Cells["PassengerId"].Value);
            txtName.Text = row.Cells["FullName"].Value?.ToString();
            txtPhone.Text = row.Cells["PhoneNumber"].Value?.ToString();
            txtAlamat.Text = row.Cells["Email"].Value?.ToString();
        }

        private void ClearForm()
        {
            txtName.Clear();
            txtPhone.Clear();
            txtAlamat.Clear();
            selectedPassengerId = 0;
            dataGridViewPassenger.ClearSelection();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Nama penumpang wajib diisi!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            if (txtName.Text.Length < 3)
            {
                MessageBox.Show("Nama minimal 3 huruf.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Nomor telepon wajib diisi!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return false;
            }

            if (!Regex.IsMatch(txtPhone.Text, @"^[0-9]{9,15}$"))
            {
                MessageBox.Show("Nomor telepon harus berupa angka (9-15 digit).", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAlamat.Text))
            {
                MessageBox.Show("Email wajib diisi!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAlamat.Focus();
                return false;
            }

            if (!Regex.IsMatch(txtAlamat.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Format email tidak valid!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAlamat.Focus();
                return false;
            }

            return true;
        }

        private async Task<bool> IsDuplicateAsync(string name, string email, int excludeId = 0)
        {
            var lowerName = name.Trim().ToLower();
            var lowerEmail = email.Trim().ToLower();

            return await _context.Passengers.AnyAsync(p =>
                p.PassengerId != excludeId &&
                (p.FullName.ToLower() == lowerName || p.Email.ToLower() == lowerEmail));
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput()) return;

                if (await IsDuplicateAsync(txtName.Text, txtAlamat.Text))
                {
                    MessageBox.Show("Nama atau email sudah digunakan penumpang lain.", "Duplikasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var passenger = new Passenger
                {
                    FullName = txtName.Text.Trim(),
                    PhoneNumber = txtPhone.Text.Trim(),
                    Email = txtAlamat.Text.Trim()
                };

                _context.Passengers.Add(passenger);
                await _context.SaveChangesAsync();

                MessageBox.Show("Penumpang berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadPassengerDataAsync();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedPassengerId == 0)
            {
                MessageBox.Show("Pilih data penumpang terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (!ValidateInput()) return;

                if (await IsDuplicateAsync(txtName.Text, txtAlamat.Text, selectedPassengerId))
                {
                    MessageBox.Show("Nama atau email sudah digunakan penumpang lain.", "Duplikasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var passenger = await _context.Passengers.FindAsync(selectedPassengerId);
                if (passenger == null)
                {
                    MessageBox.Show("Data penumpang tidak ditemukan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                passenger.FullName = txtName.Text.Trim();
                passenger.PhoneNumber = txtPhone.Text.Trim();
                passenger.Email = txtAlamat.Text.Trim();

                _context.Passengers.Update(passenger);
                await _context.SaveChangesAsync();

                MessageBox.Show("Data penumpang berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadPassengerDataAsync();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedPassengerId == 0)
            {
                MessageBox.Show("Pilih data penumpang yang ingin dihapus.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("Apakah yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                var passenger = await _context.Passengers.FindAsync(selectedPassengerId);
                if (passenger != null)
                {
                    _context.Passengers.Remove(passenger);
                    await _context.SaveChangesAsync();

                    MessageBox.Show("Data penumpang berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadPassengerDataAsync();
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
