using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApp2
{  
    internal class Data
    {
        public static string role;
        MySqlConnection conn = new MySqlConnection("server=localhost;port=3306;username=root;password=;database=gatolin");
        private Form activateForm;
        public static string username = "";
        public static string id_user = "";

          
        

        public void openChildForm(Form childForm, Panel panel, object btnSender)
        {
            if (activateForm != null)
            {
                activateForm.Close();
            }

            activateForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            panel.Controls.Add(childForm);
            panel.Tag = childForm;

            childForm.BringToFront();
            childForm.Show();

        }
        


        public void command(string query)
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
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

      

        public void showData(string query, DataGridView dg)
        {
            try
            {
                conn.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();

                sda.Fill(dt);
                dg.DataSource = dt;
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

       
        
        public void disable(KeyPressEventArgs e, object sender)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        internal static void getDatarole(object cb_Role)
        {
            throw new NotImplementedException();
        }
    }
}
