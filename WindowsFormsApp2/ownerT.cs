using iTextSharp.text.pdf;
using iTextSharp.text;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class ownerT : Form
    {
       
       
        public ownerT()
        {
            InitializeComponent();
        }
        private void Logout()
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                p.command("insert into log (id_user , activity, created_at) VALUES ('" + Data.id_user + "', 'Logout' , NOW())");
                this.Hide();
                LOGIN loginForm = new LOGIN();
                loginForm.Show();
            }
        }

        void print()
        {
            // Membuat instance dari class Document iTextSharp
            Document doc = new Document(PageSize.A4.Rotate(), 10f, 10f, 10f, 0f);

            iTextSharp.text.Font fontTitle = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 20, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12);
            // Membuat objek paragraph dengan judul
            Paragraph title = new Paragraph("Laporan transaksi", fontTitle);
            title.Alignment = Element.ALIGN_CENTER; // Mengatur perataan teks
            title.SpacingAfter = 30;
            title.SpacingBefore = 30;

            // Menambahkan tanggal saat ini ke dalam dokumen
            Paragraph currentDate = new Paragraph("Tanggal: " + DateTime.Now.ToShortDateString(), font);
            currentDate.Alignment = Element.ALIGN_RIGHT; // Mengatur perataan teks ke kanan
            currentDate.SpacingAfter = 10; // Spasi setelah tanggal
            currentDate.SpacingBefore = 10; // Spasi sebelum tanggal

            // Menambahkan waktu saat ini (jam) ke dalam dokumen
            Paragraph currentTime = new Paragraph("Waktu: " + DateTime.Now.ToShortTimeString(), font);
            currentTime.Alignment = Element.ALIGN_RIGHT; // Mengatur perataan teks ke kanan
            currentTime.SpacingAfter = 10; // Spasi setelah waktu
            currentTime.SpacingBefore = 10; // Spasi sebelum waktu

            // Menentukan lokasi penyimpanan file PDF
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "PDF (.pdf)|.pdf";
            saveFileDialog1.FileName = "laporan transaksi.pdf";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Membuka file PDF
                PdfWriter.GetInstance(doc, new FileStream(saveFileDialog1.FileName, FileMode.Create));

                // Membuka dokumen
                doc.Open();

                // Menambahkan paragraph judul ke dokumen
                doc.Add(title);

                // Menambahkan tanggal saat ini ke dalam dokumen
                doc.Add(currentDate);

                // Menambahkan waktu saat ini (jam) ke dalam dokumen
                doc.Add(currentTime);

                // Membuat table dengan jumlah kolom sesuai dengan jumlah kolom di dalam DataGridView
                PdfPTable table = new PdfPTable(dataGridView1.Columns.Count);

                // Menambahkan header ke dalam table
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(dataGridView1.Columns[i].HeaderText));
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Padding = 5;
                    cell.BorderWidth = 1;
                    table.AddCell(cell);
                }

                // Menambahkan data dari DataGridView ke dalam table
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        if (dataGridView1[j, i].Value != null)
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(dataGridView1[j, i].Value.ToString()));
                            cell.Padding = 5;
                            cell.BorderWidth = 1;
                            table.AddCell(cell);
                        }
                    }
                }

                // Mengatur garis di sekitar tabel
                table.DefaultCell.BorderWidth = 0;
                table.DefaultCell.BorderColor = new iTextSharp.text.BaseColor(200, 200, 200);
                table.DefaultCell.Padding = 7;
                table.WidthPercentage = 100;

                // Menambahkan table ke dalam dokumen
                doc.Add(table);
                // Menghitung total uang bayar dari DataGridView
                double totalUangBayar = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells[8].Value != null)
                    {
                        totalUangBayar += Convert.ToDouble(dataGridView1.Rows[i].Cells[8].Value);
                    }
                }

                // Menambahkan paragraf baru untuk menampilkan total uang bayar ke dalam dokumen
                Paragraph totalUangBayarParagraph = new Paragraph("Total Uang Bayar: " + totalUangBayar.ToString(), font);
                totalUangBayarParagraph.Alignment = Element.ALIGN_RIGHT;
                totalUangBayarParagraph.SpacingAfter = 10; // Spasi setelah total uang bayar
                doc.Add(totalUangBayarParagraph);

                // Menutup dokumen dan writer
               

                // Menutup dokumen dan writer
                doc.Close();
                MessageBox.Show("Data berhasil di-print ke dalam file PDF.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Membuka file PDF setelah disimpan
                Process.Start(saveFileDialog1.FileName);
            }
        }

        void filter()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=gatolin");
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    using (DataTable dt = new DataTable("transaksi"))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("SELECT l.id, " + " l.nama_pelanggan,l.id_produk,u.kode_produk, u.nama_produk, u.harga_produk, l.uang_bayar, l.uang_kembali , l.total_harga, l.nomor_unik, l.created_at " + "FROM transaksi l " +  "JOIN products u ON l.id_produk = u.id  WHERE DATE (l.created_at) >= DATE (@fromdate) AND DATE (l.created_at) < DATE (@todate + INTERVAL 1 DAY)", conn))

                        {
                            cmd.Parameters.AddWithValue("@fromdate", dt1.Value.Date);
                            cmd.Parameters.AddWithValue("@todate", dt2.Value.Date);
                            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(cmd);
                            mySqlDataAdapter.Fill(dt);
                            dataGridView1.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        


        Data p = new Data();

        private void ownerT_Load(object sender, EventArgs e)
        {
            string query = "SELECT l.id, " +
               " l.nama_pelanggan,l.id_produk,u.kode_produk, u.nama_produk, u.harga_produk, l.uang_bayar, l.uang_kembali , l.total_harga, l.nomor_unik, l.created_at " +
            "FROM transaksi l " +
                 "JOIN products u ON l.id_produk = u.id";
            p.showData(query, dataGridView1);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void dt2_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void dt1_ValueChanged(object sender, EventArgs e)
        {
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

           
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Owner().Show();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            print();
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            Logout();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Owner().Show();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            filter();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            print();
        }
    }
}
