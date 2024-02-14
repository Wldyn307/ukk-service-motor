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
               " l.nama_pelanggan,l.id_produk,u.kode_produk, u.nama_produk, u.harga_produk, l.uang_bayar, l.uang_kembali , l.total_harga, l.nomor_unik, l.created_at " +
            "FROM transaksi l " +
                 "JOIN products u ON l.id_produk = u.id";

            p.showData(query, dgtransaksi);

        }

        private void adminT_Load(object sender, EventArgs e)
        {
            getBarang();
            txtnamap.Enabled = false;
            string query = "SELECT l.id, " +
               " l.nama_pelanggan,l.id_produk,u.kode_produk, u.nama_produk, u.harga_produk, l.uang_bayar, l.uang_kembali , l.total_harga, l.nomor_unik, l.created_at " +
            "FROM transaksi l " +
                 "JOIN products u ON l.id_produk = u.id";

            p.showData(query, dgtransaksi);
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            p.command("insert into log (id_user, activity, created_at) VALUES ('" + Data.id_user + "', 'Admin edits product', NOW())");
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

                    clear();
                }
            }

        }



        private void dgtransaksi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnedit.Enabled = true;
            DataGridViewRow dr = this.dgtransaksi.Rows[e.RowIndex];
            txtnamap.Text = dr.Cells[1].Value.ToString();
            txttharga.Text = dr.Cells[8].Value.ToString();
            txtuangb.Text = dr.Cells[6].Value.ToString();
            txtuangk.Text = dr.Cells[7].Value.ToString();
            id = dr.Cells[0].Value.ToString();


            string idProduk = dr.Cells[2].Value.ToString();
            // Ambil nama produk dari kolom 4
            string namaProduk = dr.Cells[4].Value.ToString();

            // Gabungkan id dan nama produk dengan pemisah "-"
            string combined = idProduk + " - " + namaProduk;

            // Set nilai ComboBox
            comboBox1.Text = combined;

        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

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
              "u.kode_produk, u.nama_produk, u.harga_produk, " +
              "l.uang_bayar, l.uang_kembali, l.total_harga, " +
              "l.nomor_unik, l.created_at " +
           "FROM transaksi l " +
           "JOIN products u ON l.id_produk = u.id " +
           "WHERE l.nama_pelanggan LIKE '" + txtcari.Text + "%'", dgtransaksi);
        }
    }
    }
