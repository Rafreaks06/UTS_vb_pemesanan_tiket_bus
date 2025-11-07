using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace bus_ticket_booking.Forms
{
    public partial class HomePage : Form
    {
        private Panel contentPanel;

        public HomePage()
        {
            InitializeComponent();
            InitializeContentPanel();
        }

        /// <summary>
        /// Inisialisasi panel untuk menampung form-form child
        /// </summary>
        private void InitializeContentPanel()
        {
            // Cari MenuStrip yang ada di designer
            MenuStrip existingMenu = this.Controls.OfType<MenuStrip>().FirstOrDefault();

            // Buat panel content untuk menampung form-form
            contentPanel = new Panel
            {
                BackColor = Color.WhiteSmoke,
                Name = "contentPanel",
                Visible = true,
                Location = new Point(0, existingMenu != null ? existingMenu.Height : 0),
                Size = new Size(this.ClientSize.Width, this.ClientSize.Height - (existingMenu != null ? existingMenu.Height : 0))
            };

            // Handle resize agar panel menyesuaikan ukuran
            this.Resize += (s, e) =>
            {
                if (existingMenu != null)
                {
                    contentPanel.Location = new Point(0, existingMenu.Height);
                    contentPanel.Size = new Size(this.ClientSize.Width, this.ClientSize.Height - existingMenu.Height);
                }
                else
                {
                    contentPanel.Size = this.ClientSize;
                }
            };

            // Tambahkan ke HomePage
            this.Controls.Add(contentPanel);

            // Pastikan MenuStrip tetap di atas
            if (existingMenu != null)
            {
                existingMenu.BringToFront();
            }

            // Kirim contentPanel ke belakang agar komponen lain tetap di depan
            contentPanel.SendToBack();

            // Tampilkan welcome screen sebagai default
            ShowWelcomeScreen();
        }

        /// <summary>
        /// Tampilkan welcome screen di content panel
        /// </summary>
        private void ShowWelcomeScreen()
        {
            // Hapus semua form yang ada di contentPanel
            foreach (Control ctrl in contentPanel.Controls.OfType<Form>().ToList())
            {
                ctrl.Dispose();
            }
            contentPanel.Controls.Clear();

            Label lblWelcome = new Label
            {
                Text = "Selamat Datang di Sistem Booking Tiket Bus",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                AutoSize = true,
                ForeColor = Color.FromArgb(51, 51, 51)
            };

            Label lblSubtitle = new Label
            {
                Text = "Silakan pilih menu di atas untuk memulai",
                Font = new Font("Segoe UI", 14),
                AutoSize = true,
                ForeColor = Color.Gray
            };

            contentPanel.Controls.Add(lblWelcome);
            contentPanel.Controls.Add(lblSubtitle);

            // Center labels
            CenterLabels(lblWelcome, lblSubtitle);
            contentPanel.Resize += (s, e) => CenterLabels(lblWelcome, lblSubtitle);
        }

        private void CenterLabels(Label lblWelcome, Label lblSubtitle)
        {
            lblWelcome.Left = (contentPanel.Width - lblWelcome.Width) / 2;
            lblWelcome.Top = (contentPanel.Height - lblWelcome.Height) / 2 - 30;

            lblSubtitle.Left = (contentPanel.Width - lblSubtitle.Width) / 2;
            lblSubtitle.Top = lblWelcome.Bottom + 20;
        }

        /// <summary>
        /// Load form ke dalam content panel (BUKAN sebagai MDI child)
        /// </summary>
        private void LoadFormToContentPanel(Form form)
        {
            // Dispose semua form lama
            foreach (Control ctrl in contentPanel.Controls.OfType<Form>().ToList())
            {
                Form oldForm = ctrl as Form;
                if (oldForm != null)
                {
                    oldForm.Close();
                    oldForm.Dispose();
                }
            }

            // Bersihkan content panel
            contentPanel.Controls.Clear();

            // Pastikan panel visible
            contentPanel.Visible = true;

            // Load form baru ke dalam panel
            form.TopLevel = false; // PENTING: Set TopLevel = false agar bisa jadi child control
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            form.Visible = true;
            contentPanel.Controls.Add(form);
            form.Show();
            form.BringToFront();
        }

        /// <summary>
        /// HAPUS method OpenForm() yang lama, ganti dengan yang baru
        /// </summary>
        private void menuPelanggan_Click(object sender, EventArgs e)
        {
            LoadFormToContentPanel(new PassengerForm());
        }

        private void menuBus_Click(object sender, EventArgs e)
        {
            LoadFormToContentPanel(new BusForm());
        }

        private void menuSale_Click(object sender, EventArgs e)
        {
            LoadFormToContentPanel(new SaleForm());
        }

        private void menuLaporan_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Fitur laporan belum tersedia.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void HomePage_Load(object sender, EventArgs e)
        {
            this.IsMdiContainer = false;

            // Pastikan layout sudah benar setelah load
            MenuStrip existingMenu = this.Controls.OfType<MenuStrip>().FirstOrDefault();
            if (existingMenu != null && contentPanel != null)
            {
                contentPanel.Location = new Point(0, existingMenu.Height);
                contentPanel.Size = new Size(this.ClientSize.Width, this.ClientSize.Height - existingMenu.Height);
                existingMenu.BringToFront();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // Tidak digunakan
        }
    }
}