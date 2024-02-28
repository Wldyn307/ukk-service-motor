using CrystalDecisions.CrystalReports.Engine;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp2
{
    public partial class historyT : Form
    {
        public historyT()
        {
            InitializeComponent();
        }
      
        Data p = new Data();
        private void Logout()
        {
            p.command("insert into log (id_user , activity, created_at) VALUES ('" + Data.id_user + "', 'kasir logout' , NOW())");
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {

                this.Hide();
                LOGIN loginForm = new LOGIN();
                loginForm.Show();
            }
        }

       

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new kasirt().Show();
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            Logout();
        }

       

        private void historyT_Load(object sender, EventArgs e)
        {
            not.Enabled = false;
            txtnamap.Enabled = false;
            comboBox1.Enabled = false;
            txttharga.Enabled = false;
            txtuangb.Enabled = false;
            txtuangk.Enabled = false;
            string query = "SELECT l.id, " +
              " l.nama_pelanggan,l.id_produk, u.nama_produk, u.harga_produk, l.uang_bayar, l.uang_kembali , l.total_harga, l.nomor_unik, l.created_at " +
           "FROM transaksi l " +
                "JOIN products u ON l.id_produk = u.id";

            p.showData(query, dgtransaksi);
        }

        private void dgtransaksi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dr = this.dgtransaksi.Rows[e.RowIndex];
            txtnamap.Text = dr.Cells[1].Value.ToString();
            txttharga.Text = dr.Cells[7].Value.ToString();
            txtuangb.Text = dr.Cells[5].Value.ToString();
            
            txtuangk.Text = dr.Cells[6].Value.ToString();
            not.Text = dr.Cells[8].Value.ToString();
            string kodeproduk = dr.Cells[2].Value.ToString();
            // Ambil nama produk dari kolom 4
            string namaProduk = dr.Cells[3].Value.ToString();

            // Gabungkan id dan nama produk dengan pemisah "-"
            string combined = kodeproduk + "-" + namaProduk;

            // Set nilai ComboBox
            comboBox1.Text = combined;
        }

        private void dgtransaksi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtcari_TextChanged(object sender, EventArgs e)
        {
            p.showData("select * from transaksi where nama_pelanggan  like '" + txtcari.Text + "%'", dgtransaksi); 
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new kasirt().Show();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            this.Hide();
            new dbkasir().Show();
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cetak_Click(object sender, EventArgs e)
        {
            p.command("insert into log (id_user , activity, created_at) VALUES ('" + Data.id_user + "', 'kasir melakukan transaksi' , NOW())");
            laporan frm = new laporan();
            CrystalReport1 cr1 = new CrystalReport1();
            DataGridViewRow dr = this.dgtransaksi.Rows[0];
         
            string idProduk = comboBox1.Text.Split('-')[0].Trim();
            string namaProduk = comboBox1.Text.Split('-')[1].Trim();



            TextObject namap = (TextObject)cr1.ReportDefinition.Sections["Section3"].ReportObjects["namap"];
            namap.Text = txtnamap.Text;
            TextObject idproduk = (TextObject)cr1.ReportDefinition.Sections["Section3"].ReportObjects["idproduk"];
            idproduk.Text = idProduk;
            TextObject namaproduk = (TextObject)cr1.ReportDefinition.Sections["Section3"].ReportObjects["namaproduk"];
            namaproduk.Text = namaProduk;
            TextObject hargaproduk = (TextObject)cr1.ReportDefinition.Sections["Section3"].ReportObjects["harga"];
            hargaproduk.Text = txttharga.Text;
            TextObject uangb = (TextObject)cr1.ReportDefinition.Sections["Section3"].ReportObjects["uangbayar"];
            uangb.Text = txtuangb.Text;
            TextObject kembalian = (TextObject)cr1.ReportDefinition.Sections["Section3"].ReportObjects["kembalian"];
            kembalian.Text = txtuangk.Text;


            frm.crystalReportViewer1.ReportSource = cr1;
            frm.Show();
        }
    }
}
