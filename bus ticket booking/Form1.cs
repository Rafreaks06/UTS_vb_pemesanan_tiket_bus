using System;
using System.Drawing;
using System.Windows.Forms;
using bus_ticket_booking.Forms;

namespace bus_ticket_booking
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
            this.WindowState = FormWindowState.Maximized;
            this.Text = "Sistem Booking Tiket Bus";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Buat instance dari HomePage
            HomePage homeForm = new HomePage();

            // Atur parent dari HomePage adalah Form1
            homeForm.MdiParent = this;

            // Hilangkan border agar terlihat menyatu
            homeForm.FormBorderStyle = FormBorderStyle.None;

            // Atur agar HomePage memenuhi seluruh area Form1
            homeForm.Dock = DockStyle.Fill;

            // Tampilkan HomePage
            homeForm.Show();
        }
    }
}