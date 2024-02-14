using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using MySql.Data.MySqlClient;
using static Mysqlx.Notice.Warning.Types;
using Org.BouncyCastle.Crypto;
using CrystalDecisions.CrystalReports.Engine;

namespace WindowsFormsApp2
{
    public partial class Adminbarang : Form
    {
        string id;


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

        public Adminbarang()
        {
            InitializeComponent();

        }

        Data p = new Data();

        void clear()
        {

            nama.Text = string.Empty;
            harga.Text = string.Empty;
            txtstok.Text = string.Empty;
            txtkode.Text = string.Empty;
            p.showData("Select * from products", DataGridView1);
        }


        private void AdminB_Load(object sender, EventArgs e)
        {
            btnedit.Enabled = false;
            clear();
        }

        void insert()
        {
            p.command("insert into log (id_user , activity, created_at) VALUES ('" + Data.id_user + "', 'admin adds product' , NOW())");
            if (nama.Text == string.Empty || harga.Text == string.Empty)
            {
                MessageBox.Show("Semua kolom harus diisi!");
            }
            else
            {
                DialogResult result = MessageBox.Show("Apakah Anda yakin ingin mengupdate barang ini?", "Konfirmasi Hapus Barang", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    string query = "INSERT INTO products (kode_produk, nama_produk, harga_produk, stok, created_at) VALUES ('" + txtkode.Text + "', '" + nama.Text + "', '" + harga.Text + "','" + txtstok.Text + "', NOW() )";
                    p.command(query);
                    clear();
                }
            }
        }

        void update()
        {
            p.command("insert into log (id_user , activity, created_at) VALUES ('" + Data.id_user + "', 'Admin edits products' , NOW())");
            if (nama.Text == string.Empty || harga.Text == string.Empty || txtstok.Text == string.Empty)
            {
                MessageBox.Show("semua kolom harus di isi!");
            }
            else
            {
                DialogResult result = MessageBox.Show("Apakah Anda yakin ingin mengupdate barang ini?", "Konfirmasi Hapus Barang", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    p.command("update products set kode_produk = '" + txtkode.Text + "', nama_produk = '" + nama.Text + "', harga_produk = '" + harga.Text + "',stok = '" + txtstok.Text + "', updated_at = NOW() where id = '" + id + "' ");
                    clear();
                }
            }
        }

        void delete()
        {
            p.command("insert into log (id_user, activity, created_at) VALUES ('" + Data.id_user + "', 'Admin deletes product', NOW())");

            if (nama.Text == string.Empty || harga.Text == string.Empty)
            {
                MessageBox.Show("Semua kolom harus di isi!");
            }
            else
            {

                DialogResult result = MessageBox.Show("Apakah Anda yakin ingin menghapus barang ini?", "Konfirmasi Hapus Barang", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {

                    p.command("delete from products where kode_produk = '" + txtkode.Text + "'");
                    clear();
                }

            }
        }

       

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
           
           

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new adminuser().Show();
        }

        private void nama_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           
        }

        private void txtkodep_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            p.showData("select * from products where nama_produk like '" + txtcari.Text + "%'", DataGridView1);
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            Logout();
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            this.Hide();
            new adminuser().Show();
        }

        private void txtcari_TextChanged(object sender, EventArgs e)
        {
            p.showData("select * from products where nama_produk like '" + txtcari.Text + "%'", DataGridView1);
        }

        private void harga_TextChanged(object sender, EventArgs e)
        {

        }

        private void DataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            btntambah.Enabled = false;
            btnedit.Enabled = true;
            DataGridViewRow dr = this.DataGridView1.Rows[e.RowIndex];
            nama.Text = dr.Cells[2].Value.ToString();
            harga.Text = dr.Cells[3].Value.ToString();
            txtstok.Text = dr.Cells[4].Value.ToString();
            id = dr.Cells[0].Value.ToString();
            txtkode.Text = dr.Cells[1].Value.ToString();
            
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            btntambah.Enabled = true;
            update();

        }

        private void btntambah_Click(object sender, EventArgs e)
        {
            insert();
        }

        private void btnhapus_Click(object sender, EventArgs e)
        {
            delete();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new adminT().Show();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            clear();
            btntambah.Enabled = true;
        }
    }
}
