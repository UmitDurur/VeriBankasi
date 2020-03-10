namespace VeriBankasi
{
    partial class nesne_ekle
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
            this.btn_blgekle = new System.Windows.Forms.Button();
            this.btn_iptal = new System.Windows.Forms.Button();
            this.pnl_blgekle = new System.Windows.Forms.Panel();
            this.pnl_dsyekle = new System.Windows.Forms.Panel();
            this.lbl_icerik = new System.Windows.Forms.Label();
            this.txt_icerik = new System.Windows.Forms.TextBox();
            this.pnl_header = new System.Windows.Forms.Panel();
            this.cmb_ktgri = new System.Windows.Forms.ComboBox();
            this.lbl_baslik = new System.Windows.Forms.Label();
            this.txt_baslik = new System.Windows.Forms.TextBox();
            this.pnl_footer = new System.Windows.Forms.Panel();
            this.pnl_dsyekle.SuspendLayout();
            this.pnl_header.SuspendLayout();
            this.pnl_footer.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_blgekle
            // 
            this.btn_blgekle.Location = new System.Drawing.Point(12, 3);
            this.btn_blgekle.Name = "btn_blgekle";
            this.btn_blgekle.Size = new System.Drawing.Size(75, 23);
            this.btn_blgekle.TabIndex = 0;
            this.btn_blgekle.Text = "Ekle";
            this.btn_blgekle.UseVisualStyleBackColor = true;
            this.btn_blgekle.Click += new System.EventHandler(this.btn_klsrekle_Click);
            // 
            // btn_iptal
            // 
            this.btn_iptal.Location = new System.Drawing.Point(100, 3);
            this.btn_iptal.Name = "btn_iptal";
            this.btn_iptal.Size = new System.Drawing.Size(75, 23);
            this.btn_iptal.TabIndex = 1;
            this.btn_iptal.Text = "İptal";
            this.btn_iptal.UseVisualStyleBackColor = true;
            this.btn_iptal.Click += new System.EventHandler(this.button2_Click);
            // 
            // pnl_blgekle
            // 
            this.pnl_blgekle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_blgekle.Location = new System.Drawing.Point(0, 65);
            this.pnl_blgekle.Name = "pnl_blgekle";
            this.pnl_blgekle.Size = new System.Drawing.Size(185, 34);
            this.pnl_blgekle.TabIndex = 5;
            this.pnl_blgekle.Visible = false;
            // 
            // pnl_dsyekle
            // 
            this.pnl_dsyekle.Controls.Add(this.lbl_icerik);
            this.pnl_dsyekle.Controls.Add(this.txt_icerik);
            this.pnl_dsyekle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_dsyekle.Location = new System.Drawing.Point(0, 65);
            this.pnl_dsyekle.Name = "pnl_dsyekle";
            this.pnl_dsyekle.Size = new System.Drawing.Size(185, 34);
            this.pnl_dsyekle.TabIndex = 5;
            this.pnl_dsyekle.Visible = false;
            // 
            // lbl_icerik
            // 
            this.lbl_icerik.AutoSize = true;
            this.lbl_icerik.Location = new System.Drawing.Point(9, 64);
            this.lbl_icerik.Name = "lbl_icerik";
            this.lbl_icerik.Size = new System.Drawing.Size(50, 13);
            this.lbl_icerik.TabIndex = 8;
            this.lbl_icerik.Text = "Açıklama";
            // 
            // txt_icerik
            // 
            this.txt_icerik.Location = new System.Drawing.Point(60, 61);
            this.txt_icerik.Name = "txt_icerik";
            this.txt_icerik.Size = new System.Drawing.Size(115, 20);
            this.txt_icerik.TabIndex = 8;
            // 
            // pnl_header
            // 
            this.pnl_header.Controls.Add(this.cmb_ktgri);
            this.pnl_header.Controls.Add(this.lbl_baslik);
            this.pnl_header.Controls.Add(this.txt_baslik);
            this.pnl_header.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_header.Location = new System.Drawing.Point(0, 0);
            this.pnl_header.Name = "pnl_header";
            this.pnl_header.Size = new System.Drawing.Size(185, 65);
            this.pnl_header.TabIndex = 5;
            // 
            // cmb_ktgri
            // 
            this.cmb_ktgri.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_ktgri.FormattingEnabled = true;
            this.cmb_ktgri.Location = new System.Drawing.Point(12, 11);
            this.cmb_ktgri.Name = "cmb_ktgri";
            this.cmb_ktgri.Size = new System.Drawing.Size(163, 21);
            this.cmb_ktgri.TabIndex = 5;
            this.cmb_ktgri.Tag = "";
            this.cmb_ktgri.SelectedIndexChanged += new System.EventHandler(this.cmb_ktgri_SelectedIndexChanged);
            // 
            // lbl_baslik
            // 
            this.lbl_baslik.AutoSize = true;
            this.lbl_baslik.Location = new System.Drawing.Point(9, 41);
            this.lbl_baslik.Name = "lbl_baslik";
            this.lbl_baslik.Size = new System.Drawing.Size(35, 13);
            this.lbl_baslik.TabIndex = 6;
            this.lbl_baslik.Text = "Başlık";
            // 
            // txt_baslik
            // 
            this.txt_baslik.Location = new System.Drawing.Point(60, 38);
            this.txt_baslik.Name = "txt_baslik";
            this.txt_baslik.Size = new System.Drawing.Size(115, 20);
            this.txt_baslik.TabIndex = 7;
            // 
            // pnl_footer
            // 
            this.pnl_footer.Controls.Add(this.btn_iptal);
            this.pnl_footer.Controls.Add(this.btn_blgekle);
            this.pnl_footer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_footer.Location = new System.Drawing.Point(0, 64);
            this.pnl_footer.Name = "pnl_footer";
            this.pnl_footer.Size = new System.Drawing.Size(185, 35);
            this.pnl_footer.TabIndex = 8;
            // 
            // nesne_ekle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(185, 99);
            this.Controls.Add(this.pnl_footer);
            this.Controls.Add(this.pnl_blgekle);
            this.Controls.Add(this.pnl_dsyekle);
            this.Controls.Add(this.pnl_header);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "nesne_ekle";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "nesne_ekle";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.nesne_ekle_FormClosing);
            this.Load += new System.EventHandler(this.nesne_ekle_Load);
            this.pnl_dsyekle.ResumeLayout(false);
            this.pnl_dsyekle.PerformLayout();
            this.pnl_header.ResumeLayout(false);
            this.pnl_header.PerformLayout();
            this.pnl_footer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_blgekle;
        private System.Windows.Forms.Button btn_iptal;
        private System.Windows.Forms.Panel pnl_blgekle;
        private System.Windows.Forms.Panel pnl_dsyekle;
        private System.Windows.Forms.Panel pnl_header;
        private System.Windows.Forms.ComboBox cmb_ktgri;
        private System.Windows.Forms.Label lbl_baslik;
        private System.Windows.Forms.TextBox txt_baslik;
        private System.Windows.Forms.Panel pnl_footer;
        private System.Windows.Forms.TextBox txt_icerik;
        private System.Windows.Forms.Label lbl_icerik;
    }
}