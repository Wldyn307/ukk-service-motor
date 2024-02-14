using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Drawing.Printing;


namespace WindowsFormsApp2
{
    public partial class kasir : Form
    {
        string nama = "";
        string id = "";
        int i = 0;
        public static int IdTr = 0;


        MySqlConnection conn = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=gatolin");
        Data p = new Data();

        public kasir()
        {
            InitializeComponent();
        }
        private void Logout()
        {
            p.command("insert into log (id_user , activity, created_at) VALUES ('" + Data.id_user + "', 'Logout' , NOW())");
            this.Hide();
            LOGIN loginForm = new LOGIN();
            loginForm.Show();
        }

        void addDb()
        {
            try
            {
                conn.Open();
                foreach (DataGridViewRow dr in dgtransaksi.Rows)
                {
                    if (dr.Cells[0].Value != null || dr.Cells[1].Value != null || dr.Cells[2].Value != null || dr.Cells[3].Value != null || dr.Cells[4].Value != null || dr.Cells[5].Value != null || dr.Cells[6].Value != null)
                    {
                        string kode_produk = dr.Cells[3].Value.ToString();
                        string nama_pelanggan = dr.Cells[2].Value.ToString();
                        string nomorUnik = dr.Cells[1].Value.ToString();
                        int uang_bayar = Convert.ToInt32(txtuangb.Text);
                        int uang_kembali = Convert.ToInt32(txtkembali.Text.Replace("Rp.", ""));
                        int totalhargab = Convert.ToInt32(lblTotal.Text);

                        // Temukan posisi tanda '-' dalam string
                        int index = kode_produk.IndexOf('-');

                        // Jika tanda '-' ditemukan, ambil semua karakter sebelumnya
                        string kodeProduk = index != -1 ? kode_produk.Substring(0, index) : kode_produk;

                        p.command("INSERT INTO transaksi (id_produk, nama_pelanggan, nomor_unik, total_harga, uang_bayar, uang_kembali, created_at, updated_at) values ((select id from products where kode_produk = '" + kodeProduk + "'), '" + nama_pelanggan + "', '" + nomorUnik + "', '" + totalhargab + "', '" + uang_bayar + "', '" + uang_kembali + "', NOW(), NOW())");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }


        void totalbelanja()
        {
            decimal totalHarga = 0;

            foreach (DataGridViewRow row in dgtransaksi.Rows)
            {
                if (row.Cells[7].Value != null && row.Cells[7].Value != DBNull.Value)
                {
                    totalHarga += Convert.ToDecimal(row.Cells[7].Value);
                }
            }

            lblTotal.Text = totalHarga.ToString("0.##");
        }

        void addTroli()
        {
            string namap = txtnamap.Text;
            string kode = cbnamak.Text.Split('-')[0].Trim();
            string namaproduk = cbnamak.Text.Split('-')[1].Trim();
            string hargatotal = txttotal.Text;
            string hargasatuan = txthargap.Text;
            string qty = txtqty.Text;
            string nounik = txtnomorunik.Text;

            dgtransaksi.Rows.Add();
            dgtransaksi.Rows.Add();
            dgtransaksi.Rows[i].Cells[0].Value = IdTr;
            dgtransaksi.Rows[i].Cells[1].Value = nounik;
            dgtransaksi.Rows[i].Cells[2].Value = namap;
            dgtransaksi.Rows[i].Cells[3].Value = kode;
            dgtransaksi.Rows[i].Cells[4].Value = namaproduk;
            dgtransaksi.Rows[i].Cells[5].Value = hargasatuan;
            dgtransaksi.Rows[i].Cells[6].Value = qty;
            dgtransaksi.Rows[i].Cells[7].Value = hargatotal;

            i++;
            IdTr++;

        }

        void getBarang()
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT kode_produk, nama_produk FROM products", conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                id = reader["kode_produk"].ToString();
                nama = reader["nama_produk"].ToString();
                cbnamak.Items.Add(id + "-" + nama);
            }


            reader.Close();
            conn.Close();
        }

        string GetHarga()
        {
            MySqlConnection conn = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=gatolin");
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select harga_produk from products where kode_produk like '" + cbnamak.Text.Split('-')[0].Trim() + "%'", conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            string harga_satuan = "";

            if (reader.Read())
            {
                harga_satuan = reader["harga_produk"].ToString();
            }

            conn.Close();
            reader.Close();

            return harga_satuan;
        }

        void clear()
        {
            txtnamap.Text = string.Empty;
            cbnamak.Text = string.Empty;
            txttotal.Text = string.Empty;
            txthargap.Text = string.Empty;
            txtqty.Text = string.Empty;

        }

        private void UpdateKembalianOtomatis(object sender, EventArgs e)
        {
            // Memeriksa apakah nilai di TextBox Total dan Uang Bayar valid
            if (decimal.TryParse(lblTotal.Text, out decimal totalBelanja) && decimal.TryParse(txtuangb.Text, out decimal uangBayar))
            {
                // Menghitung kembalian
                decimal kembalian = HitungKembalian(totalBelanja, uangBayar);

                // Menampilkan hasil ke TextBox Kembalian
                txtkembali.Text = kembalian.ToString("0.##"); // Menampilkan sebagai mata uang
            }
            else
            {
                // Jika nilai tidak valid, tampilkan pesan kesalahan di TextBox Kembalian
                txtkembali.Text = "";
            }
        }

        private decimal HitungKembalian(decimal totalBelanja, decimal uangBayar)
        {
            return uangBayar - totalBelanja;
        }

        private void UpdateTotalBelanjaOtomatis(object sender, EventArgs e)
        {
            if (int.TryParse(txtqty.Text, out int qty) && decimal.TryParse(txthargap.Text, out decimal hargaPerBarang))
            {
                decimal totalBelanja = HitungTotalBelanja(qty, hargaPerBarang);
                txttotal.Text = totalBelanja.ToString("0.##"); // Menampilkan sebagai mata uang
            }
            else
            {
                txttotal.Text = "";
            }
        }

        private decimal HitungTotalBelanja(int qty, decimal hargaPerBarang)
        {
            return qty * hargaPerBarang;
        }

        private Random random = new Random();

        private void kasir_Load(object sender, EventArgs e)
        {
            int randomNumber = random.Next(1000, 9999);

            // Input nomor acak ke dalam TextBox
            txtnomorunik.Text = randomNumber.ToString();

            getBarang();
            txtqty.TextChanged += UpdateTotalBelanjaOtomatis;
            txthargap.TextChanged += UpdateTotalBelanjaOtomatis;

            txttotal.TextChanged += UpdateKembalianOtomatis;
            txtuangb.TextChanged += UpdateKembalianOtomatis;

            tambahdg.Click -= bayar_Click;
            tambahdg.Click += bayar_Click;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtidp_TextChanged(object sender, EventArgs e)
        {

        }

        private void bayar_Click(object sender, EventArgs e)
        {
            DialogResult d;
            d = MessageBox.Show("Apakah anda yakin?", "Pertanyaan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (d == DialogResult.Yes)
            {
                addTroli();
                clear();
                txtnamap.Enabled = false;
                totalbelanja();
            }
        }

        private void logout_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {

                Logout();
            }
        }

        private void print_Click(object sender, EventArgs e)
        {
            addDb();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txthargap.Text = GetHarga();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addDb();
            string invoiceData = "Invoice\n\n" +
                                    "No\tCustomer Name\tProduct Code\tProduct Name\tUnit Price\tQuantity\tTotal\n";
            double totalAmount = 0;

            foreach (DataGridViewRow row in dgtransaksi.Rows)
            {
                string rowText = $"{row.Cells[0].Value}\t{row.Cells[2].Value}\t{row.Cells[3].Value}\t{row.Cells[4].Value}\t{row.Cells[5].Value}\t{row.Cells[6].Value}\t{row.Cells[7].Value}\n";
                invoiceData += rowText;

                totalAmount += Convert.ToDouble(row.Cells[7].Value);
            }

            string totalBelanja = lblTotal.Text;
            string uangBayar = txtuangb.Text;
            string uangKembali = txtkembali.Text;

            PrintDocument printDocument = new PrintDocument();
            printDocument.DefaultPageSettings.Landscape = false; // Set the orientation to portrait
            printDocument.PrintPage += (s, ev) =>
            {
                int startX = ev.MarginBounds.Left;
                int startY = ev.MarginBounds.Top;
                int printableWidth = ev.MarginBounds.Width;
                int rowHeight = 20;
                int offset = 40;

                float x = startX;
                float y = startY;

            
                y += 60; // Adjust the vertical position after logo

                // Custom Header
                string header = "Indomaret Invoice";
                SizeF headerSize = ev.Graphics.MeasureString(header, new Font("Arial", 18, FontStyle.Bold));
                x = (printableWidth - headerSize.Width) / 2;
                ev.Graphics.DrawString(header, new Font("Arial", 18, FontStyle.Bold), Brushes.Black, x, y);
                y += headerSize.Height + rowHeight * 2; // Increased spacing

                // Print invoice data headers
                string[] headers = { "No", "Customer Name", "Product Code", "Unit Price", "Quantity", "Total" };
                x = startX;
                foreach (string headerText in headers)
                {
                    ev.Graphics.DrawString(headerText, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, x, y);
                    y += rowHeight; // Move to the next row vertically
                }

                // Print "Product Name" header
                x = startX + 200; // Adjust the x position for "Product Name"
                y = startY + headerSize.Height + rowHeight * 2; // Reset y position
                ev.Graphics.DrawString("Product Name", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, x, y);

                // Draw line separator
                ev.Graphics.DrawLine(new Pen(Color.Black), startX, startY + headerSize.Height + rowHeight * 2, startX, y + rowHeight * 5); // Adjust the length of the line
                ev.Graphics.DrawLine(new Pen(Color.Black), startX + 100, startY + headerSize.Height + rowHeight * 2, startX + 100, y + rowHeight * 5); // Adjust the length of the line
                ev.Graphics.DrawLine(new Pen(Color.Black), startX + 200, startY + headerSize.Height + rowHeight * 2, startX + 200, y + rowHeight * 5); // Adjust the length of the line

                // Draw line separator between headers and data
                ev.Graphics.DrawLine(new Pen(Color.Black), startX, y, startX + 300, y);

                y += rowHeight;

                // Print invoice data
                foreach (var line in invoiceData.Split('\n'))
                {
                    string[] parts = line.Split('\t');
                    x = startX;

                    for (int i = 0; i < parts.Length; i++)
                    {
                        ev.Graphics.DrawString(parts[i], new Font("Arial", 10), Brushes.Black, x, y);
                        x += (printableWidth / 7); // Adjust position dynamically based on the number of columns
                    }
                    y += rowHeight;
                }

                // Draw line separator
                ev.Graphics.DrawLine(new Pen(Color.Black), startX, y, startX + printableWidth, y);
                y += rowHeight;

                // Print total amount
                ev.Graphics.DrawString("Total Belanja: ", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, startX, y);
                ev.Graphics.DrawString(totalBelanja, new Font("Arial", 12), Brushes.Black, startX + 200, y);
                y += rowHeight;

                // Print payment details
                ev.Graphics.DrawString("Uang Bayar: ", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, startX, y);
                ev.Graphics.DrawString(uangBayar, new Font("Arial", 12), Brushes.Black, startX + 200, y);
                y += rowHeight;

                ev.Graphics.DrawString("Uang Kembali: ", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, startX, y);
                ev.Graphics.DrawString(uangKembali, new Font("Arial", 12), Brushes.Black, startX + 200, y);
            };

            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            printPreviewDialog.Document = printDocument;

            if (printPreviewDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }


        }

        private void txthargap_TextChanged(object sender, EventArgs e)
        {

        }
    }
    }
        

