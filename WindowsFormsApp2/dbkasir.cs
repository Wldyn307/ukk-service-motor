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
    public partial class dbkasir : Form
    {
        Data p = new Data();
        public dbkasir()
        {
            InitializeComponent();
        }

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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new kasirt().Show();
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void dbkasir_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new kasirt().Show();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new historyT().Show();
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            Logout();
        }
    }
}
