using CrystalDecisions.CrystalReports.Engine;
using iTextSharp.text.pdf;
using iTextSharp.text;
using MySql.Data.MySqlClient;
using Mysqlx.Expect;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace WindowsFormsApp2
{
    public partial class Owner : Form
    {

        Data p = new Data();
        MySqlConnection conn = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=gatolin");

        void  Logout()
        {

            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Hide();
                LOGIN loginForm = new LOGIN();
                loginForm.Show();
            }

        }


        public Owner()
        {
            InitializeComponent();
            
        }

        void filter()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=gatolin");
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    using (DataTable dt = new DataTable("log"))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("SELECT l.id, l.id_user, u.nama, u.role, l.activity, l.created_at FROM log l JOIN users u ON l.id_user = u.id WHERE DATE (l.created_at) >= DATE (@fromdate) AND DATE (l.created_at) < DATE (@todate + INTERVAL 1 DAY)", conn))
                        {
                            cmd.Parameters.AddWithValue("@fromdate", dt1.Value);
                            cmd.Parameters.AddWithValue("@todate", dt2.Value);
                            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(cmd);
                            mySqlDataAdapter.Fill(dt);
                            dataGridView1.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void print()
        {
            // Membuat instance dari class Document iTextSharp
            Document doc = new Document(PageSize.A4.Rotate(), 10f, 10f, 10f, 0f);

            iTextSharp.text.Font fontTitle = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 20, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12);
            // Membuat objek paragraph dengan judul
            Paragraph title = new Paragraph("Laporan Kegiatan", fontTitle);
            title.Alignment = Element.ALIGN_CENTER; // Mengatur perataan teks
            title.SpacingAfter = 30;
            title.SpacingBefore = 30;

            // Menambahkan tanggal saat ini ke dalam dokumen
            Paragraph currentDate = new Paragraph("Tanggal: " + DateTime.Now.ToShortDateString(), font);
            currentDate.Alignment = Element.ALIGN_RIGHT; // Mengatur perataan teks ke kanan
            currentDate.SpacingAfter = 10;
            currentDate.SpacingBefore = 10; // Spasi sebelum tanggal

            Paragraph currentTime = new Paragraph("Waktu: " + DateTime.Now.ToShortTimeString(), font);
            currentTime.Alignment = Element.ALIGN_RIGHT;
            currentTime.SpacingAfter = 10;
            currentTime.SpacingBefore = 10;

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "PDF (.pdf)|.pdf";
            saveFileDialog1.FileName = "log ativi.pdf";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                PdfWriter.GetInstance(doc, new FileStream(saveFileDialog1.FileName, FileMode.Create));

                doc.Open();

                doc.Add(title);
                doc.Add(currentDate);

                doc.Add(currentTime);

                PdfPTable table = new PdfPTable(dataGridView1.Columns.Count);

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

                table.DefaultCell.BorderWidth = 0;
                table.DefaultCell.BorderColor = new iTextSharp.text.BaseColor(200, 200, 200);
                table.DefaultCell.Padding = 7;
                table.WidthPercentage = 100;

                doc.Add(table);

                doc.Close();
                MessageBox.Show("Data berhasil di-print ke dalam file PDF.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);


                Process.Start(saveFileDialog1.FileName);
            }
        }



        private void Owner_Load(object sender, EventArgs e)
        {
            string query = "SELECT l.id, l.id_user, u.nama, u.role, l.activity, l.created_at " +
            "FROM log l " +
                 "JOIN users u ON l.id_user = u.id";

            p.showData(query, dataGridView1);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {


        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
          
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void reportDocument1_InitReport(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void search_TextChanged(object sender, EventArgs e)
        {

        }

        private void reset_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }


        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void txtcari_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            print();
        }

       

        private void bunifuGradientPanel1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new ownerT().Show();
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            Logout();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            filter();
        }
    }

}




