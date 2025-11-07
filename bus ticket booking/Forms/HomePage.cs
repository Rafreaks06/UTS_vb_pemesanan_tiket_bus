using System;
using System.Linq;
using System.Windows.Forms;

namespace bus_ticket_booking.Forms
{
    public partial class HomePage : Form
    {
        public HomePage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Membuka form baru dalam MDI parent. 
        /// Jika form sudah terbuka, maka form tersebut akan diaktifkan.
        /// </summary>
        private void OpenForm(Form newForm)
        {
            // Cek apakah form sudah terbuka
            foreach (Form openForm in this.MdiParent.MdiChildren)
            {
                if (openForm.GetType() == newForm.GetType())
                {
                    openForm.Activate(); // Jika sudah terbuka, tampilkan di depan
                    return;
                }
            }

            // Jika belum terbuka, tampilkan sebagai child dari MDI container
            newForm.MdiParent = this.MdiParent;
            newForm.WindowState = FormWindowState.Maximized; // agar form memenuhi layar MDI
            newForm.Show();
        }

        private void menuPelanggan_Click(object sender, EventArgs e)
        {
            OpenForm(new PassengerForm());
        }

        private void menuBus_Click(object sender, EventArgs e)
        {
            OpenForm(new BusForm());
        }

        private void menuSale_Click(object sender, EventArgs e)
        {
            OpenForm(new SaleForm());
        }

        private void menuLaporan_Click(object sender, EventArgs e)
        {
            // Nanti bisa diganti dengan form laporan yang sudah kamu buat
            // Contoh: OpenForm(new ReportForm());
            MessageBox.Show("Fitur laporan belum tersedia.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void HomePage_Load(object sender, EventArgs e)
        {
            // Tambahkan inisialisasi jika dibutuhkan
            this.IsMdiContainer = false; // HomePage bukan MDI container, tapi child di dalam MainForm
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // Tidak digunakan, bisa dihapus jika tidak perlu
        }
    }
}
