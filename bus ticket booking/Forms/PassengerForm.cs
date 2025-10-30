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
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dataGridViewPassenger.Rows[e.RowIndex];
                selectedPassengerId = Convert.ToInt32(row.Cells["PassengerId"].Value);
                txtName.Text = row.Cells["FullName"].Value.ToString();
                txtPhone.Text = row.Cells["PhoneNumber"].Value?.ToString();
                txtAlamat.Text = row.Cells["Email"].Value?.ToString();
            }

        }

        private async void PassengerForm_Load(object sender, EventArgs e)
        {
            await LoadPassengerDataAsync();
        }
        private async Task LoadPassengerDataAsync()
        {
            var passengers = await _context.Passengers.ToListAsync();
            dataGridViewPassenger.DataSource = passengers.Select(p => new
            {
                p.PassengerId,
                p.FullName,
                p.PhoneNumber,
                p.Email
            }).ToList();
        }
        private void ClearForm()
        {
            txtName.Text = "";
            txtPhone.Text = "";
            txtAlamat.Text = ""; // diasumsikan txtAlamat = Email
            selectedPassengerId = 0;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Nama penumpang harus diisi!");
                    return;
                }

                var passenger = new Passenger
                {
                    FullName = txtName.Text,
                    PhoneNumber = txtPhone.Text,
                    Email = txtAlamat.Text
                };

                _context.Passengers.Add(passenger);
                await _context.SaveChangesAsync();

                MessageBox.Show("Penumpang berhasil ditambahkan!");
                await LoadPassengerDataAsync();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan: {ex.Message}");
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedPassengerId == 0)
            {
                MessageBox.Show("Pilih data penumpang terlebih dahulu!");
                return;
            }

            try
            {
                var passenger = await _context.Passengers.FindAsync(selectedPassengerId);
                if (passenger == null)
                {
                    MessageBox.Show("Data penumpang tidak ditemukan.");
                    return;
                }

                passenger.FullName = txtName.Text;
                passenger.PhoneNumber = txtPhone.Text;
                passenger.Email = txtAlamat.Text;

                _context.Passengers.Update(passenger);
                await _context.SaveChangesAsync();

                MessageBox.Show("Data penumpang berhasil diperbarui!");
                await LoadPassengerDataAsync();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan: {ex.Message}");
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedPassengerId == 0)
            {
                MessageBox.Show("Pilih data penumpang yang ingin dihapus!");
                return;
            }

            var confirm = MessageBox.Show("Apakah yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    var passenger = await _context.Passengers.FindAsync(selectedPassengerId);
                    if (passenger != null)
                    {
                        _context.Passengers.Remove(passenger);
                        await _context.SaveChangesAsync();

                        MessageBox.Show("Data penumpang berhasil dihapus!");
                        await LoadPassengerDataAsync();
                        ClearForm();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Terjadi kesalahan: {ex.Message}");
                }
            }
        }

    }


}
