namespace VeriBankasi
{
    partial class Ayarlar
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
            this.cmb_languages = new System.Windows.Forms.ComboBox();
            this.btn_iptal = new System.Windows.Forms.Button();
            this.lbl_lang = new System.Windows.Forms.Label();
            this.btn_kydt = new System.Windows.Forms.Button();
            this.chb_pwhatirla = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cmb_languages
            // 
            this.cmb_languages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_languages.FormattingEnabled = true;
            this.cmb_languages.Items.AddRange(new object[] {
            "Türkçe",
            "English",
            "French"});
            this.cmb_languages.Location = new System.Drawing.Point(82, 12);
            this.cmb_languages.Name = "cmb_languages";
            this.cmb_languages.Size = new System.Drawing.Size(134, 21);
            this.cmb_languages.TabIndex = 0;
            this.cmb_languages.SelectedIndexChanged += new System.EventHandler(this.cmb_languages_SelectedIndexChanged);
            // 
            // btn_iptal
            // 
            this.btn_iptal.Location = new System.Drawing.Point(141, 74);
            this.btn_iptal.Name = "btn_iptal";
            this.btn_iptal.Size = new System.Drawing.Size(75, 23);
            this.btn_iptal.TabIndex = 1;
            this.btn_iptal.Text = "Kapat";
            this.btn_iptal.UseVisualStyleBackColor = true;
            this.btn_iptal.Click += new System.EventHandler(this.btn_iptal_Click);
            // 
            // lbl_lang
            // 
            this.lbl_lang.AutoSize = true;
            this.lbl_lang.Location = new System.Drawing.Point(12, 15);
            this.lbl_lang.Name = "lbl_lang";
            this.lbl_lang.Size = new System.Drawing.Size(35, 13);
            this.lbl_lang.TabIndex = 2;
            this.lbl_lang.Text = "label1";
            // 
            // btn_kydt
            // 
            this.btn_kydt.Location = new System.Drawing.Point(60, 74);
            this.btn_kydt.Name = "btn_kydt";
            this.btn_kydt.Size = new System.Drawing.Size(75, 23);
            this.btn_kydt.TabIndex = 3;
            this.btn_kydt.Text = "button1";
            this.btn_kydt.UseVisualStyleBackColor = true;
            this.btn_kydt.Click += new System.EventHandler(this.btn_kydt_Click);
            // 
            // chb_pwhatirla
            // 
            this.chb_pwhatirla.AutoSize = true;
            this.chb_pwhatirla.Location = new System.Drawing.Point(12, 39);
            this.chb_pwhatirla.Name = "chb_pwhatirla";
            this.chb_pwhatirla.Size = new System.Drawing.Size(80, 17);
            this.chb_pwhatirla.TabIndex = 4;
            this.chb_pwhatirla.Text = "checkBox1";
            this.chb_pwhatirla.UseVisualStyleBackColor = true;
            // 
            // Ayarlar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(228, 111);
            this.Controls.Add(this.chb_pwhatirla);
            this.Controls.Add(this.btn_kydt);
            this.Controls.Add(this.lbl_lang);
            this.Controls.Add(this.btn_iptal);
            this.Controls.Add(this.cmb_languages);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Ayarlar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ayarlar";
            this.Load += new System.EventHandler(this.Ayarlar_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmb_languages;
        private System.Windows.Forms.Button btn_iptal;
        private System.Windows.Forms.Label lbl_lang;
        private System.Windows.Forms.Button btn_kydt;
        private System.Windows.Forms.CheckBox chb_pwhatirla;
    }
}