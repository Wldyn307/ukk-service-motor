using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class adminuser : Form
    {
        string id;
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
        

        void insert()
        {
            p.command("insert into log (id_user, activity, created_at) VALUES ('" + Data.id_user + "', 'Admin menambahkan user', NOW())");
            if (us.Text == string.Empty || pw.Text == string.Empty || nama.Text == string.Empty || role.Text == string.Empty)
            {
                MessageBox.Show("Semua kolom harus diisi!");
            }
            else
            {
                DialogResult result = MessageBox.Show("Apakah Anda yakin ingin menambah barang ini?", "Konfirmasi Hapus Barang", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string query = "INSERT INTO users (username, password, nama, role, created_at) VALUES ('" + us.Text + "', '" + pw.Text + "', '" + nama.Text + "', '" + role.Text + "', NOW())";

                    p.command(query);
                    clear();
                }
            }
        }

        void update()
        {

            p.command("insert into log (id_user, activity, created_at) VALUES ('" + Data.id_user + "', 'Admin edit produk', NOW())");
            if (us.Text == string.Empty || pw.Text == string.Empty || nama.Text == string.Empty || role.Text == string.Empty)
            {
                MessageBox.Show("semua kolom harus di isi!");
            }
            else
            {
                DialogResult result = MessageBox.Show("Apakah Anda yakin ingin update barang ini?", "Konfirmasi update Barang", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    p.command("update users SET username = '" + us.Text + "', password = '" + pw.Text + "', nama = '" + nama.Text + "', role = '" + role.Text + "', updated_at = NOW() WHERE id = '" + id + "'");

                    clear();
                }
            }
        }

        void delete()
        {
            p.command("insert into log (id_user, activity, created_at) VALUES ('" + Data.id_user + "', 'Admin menghapus produk', NOW())");
            {
                if (us.Text == string.Empty || pw.Text == string.Empty)
                {
                    MessageBox.Show("semua kolom harus di isi!");
                }
                else
                {
                    DialogResult result = MessageBox.Show("Apakah Anda yakin ingin menghapus barang ini?", "Konfirmasi Hapus Barang", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        p.command("delete from users where username = '" + us.Text + "'");
                        clear();
                    }
                }
            }
        }
        void clear()
        {

            
            us.Text = string.Empty;
            pw.Text = string.Empty;
            nama.Text = string.Empty;
            role.Text = string.Empty;
            p.showData("Select * from users", dataGridView1);
        }
        public adminuser()
        {
            InitializeComponent();
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
         

        }

      

        private void adminA_Load(object sender, EventArgs e)
        {
            btnedit.Enabled = false;
            clear();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

       

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

       

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Adminbarang().Show();
        }

        private void us_TextChanged(object sender, EventArgs e)
        {

        }

        private void nama_TextChanged(object sender, EventArgs e)
        {
              
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Adminbarang().Show();
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
          
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {
            p.showData("select * from users where nama like '" + txtcari.Text + "%'", dataGridView1);
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            insert();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dr = this.dataGridView1.Rows[e.RowIndex];

            btnedit.Enabled = true;
            btntambah.Enabled = false;
            us.Text = dr.Cells[1].Value.ToString();
            pw.Text = dr.Cells[2].Value.ToString();
            nama.Text = dr.Cells[3].Value.ToString();
            role.Text = dr.Cells[4].Value.ToString();
            id = dr.Cells[0].Value.ToString();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            update();
            btntambah.Enabled = true;
            btnedit.Enabled = false;
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            delete();
            btntambah.Enabled = true;
        }

        private void bunifuButton2_Click_1(object sender, EventArgs e)
        {
            Logout();
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton3_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            new adminT().Show();
        }

        private void bunifuButton3_Click_2(object sender, EventArgs e)
        {
            this.Hide();
            new adminT().Show();
        }

        private void bunifuButton4_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            new dbadmin().Show();
        }

        private void btnbatal_Click(object sender, EventArgs e)
        {
            btntambah.Enabled = true;
            clear();
        }
    }
}
