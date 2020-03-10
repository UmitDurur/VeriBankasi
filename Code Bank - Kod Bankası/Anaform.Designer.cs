namespace VeriBankasi
{
    partial class Anaform
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Anaform));
            this.button1 = new System.Windows.Forms.Button();
            this.context_ozellist = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.newDocument = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.passtool = new System.Windows.Forms.ToolStripMenuItem();
            this.removepass = new System.Windows.Forms.ToolStripMenuItem();
            this.rename = new System.Windows.Forms.ToolStripMenuItem();
            this.delete = new System.Windows.Forms.ToolStripMenuItem();
            this.bildiri = new System.Windows.Forms.ToolTip(this.components);
            this.tab_txteditor = new System.Windows.Forms.TabControl();
            this.cmb_syntax = new System.Windows.Forms.ComboBox();
            this.tmr_formbuyu = new System.Windows.Forms.Timer(this.components);
            this.pnl_genisle = new System.Windows.Forms.Panel();
            this.btn_setting = new System.Windows.Forms.PictureBox();
            this.btn_kaydet = new System.Windows.Forms.PictureBox();
            this.sts_durumcubugu = new System.Windows.Forms.StatusStrip();
            this.drmcbk_lbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.tmr_statusbar = new System.Windows.Forms.Timer(this.components);
            this.lbx_basliklar = new VeriBankasi.OzelListbox();
            this.pnl_yukleniyor = new VeriBankasi.TransparanPanel();
            this.pb_yukleniyor = new System.Windows.Forms.PictureBox();
            this.tlbl_yukleniyor = new VeriBankasi.TransparanLabel();
            this.formbildirim = new System.Windows.Forms.ToolTip(this.components);
            this.context_ozellist.SuspendLayout();
            this.pnl_genisle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_setting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_kaydet)).BeginInit();
            this.sts_durumcubugu.SuspendLayout();
            this.pnl_yukleniyor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_yukleniyor)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(39, 15);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // context_ozellist
            // 
            this.context_ozellist.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newFolder,
            this.newDocument,
            this.toolStripSeparator1,
            this.passtool,
            this.removepass,
            this.rename,
            this.delete});
            this.context_ozellist.Name = "contextMenuStrip1";
            this.context_ozellist.Size = new System.Drawing.Size(153, 142);
            this.context_ozellist.Closing += new System.Windows.Forms.ToolStripDropDownClosingEventHandler(this.context_ozellist_Closing);
            this.context_ozellist.Opening += new System.ComponentModel.CancelEventHandler(this.context_ozellist_Opening);
            // 
            // newFolder
            // 
            this.newFolder.Image = global::VeriBankasi.Properties.Resources.dosya_ekle;
            this.newFolder.Name = "newFolder";
            this.newFolder.Size = new System.Drawing.Size(152, 22);
            this.newFolder.Text = "newFolder";
            this.newFolder.Click += new System.EventHandler(this.klasörEkleToolStripMenuItem_Click);
            // 
            // newDocument
            // 
            this.newDocument.Image = global::VeriBankasi.Properties.Resources.belge_ekle;
            this.newDocument.Name = "newDocument";
            this.newDocument.Size = new System.Drawing.Size(152, 22);
            this.newDocument.Text = "newDocument";
            this.newDocument.Click += new System.EventHandler(this.belgeEkleToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // passtool
            // 
            this.passtool.Image = global::VeriBankasi.Properties.Resources.kilitkapali;
            this.passtool.Name = "passtool";
            this.passtool.Size = new System.Drawing.Size(152, 22);
            this.passtool.Text = "password";
            this.passtool.Click += new System.EventHandler(this.passtool_Click);
            // 
            // removepass
            // 
            this.removepass.Image = global::VeriBankasi.Properties.Resources.kilitacik;
            this.removepass.Name = "removepass";
            this.removepass.Size = new System.Drawing.Size(152, 22);
            this.removepass.Text = "removepass";
            this.removepass.Click += new System.EventHandler(this.removepass_Click);
            // 
            // rename
            // 
            this.rename.Name = "rename";
            this.rename.Size = new System.Drawing.Size(152, 22);
            this.rename.Text = "rename";
            this.rename.Click += new System.EventHandler(this.yenidenAdlandırToolStripMenuItem_Click);
            // 
            // delete
            // 
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(152, 22);
            this.delete.Text = "delete";
            this.delete.Click += new System.EventHandler(this.silToolStripMenuItem_Click);
            // 
            // tab_txteditor
            // 
            this.tab_txteditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tab_txteditor.Location = new System.Drawing.Point(10, 75);
            this.tab_txteditor.Name = "tab_txteditor";
            this.tab_txteditor.SelectedIndex = 0;
            this.tab_txteditor.Size = new System.Drawing.Size(571, 341);
            this.tab_txteditor.TabIndex = 3;
            this.tab_txteditor.SelectedIndexChanged += new System.EventHandler(this.tab_txteditor_SelectedIndexChanged);
            this.tab_txteditor.Click += new System.EventHandler(this.tab_txteditor_Click);
            // 
            // cmb_syntax
            // 
            this.cmb_syntax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmb_syntax.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_syntax.FormattingEnabled = true;
            this.cmb_syntax.Location = new System.Drawing.Point(507, 46);
            this.cmb_syntax.Name = "cmb_syntax";
            this.cmb_syntax.Size = new System.Drawing.Size(70, 21);
            this.cmb_syntax.TabIndex = 4;
            this.cmb_syntax.SelectedIndexChanged += new System.EventHandler(this.cmb_syntax_SelectedIndexChanged);
            // 
            // tmr_formbuyu
            // 
            this.tmr_formbuyu.Interval = 30;
            this.tmr_formbuyu.Tick += new System.EventHandler(this.tmr_formbuyu_Tick);
            // 
            // pnl_genisle
            // 
            this.pnl_genisle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_genisle.Controls.Add(this.btn_setting);
            this.pnl_genisle.Controls.Add(this.btn_kaydet);
            this.pnl_genisle.Controls.Add(this.cmb_syntax);
            this.pnl_genisle.Controls.Add(this.tab_txteditor);
            this.pnl_genisle.Location = new System.Drawing.Point(203, -1);
            this.pnl_genisle.Name = "pnl_genisle";
            this.pnl_genisle.Size = new System.Drawing.Size(588, 421);
            this.pnl_genisle.TabIndex = 5;
            this.pnl_genisle.Visible = false;
            // 
            // btn_setting
            // 
            this.btn_setting.BackgroundImage = global::VeriBankasi.Properties.Resources.setting;
            this.btn_setting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_setting.ErrorImage = global::VeriBankasi.Properties.Resources.setting;
            this.btn_setting.Location = new System.Drawing.Point(122, 44);
            this.btn_setting.Name = "btn_setting";
            this.btn_setting.Size = new System.Drawing.Size(25, 25);
            this.btn_setting.TabIndex = 6;
            this.btn_setting.TabStop = false;
            this.btn_setting.Tag = "";
            this.btn_setting.Click += new System.EventHandler(this.btn_setting_Click);
            // 
            // btn_kaydet
            // 
            this.btn_kaydet.BackColor = System.Drawing.Color.Transparent;
            this.btn_kaydet.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_kaydet.BackgroundImage")));
            this.btn_kaydet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_kaydet.Location = new System.Drawing.Point(91, 44);
            this.btn_kaydet.Name = "btn_kaydet";
            this.btn_kaydet.Size = new System.Drawing.Size(25, 25);
            this.btn_kaydet.TabIndex = 5;
            this.btn_kaydet.TabStop = false;
            this.btn_kaydet.Click += new System.EventHandler(this.btn_kaydet_Click);
            // 
            // sts_durumcubugu
            // 
            this.sts_durumcubugu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.drmcbk_lbl});
            this.sts_durumcubugu.Location = new System.Drawing.Point(0, 423);
            this.sts_durumcubugu.Name = "sts_durumcubugu";
            this.sts_durumcubugu.Size = new System.Drawing.Size(789, 22);
            this.sts_durumcubugu.TabIndex = 2;
            this.sts_durumcubugu.Text = "statusStrip1";
            // 
            // drmcbk_lbl
            // 
            this.drmcbk_lbl.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.drmcbk_lbl.Name = "drmcbk_lbl";
            this.drmcbk_lbl.Size = new System.Drawing.Size(0, 17);
            // 
            // tmr_statusbar
            // 
            this.tmr_statusbar.Interval = 3000;
            this.tmr_statusbar.Tick += new System.EventHandler(this.tmr_statusbar_Tick);
            // 
            // lbx_basliklar
            // 
            this.lbx_basliklar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbx_basliklar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbx_basliklar.ContextMenuStrip = this.context_ozellist;
            this.lbx_basliklar.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbx_basliklar.FormattingEnabled = true;
            this.lbx_basliklar.ItemHeight = 16;
            this.lbx_basliklar.Location = new System.Drawing.Point(12, 93);
            this.lbx_basliklar.Name = "lbx_basliklar";
            this.lbx_basliklar.Size = new System.Drawing.Size(185, 322);
            this.lbx_basliklar.TabIndex = 1;
            this.lbx_basliklar.DoubleClick += new System.EventHandler(this.OzelListbox1_DoubleClick);
            this.lbx_basliklar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OzelListbox1_MouseMove);
            this.lbx_basliklar.Resize += new System.EventHandler(this.lbx_basliklar_Resize);
            // 
            // pnl_yukleniyor
            // 
            this.pnl_yukleniyor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_yukleniyor.BackColor = System.Drawing.Color.Gray;
            this.pnl_yukleniyor.Controls.Add(this.pb_yukleniyor);
            this.pnl_yukleniyor.Controls.Add(this.tlbl_yukleniyor);
            this.pnl_yukleniyor.Location = new System.Drawing.Point(0, 0);
            this.pnl_yukleniyor.Name = "pnl_yukleniyor";
            this.pnl_yukleniyor.Opacity = 50;
            this.pnl_yukleniyor.Size = new System.Drawing.Size(789, 445);
            this.pnl_yukleniyor.TabIndex = 6;
            this.pnl_yukleniyor.Visible = false;
            // 
            // pb_yukleniyor
            // 
            this.pb_yukleniyor.InitialImage = null;
            this.pb_yukleniyor.Location = new System.Drawing.Point(338, 153);
            this.pb_yukleniyor.Name = "pb_yukleniyor";
            this.pb_yukleniyor.Size = new System.Drawing.Size(60, 64);
            this.pb_yukleniyor.TabIndex = 0;
            this.pb_yukleniyor.TabStop = false;
            // 
            // tlbl_yukleniyor
            // 
            this.tlbl_yukleniyor.AutoSize = true;
            this.tlbl_yukleniyor.BackColor = System.Drawing.Color.Transparent;
            this.tlbl_yukleniyor.Location = new System.Drawing.Point(32, 43);
            this.tlbl_yukleniyor.Name = "tlbl_yukleniyor";
            this.tlbl_yukleniyor.Opacity = 100;
            this.tlbl_yukleniyor.Size = new System.Drawing.Size(129, 20);
            this.tlbl_yukleniyor.TabIndex = 1;
            this.tlbl_yukleniyor.Text = "transparanLabel1";
            this.tlbl_yukleniyor.Textmessage = "İşlem yapılıyor...";
            this.tlbl_yukleniyor.X = 20;
            this.tlbl_yukleniyor.Y = 129;
            // 
            // Anaform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 445);
            this.Controls.Add(this.sts_durumcubugu);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lbx_basliklar);
            this.Controls.Add(this.pnl_genisle);
            this.Controls.Add(this.pnl_yukleniyor);
            this.Name = "Anaform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DRR Kod Bankası";
            this.Activated += new System.EventHandler(this.Anaform_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Anaform_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Anaform_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.context_ozellist.ResumeLayout(false);
            this.pnl_genisle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btn_setting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_kaydet)).EndInit();
            this.sts_durumcubugu.ResumeLayout(false);
            this.sts_durumcubugu.PerformLayout();
            this.pnl_yukleniyor.ResumeLayout(false);
            this.pnl_yukleniyor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_yukleniyor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ContextMenuStrip context_ozellist;
        private System.Windows.Forms.ToolStripMenuItem newFolder;
        private System.Windows.Forms.ToolTip bildiri;
        private System.Windows.Forms.ToolStripMenuItem newDocument;
        private System.Windows.Forms.TabControl tab_txteditor;
        private System.Windows.Forms.ComboBox cmb_syntax;
        private System.Windows.Forms.Timer tmr_formbuyu;
        private System.Windows.Forms.Panel pnl_genisle;
        private TransparanPanel pnl_yukleniyor;
        private System.Windows.Forms.PictureBox pb_yukleniyor;
        private System.Windows.Forms.PictureBox btn_kaydet;
        private System.Windows.Forms.StatusStrip sts_durumcubugu;
        private TransparanLabel tlbl_yukleniyor;
        public System.Windows.Forms.Timer tmr_statusbar;
        private System.Windows.Forms.ToolStripStatusLabel drmcbk_lbl;
        private System.Windows.Forms.ToolStripMenuItem rename;
        private OzelListbox lbx_basliklar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem delete;
        private System.Windows.Forms.PictureBox btn_setting;
        private System.Windows.Forms.ToolStripMenuItem passtool;
        private System.Windows.Forms.ToolStripMenuItem removepass;
        private System.Windows.Forms.ToolTip formbildirim;
    }
}

