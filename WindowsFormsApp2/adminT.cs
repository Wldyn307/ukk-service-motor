using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp2
{
    public partial class adminT : Form
    {
        public adminT()
        {
            InitializeComponent();
        }
        string id = "";
        string nama = "";
        MySqlConnection conn = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=gatolin");
        Data p = new Data();

        private void Logout()
        {
            p.command("insert into log (id_user , activity, created_at) VALUES ('" + Data.id_user + "', 'Admin logout' , NOW())");
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {

                this.Hide();
                LOGIN loginForm = new LOGIN();
                loginForm.Show();
            }
        }

        string GetHarga()
        {
            MySqlConnection conn = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=gatolin");
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select harga_produk from products where id like '" + comboBox1.Text.Split('-')[0].Trim() + "%'", conn);
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


        void getBarang()
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT id, nama_produk FROM products", conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                id = reader["id"].ToString();
                nama = reader["nama_produk"].ToString();
                comboBox1.Items.Add(id + "-" + nama);
            }


            reader.Close();
            conn.Close();
        }

        void clear()
        {
       
            txtnamap.Text = string.Empty;
            txttharga.Text = string.Empty;
            txtuangb.Text = string.Empty;
            txtuangk.Text = string.Empty;
            comboBox1.Text = string.Empty;
            string query = "SELECT l.id, " +
               " l.nama_pelanggan,l.id_produk, u.nama_produk, u.harga_produk, l.uang_bayar, l.uang_kembali , l.total_harga, l.nomor_unik, l.created_at " +
            "FROM transaksi l " +
                 "JOIN products u ON l.id_produk = u.id";

            p.showData(query, dgtransaksi);

        }

        private void UpdateKembalian(object sender, EventArgs e)
        {
            // Memeriksa apakah nilai di TextBox Total dan Uang Bayar valid
            if (decimal.TryParse(txttharga.Text, out decimal totalBelanja) && decimal.TryParse(txtuangb.Text, out decimal uangBayar))
            {
                // Menghitung kembalian
                decimal kembalian = uangBayar - totalBelanja;

                // Menampilkan hasil ke Label Kembalian
                txtuangk.Text = kembalian.ToString("0.##"); // Menampilkan sebagai mata uang
            }
            else
            {

                txtuangk.Text = "";
            }
        }

        private void adminT_Load(object sender, EventArgs e)
        {
            txttharga.TextChanged += UpdateKembalian;
            txtuangb.TextChanged += UpdateKembalian;
            getBarang();
            txtnamap.Enabled = false;
            string query = "SELECT l.id, " +
               " l.nama_pelanggan,l.id_produk, u.nama_produk, u.harga_produk, l.uang_bayar, l.uang_kembali , l.total_harga, l.nomor_unik, l.created_at " +
            "FROM transaksi l " +
                 "JOIN products u ON l.id_produk = u.id";

            p.showData(query, dgtransaksi);
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
           
            if ( txtnamap.Text == string.Empty || txttharga.Text == string.Empty || txtuangb.Text == string.Empty || txtuangk.Text == string.Empty)
            {
                MessageBox.Show("semua kolom harus di isi!");
            }
            else
            {
                DialogResult result = MessageBox.Show("Apakah Anda yakin ingin update barang ini?", "Konfirmasi Hapus Barang", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string[] idNamaProduk = comboBox1.SelectedItem.ToString().Split('-');
                    string idProduk = idNamaProduk[0].Trim(); 

                    p.command("update transaksi SET nama_pelanggan = '" + txtnamap.Text + "', id_produk = '" + idProduk + "', total_harga = '" + txttharga.Text + "', uang_bayar = '" + txtuangb.Text + "', uang_kembali = '" + txtuangk.Text + "', updated_at = NOW() WHERE id = '" + id + "'");
                    p.command("insert into log (id_user, activity, created_at) VALUES ('" + Data.id_user + "', 'Admin edit transaksi', NOW())");
                    clear();
                }
            }

        }



        private void dgtransaksi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnedit.Enabled = true;
            DataGridViewRow dr = this.dgtransaksi.Rows[e.RowIndex];
            txtnamap.Text = dr.Cells[1].Value.ToString();
            txttharga.Text = dr.Cells[4].Value.ToString();
            txtuangb.Text = dr.Cells[5].Value.ToString();
            txtuangk.Text = dr.Cells[6].Value.ToString();
            id = dr.Cells[0].Value.ToString();


            string idProduk = dr.Cells[2].Value.ToString();
            // Ambil nama produk dari kolom 4
            string namaProduk = dr.Cells[3].Value.ToString();

            // Gabungkan id dan nama produk dengan pemisah "-"
            string combined = idProduk + " - " + namaProduk;

            // Set nilai ComboBox
            comboBox1.Text = combined;

        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txttharga.Text = GetHarga();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            Logout();
        }

        private void txtcari_TextChanged(object sender, EventArgs e)
        {
            p.showData("SELECT l.id, " +
              "l.nama_pelanggan, l.id_produk, " +
              "u.nama_produk, u.harga_produk, " +
              "l.uang_bayar, l.uang_kembali, l.total_harga, " +
              "l.nomor_unik, l.created_at " +
           "FROM transaksi l " +
           "JOIN products u ON l.id_produk = u.id " +
           "WHERE l.nama_pelanggan LIKE '" + txtcari.Text + "%'", dgtransaksi);
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
           
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            this.Hide();
            new adminuser().Show();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Adminbarang().Show();
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {

            this.Hide();
            new dbadmin().Show();
        }
    }
    }
