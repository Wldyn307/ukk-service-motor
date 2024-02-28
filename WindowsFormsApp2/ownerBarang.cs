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
    public partial class ownerBarang : Form
    {
        public ownerBarang()
        {
            InitializeComponent();
        }
        Data p = new Data();    

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new ownerT().Show();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Owner().Show();
        }

        private void ownerBarang_Load(object sender, EventArgs e)
        {
            p.showData("select * from products", dataGridView1);
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

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            Logout();
        }
    }
}
