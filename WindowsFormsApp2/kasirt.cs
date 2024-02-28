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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using System.Web.UI.WebControls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Reflection.Emit;
using Mysqlx.Expect;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


namespace WindowsFormsApp2
{
    public partial class kasirt : Form
    {
       MySqlCommand cmd = new MySqlCommand();
        MySqlConnection conn = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=gatolin");
        Data p = new Data();
        DateTime time = DateTime.Now;
        int i = 0;
        public static int IdTr = 1;
        string nama = "";
        string id = "";
        
        private string nama1;

        public kasirt()
        {
            InitializeComponent();
           
        }

        void numrandom()
        {
            Random random = new Random();
            int randomValue = random.Next(10000, 99999); 
            txtnounik.Text = randomValue.ToString();
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
                cbidnm.Items.Add(id + "-" + nama);
            }

           
            reader.Close();
            conn.Close();
        }

        string GetHarga()
        {
            MySqlConnection conn = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=gatolin");
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select harga_produk from products where id like '" + cbidnm.Text.Split('-')[0].Trim() + "%'", conn);
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

       



     
       void getdg()
        {
            lblTotal.Text = "0";
            for(int i = 0; i < dgtransaksi.Rows.Count; i++)
            {
                lblTotal.Text = Convert.ToString(double.Parse(lblTotal.Text) + double.Parse(dgtransaksi.Rows[i].Cells[6].Value.ToString()));
            }
        }

        void struk()
        {
            p.command("insert into log (id_user , activity, created_at) VALUES ('" + Data.id_user + "', 'kasir melakukan transaksi' , NOW())");
            laporan frm = new laporan();
            CrystalReport1 cr1 = new CrystalReport1();
            DataGridViewRow dr = this.dgtransaksi.Rows[0];

            TextObject namap = (TextObject)cr1.ReportDefinition.Sections["Section3"].ReportObjects["namap"];
            namap.Text = dr.Cells[1].Value.ToString();
            TextObject notransaksi = (TextObject)cr1.ReportDefinition.Sections["Section3"].ReportObjects["Notransaksi"];
            notransaksi.Text = dr.Cells[5].Value.ToString();
            TextObject idproduk = (TextObject)cr1.ReportDefinition.Sections["Section3"].ReportObjects["idproduk"];
            idproduk.Text = dr.Cells[2].Value.ToString();
            TextObject namaproduk = (TextObject)cr1.ReportDefinition.Sections["Section3"].ReportObjects["namaproduk"];
            namaproduk.Text = dr.Cells[3].Value.ToString();
            TextObject hargaproduk = (TextObject)cr1.ReportDefinition.Sections["Section3"].ReportObjects["harga"];
            hargaproduk.Text = dr.Cells[4].Value.ToString();
            TextObject uangb = (TextObject)cr1.ReportDefinition.Sections["Section3"].ReportObjects["uangbayar"];
            uangb.Text = txtuangb.Text;
            TextObject kembalian = (TextObject)cr1.ReportDefinition.Sections["Section3"].ReportObjects["kembalian"];
            kembalian.Text = txtkembali.Text;


            frm.crystalReportViewer1.ReportSource = cr1;
            frm.Show();
        }

        void addTroli()
        {
            string namap = txtnamap.Text;
            string id = cbidnm.Text.Split('-')[0].Trim();
            string namaproduk = cbidnm.Text.Split('-')[1].Trim();
            string harga = txthargap.Text;
           
           
            string nonik = txtnounik.Text;
            

            dgtransaksi.Rows.Add();
            dgtransaksi.Rows.Add();
            dgtransaksi.Rows[i].Cells[0].Value = IdTr;
            dgtransaksi.Rows[i].Cells[1].Value = namap;
            dgtransaksi.Rows[i].Cells[2].Value = id;
            dgtransaksi.Rows[i].Cells[3].Value = namaproduk;
            dgtransaksi.Rows[i].Cells[4].Value = harga;
           
           
            dgtransaksi.Rows[i].Cells[5].Value = nonik;
            i++;
            IdTr++;
            
        }

          void clear()
        {

            txtnamap.Text = string.Empty;
            cbidnm.Text = string.Empty;
            txthargap.Text = string.Empty;
           
            txtuangb.Text = string.Empty;
            txtkembali.Text = string.Empty;
           
            lblTotal.Text = string.Empty;


            dgtransaksi.Rows.Clear();
            p.showData("Select * from products", dataGridView1);
        }

        void clear1()
        {
            
            txtnamap.Text = string.Empty;
            cbidnm.Text = string.Empty;
            txthargap.Text = string.Empty;
           
            txtuangb.Text = string.Empty;
            txtkembali.Text = string.Empty;
           
            
            

            p.showData("Select * from products", dataGridView1);
        }

        private void UpdateKembalian(object sender, EventArgs e)
        {
      
            if (decimal.TryParse(lblTotal.Text, out decimal totalBelanja) && decimal.TryParse(txtuangb.Text, out decimal uangBayar))
            {
              
                decimal kembalian = uangBayar- totalBelanja;

                
                txtkembali.Text = kembalian.ToString("0.##"); 
            }
            else
            {
            
                txtkembali.Text = "";
            }
        }

      

      
        void kurangistokk()
        {
            DataGridViewRow dr = this.dgtransaksi.Rows[0];
            string productId = dr.Cells[2].Value.ToString();
            {
                // Kurangi stok berdasarkan nilai qty
                string updateQuery = "UPDATE products SET stok = stok - 1 WHERE id = @productId";

                using (MySqlConnection conn = new MySqlConnection("server=localhost;port=3306;username=root;password=;database=gatolin"))
                {
                    conn.Open();
                    using (MySqlCommand command = new MySqlCommand(updateQuery, conn))
                    {
                        command.Parameters.AddWithValue("@productId", productId);

                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        private decimal HitungTotalBelanja(int qty, decimal hargaPerBarang)
        {
            return qty * hargaPerBarang;
        }

       

        private void kasir_Load(object sender, EventArgs e)
        {
            txtnounik.Enabled = false;
            numrandom();
            getBarang();
          

           lblTotal.TextChanged += UpdateKembalian;
            txtuangb.TextChanged += UpdateKembalian;

            btnbayar.Click -= btnedit_Click;
            btnbayar.Click += btnedit_Click;

            clear1();


          p.showData("Select * from products", dataGridView1);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtidp_TextChanged(object sender, EventArgs e)
        {
        
        }

        private void bayar_Click(object sender, EventArgs e)
        {
           
        }

        private void logout_Click(object sender, EventArgs e)
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

        private void print_Click(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dr = this.dataGridView1.Rows[e.RowIndex];

            txthargap.Text = dr.Cells[2].Value.ToString();
            string kodeproduk = dr.Cells[0].Value.ToString();
          
            string namaProduk = dr.Cells[1].Value.ToString();

           
            string combined = kodeproduk + "-" + namaProduk;

           cbidnm.Text = combined;

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
             
           
        }

        private void lblTotal_Click(object sender, EventArgs e)
        {
            
        }
        private void UpdateTotalHarga()
        {
            decimal totalHarga = 0;

            foreach (DataGridViewRow row in dgtransaksi.Rows)
            {
                if (row.Cells[4].Value != null && row.Cells[4].Value != DBNull.Value)
                {
                    totalHarga += Convert.ToDecimal(row.Cells[4].Value);
                }
            }

            lblTotal.Text = totalHarga.ToString("0.##");
        }


        private void dgtransaksi_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgtransaksi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private int GetStok(string productId)
        {
            int stok = 0;

            using (MySqlConnection conn = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=gatolin"))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT stok FROM products WHERE id = @productId", conn);
                cmd.Parameters.AddWithValue("@productId", productId);
                object result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    stok = Convert.ToInt32(result);
                }
            }

            return stok;
        }

        private int GetProductStock(string productID)
        {
            int stock = 0;
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=gatolin";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT stok FROM Products WHERE id = @ProductID";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProductID", productID);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        stock = Convert.ToInt32(result);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

            return stock;
        }

        private void cbidnm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbidnm.SelectedItem == null)
            {
               
                cbidnm.SelectedIndex = -1;
                return;
            }

            string selectedProductID = cbidnm.SelectedItem.ToString().Split('-')[0].Trim();

            int productStock = GetProductStock(selectedProductID);

            if (productStock <= 0)
            {
               
                MessageBox.Show("Stok habis! Silakan pilih item lain.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbidnm.SelectedIndex = -1;
                txthargap.Text = string.Empty;

            }
            else
            {
                txthargap.Text = GetHarga();
            }
        }

        private void txthargap_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btnhapusdg_Click(object sender, EventArgs e)
        {
            dgtransaksi.Rows.Clear();
            i = 0;
            IdTr = 1;
            lblTotal.Text = "";
            numrandom();
        }

        private void btntambah_Click(object sender, EventArgs e)
        {
            DialogResult d;
            d = MessageBox.Show("Apakah anda yakin?", "Pertanyaan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (d == DialogResult.Yes)
            {
                // Cek apakah nama pelanggan sudah diisi
                if (string.IsNullOrEmpty(txtnamap.Text))
                {
                    MessageBox.Show("Mohon isi nama pelanggan.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Hentikan proses karena nama pelanggan belum diisi
                }

                // Cek apakah nama barang sudah dipilih
                if (cbidnm.SelectedIndex == -1)
                {
                    MessageBox.Show("Mohon pilih produk.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Hentikan proses karena produk belum dipilih
                }

                addTroli();
                clear1();
                UpdateTotalHarga();
            }

        }

        void rdm()
        {
            i = 0;
            IdTr = 1;
            lblTotal.Text = "";
            numrandom();
        }

        private void btnhapus_Click(object sender, EventArgs e)
        {
            dgtransaksi.Rows.Clear();
            rdm();
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            foreach (DataGridViewRow dr in dgtransaksi.Rows)
            {
                if (dr.Cells[0].Value != null || dr.Cells[1].Value != null || dr.Cells[2].Value != null || dr.Cells[3].Value != null || dr.Cells[4].Value != null || dr.Cells[5].Value != null || dr.Cells[6].Value != null)
                {
                    kurangistokk();
                    string id_produk = dr.Cells[2].Value.ToString();
                    string nama_pelanggan = dr.Cells[1].Value.ToString();
                    string totalharga = Convert.ToString(lblTotal.Text);
                    string nomorUnik = dr.Cells[5].Value.ToString();
                    string uang_bayar = Convert.ToString(txtuangb.Text);
                    string uang_kembali = Convert.ToString(txtkembali.Text);
                  

                    struk();

                    p.command("INSERT INTO transaksi (id_produk, nama_pelanggan, nomor_unik, uang_bayar, uang_kembali ,total_harga, created_at, updated_at) values ('" + id_produk + "' , '" + nama_pelanggan + "', '" + nomorUnik + "', '" + uang_bayar + "', '" + uang_kembali + "', '" + totalharga + "', NOW(), NOW())");
                    p.command("insert into log (id_user , activity, created_at) VALUES ('" + Data.id_user + "', 'kasir melakukan transaksi' , NOW())");
                    clear();
                    rdm();
                }
            }

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new historyT().Show();
        }

        private void txtuangb_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            Logout();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            this.Hide();
            new dbkasir().Show();
        }
    }
}
