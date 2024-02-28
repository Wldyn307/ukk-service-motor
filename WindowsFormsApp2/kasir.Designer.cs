namespace WindowsFormsApp2
{
    partial class kasir
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgtransaksi = new System.Windows.Forms.DataGridView();
            this.logout = new System.Windows.Forms.Button();
            this.txtnamap = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txthargap = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txttotal = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtuangb = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtkembali = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tambahdg = new System.Windows.Forms.Button();
            this.txtqty = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.print = new System.Windows.Forms.Button();
            this.cbnamak = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.txtnomorunik = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.button1 = new System.Windows.Forms.Button();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgtransaksi)).BeginInit();
            this.SuspendLayout();
            // 
            // dgtransaksi
            // 
            this.dgtransaksi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgtransaksi.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column8,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7});
            this.dgtransaksi.Location = new System.Drawing.Point(12, 256);
            this.dgtransaksi.Name = "dgtransaksi";
            this.dgtransaksi.Size = new System.Drawing.Size(767, 168);
            this.dgtransaksi.TabIndex = 14;
            this.dgtransaksi.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // logout
            // 
            this.logout.Location = new System.Drawing.Point(23, 456);
            this.logout.Name = "logout";
            this.logout.Size = new System.Drawing.Size(75, 23);
            this.logout.TabIndex = 18;
            this.logout.Text = "Logout";
            this.logout.UseVisualStyleBackColor = true;
            this.logout.Click += new System.EventHandler(this.logout_Click);
            // 
            // txtnamap
            // 
            this.txtnamap.Location = new System.Drawing.Point(126, 127);
            this.txtnamap.Name = "txtnamap";
            this.txtnamap.Size = new System.Drawing.Size(169, 20);
            this.txtnamap.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(123, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Nama pelanggan";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(123, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "nama produk";
            // 
            // txthargap
            // 
            this.txthargap.Location = new System.Drawing.Point(333, 123);
            this.txthargap.Name = "txthargap";
            this.txthargap.Size = new System.Drawing.Size(169, 20);
            this.txthargap.TabIndex = 6;
            this.txthargap.TextChanged += new System.EventHandler(this.txthargap_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(330, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Harga produk";
            // 
            // txttotal
            // 
            this.txttotal.Location = new System.Drawing.Point(333, 227);
            this.txttotal.Name = "txttotal";
            this.txttotal.Size = new System.Drawing.Size(169, 20);
            this.txttotal.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(330, 211);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Total";
            // 
            // txtuangb
            // 
            this.txtuangb.Location = new System.Drawing.Point(689, 477);
            this.txtuangb.Name = "txtuangb";
            this.txtuangb.Size = new System.Drawing.Size(83, 20);
            this.txtuangb.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(621, 480);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Uang bayar";
            // 
            // txtkembali
            // 
            this.txtkembali.Location = new System.Drawing.Point(689, 513);
            this.txtkembali.Name = "txtkembali";
            this.txtkembali.Size = new System.Drawing.Size(83, 20);
            this.txtkembali.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(640, 520);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "kembali";
            // 
            // tambahdg
            // 
            this.tambahdg.Location = new System.Drawing.Point(136, 227);
            this.tambahdg.Name = "tambahdg";
            this.tambahdg.Size = new System.Drawing.Size(75, 23);
            this.tambahdg.TabIndex = 15;
            this.tambahdg.Text = "Tambah";
            this.tambahdg.UseVisualStyleBackColor = true;
            this.tambahdg.Click += new System.EventHandler(this.bayar_Click);
            // 
            // txtqty
            // 
            this.txtqty.Location = new System.Drawing.Point(333, 177);
            this.txtqty.Name = "txtqty";
            this.txtqty.Size = new System.Drawing.Size(169, 20);
            this.txtqty.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(330, 161);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(21, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "qty";
            // 
            // print
            // 
            this.print.Location = new System.Drawing.Point(239, 227);
            this.print.Name = "print";
            this.print.Size = new System.Drawing.Size(75, 23);
            this.print.TabIndex = 19;
            this.print.Text = "print";
            this.print.UseVisualStyleBackColor = true;
            this.print.Click += new System.EventHandler(this.print_Click);
            // 
            // cbnamak
            // 
            this.cbnamak.FormattingEnabled = true;
            this.cbnamak.Location = new System.Drawing.Point(126, 166);
            this.cbnamak.Name = "cbnamak";
            this.cbnamak.Size = new System.Drawing.Size(169, 21);
            this.cbnamak.TabIndex = 20;
            this.cbnamak.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(615, 445);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Total belanja";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(689, 445);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(16, 13);
            this.lblTotal.TabIndex = 22;
            this.lblTotal.Text = "rp";
            // 
            // txtnomorunik
            // 
            this.txtnomorunik.Location = new System.Drawing.Point(126, 88);
            this.txtnomorunik.Name = "txtnomorunik";
            this.txtnomorunik.Size = new System.Drawing.Size(100, 20);
            this.txtnomorunik.TabIndex = 23;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(123, 72);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "nomor unik";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(526, 224);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 25;
            this.button1.Text = "Bayar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "id";
            this.Column1.Name = "Column1";
            // 
            // Column8
            // 
            this.Column8.HeaderText = "nomor unik";
            this.Column8.Name = "Column8";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "nama pelanggan";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "id produk";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "nama produk";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "harga satuan";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "jumlah barang";
            this.Column6.Name = "Column6";
            // 
            // Column7
            // 
            this.Column7.HeaderText = "total";
            this.Column7.Name = "Column7";
            // 
            // kasir
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 578);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtnomorunik);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbnamak);
            this.Controls.Add(this.print);
            this.Controls.Add(this.logout);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtqty);
            this.Controls.Add(this.tambahdg);
            this.Controls.Add(this.dgtransaksi);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtkembali);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtuangb);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txttotal);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txthargap);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtnamap);
            this.Name = "kasir";
            this.Text = "kasir";
            this.Load += new System.EventHandler(this.kasir_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgtransaksi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgtransaksi;
        private System.Windows.Forms.Button logout;
        private System.Windows.Forms.TextBox txtnamap;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txthargap;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txttotal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtuangb;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtkembali;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button tambahdg;
        private System.Windows.Forms.TextBox txtqty;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button print;
        private System.Windows.Forms.ComboBox cbnamak;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TextBox txtnomorunik;
        private System.Windows.Forms.Label label9;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
    }
}