using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class dbadmin : Form
    {
        string conn = "datasource=127.0.0.1;port=3306;username=root;password=;database=gatolin";
        public dbadmin()
        {
            InitializeComponent();
        }
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

        private void bunifuButton2_Click(object sender, EventArgs e)
        {

        }



        private void bunifuButton6_Click_1(object sender, EventArgs e)
        {
            Logout();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Adminbarang().Show();
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            this.Hide();
            new adminuser().Show();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            this.Hide();
            new adminT().Show();
        }

        private void dbadmin_Load(object sender, EventArgs e)
        {
            p.showData ("select * from products", dataGridView1);
            string query = "SELECT COUNT(*) FROM users";

            using (MySqlConnection connection = new MySqlConnection(conn))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                int count = Convert.ToInt32(command.ExecuteScalar());

                // Menampilkan jumlah data dalam tombol Bunifu
                btnjmlhuser.Text = "Jumlah user: " + count.ToString();
            }
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void bunifuSeparator1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
