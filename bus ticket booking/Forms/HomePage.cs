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
    public partial class HomePage : Form
    {
        public HomePage()
        {
            InitializeComponent();
        }
        private void OpenForm(Form newForm)
        {
            // Cek apakah form sudah terbuka
            foreach (Form openForm in this.MdiParent.MdiChildren)
            {
                if (openForm.GetType() == newForm.GetType())
                {
                    openForm.Activate(); // Jika sudah, bawa ke depan
                    return;
                }
            }
            //Jika belum terbuka, tampilkan sebagai child dari MDI container
            newForm.MdiParent = this.MdiParent;
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
            OpenForm(new ());
        }

        private void HomePage_Load(object sender, EventArgs e)
        {

        }
    }
}
