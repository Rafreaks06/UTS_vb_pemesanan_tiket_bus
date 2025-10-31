using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            // 2. Atur agar jendela utama terbuka maksimal
            this.WindowState = FormWindowState.Maximized;
            this.Text = "Sistem Booking Tiket Bus";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 3. Buat instance dari HomePage
            HomePage homeForm = new HomePage();
            // 4. Atur parent dari HomePage adalah Form1
            homeForm.MdiParent = this;
            // 5. Hilangkan border agar terlihat menyatu
            homeForm.FormBorderStyle = FormBorderStyle.None;
            // 6. Atur agar HomePage memenuhi seluruh area Form1
            homeForm.Dock = DockStyle.Fill;
            // 7. Tampilkan HomePage
            homeForm.Show();
        }
    }
}
