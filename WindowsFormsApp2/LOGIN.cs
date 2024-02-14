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
using MySql.Data.MySqlClient;

namespace WindowsFormsApp2
{
    public partial class LOGIN : Form
    {
        MySqlConnection conn = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=gatolin");
        Data p = new Data();
 
    
        void login()
        {

            try
            {
                conn.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter("SELECT * From users WHERE username='" + us.Text + "'AND password='" + pw.Text + "'", conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Data.role = dr["role"].ToString();
                        Data.id_user = dr["id"].ToString();

                        Console.WriteLine(Data.id_user);

                        
                        MessageBox.Show("Login sukses !", "informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       
                        if (Data.role == "admin")
                        {
                            p.command("insert into log (id_user , activity, created_at) VALUES ('" + Data.id_user + "', 'Loginn As Admin' , NOW())");
                            this.Hide();
                            new Adminbarang().Show();
                        }
                        else if (Data.role == "kasir")
                        {
                            p.command("insert into log (id_user , activity, created_at) VALUES ('" + Data.id_user + "', 'Login As Kasir' , NOW())");
                           
                            this.Hide();
                            new kasirt().Show();
                        }
                        else if (Data.role == "owner")
                        {
                            this.Hide();
                            new Owner().Show();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Akun Tidak Ditemukan");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        void pwchart()
        {
            if (checkBox1.Checked)
            {
                pw.UseSystemPasswordChar = false;
            }
            else
            {
                pw.UseSystemPasswordChar = true;
            }
        }


       
        public LOGIN()
        {
            InitializeComponent();
           
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextbox1_OnTextChange(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            
            login();  
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            pwchart();
        }

        private void pw_TextChanged(object sender, EventArgs e)
        {
            pwchart();
        }
    }
    }

