using ICSharpCode.AvalonEdit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Xml;
using System.Reflection;
using System.Resources;
using System.Globalization;

namespace VeriBankasi
{
    public partial class Anaform : Form
    {
        public Anaform()
        {
            InitializeComponent();
        }
        CultureInfo ci;
        void language()
        {
            ci = new CultureInfo(options.Default.Language);
            Assembly a = Assembly.Load("VeriBank");
            ResourceManager rm = new ResourceManager("VeriBankasi.Lang.langres", a);
            for (int i = 0; i < context_ozellist.Items.Count; i++)
            {
                context_ozellist.Items[i].Text = rm.GetString(context_ozellist.Items[i].Name, ci);
            }
            formbildirim.SetToolTip(btn_setting, rm.GetString("setting", ci));
            formbildirim.SetToolTip(btn_kaydet, rm.GetString("save", ci));
            this.Text = rm.GetString("progname", ci) + ' ' + Properties.Settings.Default.VersionInfo;
            tlbl_yukleniyor.Textmessage = rm.GetString("loading", ci);
        }
        System.Drawing.Point farekonum;
        SQLiteConnection baglan = new SQLiteConnection("Data Source=data.dll; Version=3;");
        SQLiteCommand kmt;
        SQLiteDataReader dr;
        OzelListbox.Nesne listnesne;
        public List<dizi_nesne> dizi = new List<dizi_nesne>();

        public TextEditor textEditor;
        int slctd = 0;
        bool formacik = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            language();
            pnl_yukleniyor.Parent = this;
            this.Controls.SetChildIndex(pnl_yukleniyor, 0);
            this.Size = new System.Drawing.Size(lbx_basliklar.Location.X + lbx_basliklar.Size.Width + 30, lbx_basliklar.Location.Y + lbx_basliklar.Height + 7);
            this.CenterToScreen();
            lbx_basliklar.Anchor = AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom;
            pb_yukleniyor.Image = Properties.Resources.yukleniyor;
            yukleniyor();
            lbx_basliklar.HorizontalScrollbar = false;
            //diziguncelle();
            listdoldur(0);
            syn_kontrol();
            lbx_basliklar.ColumnWidth = 150;
            yukleniyor("");
        }

        #region Yükleniyor paneli
        void yukleniyor()
        {

            pnl_yukleniyor.Visible = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            tlbl_yukleniyor.Parent = pnl_yukleniyor;
            this.Controls.SetChildIndex(pnl_yukleniyor, 0);
            tlbl_yukleniyor.BackColor = Color.Transparent;
            tlbl_yukleniyor.Location = new System.Drawing.Point((pnl_yukleniyor.Width / 2) - tlbl_yukleniyor.Width / 2, ((pnl_yukleniyor.Height / 2) - 24) - tlbl_yukleniyor.Height / 2);
            pb_yukleniyor.Location = new System.Drawing.Point((pnl_yukleniyor.Width / 2) - pb_yukleniyor.Width / 2, ((pnl_yukleniyor.Height / 2) + 24) - pb_yukleniyor.Height / 2);
        }
        void yukleniyor(string a)
        {
            try
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.MaximizeBox = true;
                pnl_yukleniyor.Hide();
            }
            catch { }
        }

        #endregion

        #region Syntax kontrolleri
        void syn_kontrol()
        {
            //Dosyadaki tüm nesneler vtde var mı ?
            string[] klasorler = new string[10];
            klasorler = Directory.GetFiles("Syntax/");
            foreach (string klasor in klasorler)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(klasor);
                if (directoryInfo.Extension == ".xshd")
                {
                    string kisaca = directoryInfo.Name.Substring(0, directoryInfo.Name.IndexOf(".xshd"));
                    if (baglan.State == ConnectionState.Closed)
                        baglan.Open();
                    kmt = new SQLiteCommand("select syn_ad from syntax where syn_konum=@synyer", baglan);
                    kmt.Parameters.AddWithValue("@synyer", kisaca);
                    dr = kmt.ExecuteReader();
                    if (!dr.Read())
                    {
                        syntax_ekle(kisaca);
                    }
                }
            }

            //vtdeki tüm nesneler dosyada var mı?
            kmt = new SQLiteCommand("select syn_konum,syn_ad from syntaxs", baglan);
            dr = kmt.ExecuteReader();
            while (dr.Read())
            {
                string a = @"Syntax\" + dr["syn_konum"].ToString() + ".xshd";
                if (!File.Exists(a) && dr["syn_konum"].ToString() != "resources")
                {
                    MessageBox.Show("OzelListbox.Nesne yok " + a);
                }
                else cmb_syntax.Items.Add(dr["syn_ad"]);
            }

            //Syntax combo doldurma işlemi
            /*
            StreamReader dosya = new StreamReader(@"Syntax\synt.con");
            string line;
            while ((line = dosya.ReadLine()) != null)
            {
                string ilk = "";
                foreach (char a in line)
                {
                    if (a == '-')
                    { break; }
                    ilk += a;
                }
                cmb_syntax.Items.Add(ilk);
            }*/
        }

        void synt_confekle(string dilad, string dilkonum)
        {
            StreamWriter yazici = File.AppendText(@"Syntax\synt.con");
            yazici.WriteLine(dilad + "-" + dilkonum);
            yazici.Close();
        }


        private void syntax_ekle(string tmp_syntax)
        {
            string diladi = Microsoft.VisualBasic.Interaction.InputBox("başlık", "konu", "cevap", 0, 0);
            if (baglan.State == ConnectionState.Closed)
                baglan.Open();
            kmt = new SQLiteCommand("insert into syntax(syn_ad,syn_konum) values(@synad,@synyer)", baglan);
            kmt.Parameters.AddWithValue("@synad", diladi);
            kmt.Parameters.AddWithValue("@synyer", tmp_syntax);
            kmt.ExecuteNonQuery();
        }

        #endregion

        private void listdoldur()
        {
            if (lbx_basliklar.SelectedItem != null)
            {
                OzelListbox.Nesne secilen = (OzelListbox.Nesne)lbx_basliklar.SelectedItem;
                slctd = lbx_basliklar.SelectedIndex;
                if (baglan.State == ConnectionState.Closed)
                    baglan.Open();
                kmt = new SQLiteCommand("select * from icerikler where baslik='" + secilen.isim.ToString() + "'", baglan);
                dr = kmt.ExecuteReader();
                while (dr.Read())
                {
                    if (Convert.ToInt16(dr["is_klasor"].ToString()) == 1)
                        eksorgu(new dizi_nesne() { baslik = dr["baslik"].ToString(), icerik = dr["icerik"].ToString(), kat_id = (int)dr["kat_id"], ust_kat = (int)dr["ust_kat"], isklasor = dr["is_klasor"].ToString(), katman = (int)dr["katman"] });
                    else eksorgu(new dizi_nesne() { baslik = dr["baslik"].ToString(), icerik = dr["icerik"].ToString(), kat_id = (int)dr["kat_id"], ust_kat = (int)dr["ust_kat"], isklasor = dr["is_klasor"].ToString(), katman = (int)dr["katman"] });

                }
                //dizi.Where(dizi_nesne => dizi_nesne.baslik == secilen.isim.ToString()).ToList().ForEach(yeniAta => eksorgu(new dizi_nesne() { baslik = yeniAta.baslik.ToString(), icerik = "" /*yeniAta.icerik.ToString()*/, kat_id = yeniAta.kat_id, ust_kat = yeniAta.ust_kat, isklasor = yeniAta.isklasor, katman = yeniAta.katman }));
            }
            else MessageBox.Show("Seçilen nesne yok.");

        }

        void eksorgu(dizi_nesne nesne)
        {
            try
            {
                lbx_basliklar.Items.RemoveAt(slctd);
                if (nesne.sifre == '1')
                    listnesne = new OzelListbox.Nesne() { id = nesne.kat_id, isim = nesne.baslik, is_klasor = Convert.ToInt16(nesne.isklasor), katman = nesne.katman, icerik = ""/*nesne.icerik*/, acik = true, sifreli = '2' };
                else listnesne = new OzelListbox.Nesne() { id = nesne.kat_id, isim = nesne.baslik, is_klasor = Convert.ToInt16(nesne.isklasor), katman = nesne.katman, icerik = ""/*nesne.icerik*/, acik = true, sifreli = '0' };
                lbx_basliklar.Items.Insert(slctd, listnesne);
                if (baglan.State == ConnectionState.Closed)
                    baglan.Open();
                kmt = new SQLiteCommand("select * from icerikler where ust_kat=" + nesne.kat_id + " order by is_klasor desc,baslik asc", baglan);
                dr = kmt.ExecuteReader();
                bool okundu = false;
                while (dr.Read())
                {
                    okundu = true;
                    olustur(Convert.ToInt16(dr["kat_id"].ToString()), dr["baslik"].ToString(), dr["is_klasor"].ToString(), Convert.ToInt16(dr["katman"].ToString()), "sorgu", "", Convert.ToChar(dr["sifre"].ToString()));
                }
                if (!okundu)
                {
                    slctd = slctd + 1;
                    listnesne = new OzelListbox.Nesne() { id = -1, isim = "Nesne yok", is_klasor = 2, katman = nesne.katman + 1, sifreli = '0', icerik = "Nesne bulunamadı" };
                    lbx_basliklar.Items.Insert(slctd, listnesne); lbx_basliklar.DisableItem(slctd);
                }


                //dizi.Where(arama => arama.ust_kat == nesne.kat_id).ToList().ForEach(arama => olustur(arama.baslik.ToString(), arama.ust_kat, arama.isklasor, arama.katman, "sorgu", ""));

            }
            catch (Exception exc) { MessageBox.Show(exc.Message); }
        }

        void olustur(int id, string baslik, string klasor, int kacinci, string nerden, string tmp_icerik, char tmp_sifre)
        {
            if (nerden == "acilis")
            {
                listnesne = new OzelListbox.Nesne() { id = id, isim = baslik, is_klasor = Convert.ToInt16(klasor), katman = kacinci, icerik = tmp_icerik, sifreli = tmp_sifre, acik = false };
                lbx_basliklar.Items.Add(listnesne);
            }
            else if (nerden == "sorgu")
            {
                slctd = slctd + 1;
                listnesne = new OzelListbox.Nesne() { id = id, isim = baslik, is_klasor = Convert.ToInt16(klasor), katman = kacinci, sifreli = tmp_sifre, icerik = tmp_icerik };
                lbx_basliklar.Items.Insert(slctd, listnesne);
            }
        }

        #region aşırı yüklemiş veritabanı çağırma metodu

        private void vtb_cagir()
        {
            if (baglan.State == ConnectionState.Closed)
                baglan.Open();
            kmt = new SQLiteCommand("Select * from icerikler ORDER BY baslik ASC", baglan);
            dr = kmt.ExecuteReader();
            dizi.Clear();
            while (dr.Read())
            {
                dizi.Add(new dizi_nesne { kat_id = Convert.ToInt16(dr["kat_id"].ToString()), ust_kat = Convert.ToInt16(dr["ust_kat"].ToString()), baslik = dr["baslik"].ToString(), icerik = dr["icerik"].ToString(), isklasor = dr["is_klasor"].ToString(), katman = Convert.ToInt16(dr["katman"].ToString()) });
            }
        }/*
        private void vtb_cagir()
        {
            if (baglan.State == ConnectionState.Closed)
                baglan.Open();
            kmt = new SQLiteCommand("Select * from icerikler ORDER BY baslik ASC", baglan);
            dr = kmt.ExecuteReader();
            dizi.Clear();
            while (dr.Read())
            {
                dizi.Add(new dizi_nesne { kat_id = Convert.ToInt16(dr["kat_id"].ToString()), ust_kat = Convert.ToInt16(dr["ust_kat"].ToString()), baslik = dr["baslik"].ToString(), icerik = dr["icerik"].ToString(), isklasor = dr["is_klasor"].ToString(), katman = Convert.ToInt16(dr["katman"].ToString()) });
            }
        }
        private void vtb_cagir()
        {
            if (baglan.State == ConnectionState.Closed)
                baglan.Open();
            kmt = new SQLiteCommand("Select * from icerikler ORDER BY baslik ASC", baglan);
            dr = kmt.ExecuteReader();
            dizi.Clear();
            while (dr.Read())
            {
                dizi.Add(new dizi_nesne { kat_id = Convert.ToInt16(dr["kat_id"].ToString()), ust_kat = Convert.ToInt16(dr["ust_kat"].ToString()), baslik = dr["baslik"].ToString(), icerik = dr["icerik"].ToString(), isklasor = dr["is_klasor"].ToString(), katman = Convert.ToInt16(dr["katman"].ToString()) });
            }
        }
        private void vtb_cagir()
        {
            if (baglan.State == ConnectionState.Closed)
                baglan.Open();
            kmt = new SQLiteCommand("Select * from icerikler ORDER BY baslik ASC", baglan);
            dr = kmt.ExecuteReader();
            dizi.Clear();
            while (dr.Read())
            {
                dizi.Add(new dizi_nesne { kat_id = Convert.ToInt16(dr["kat_id"].ToString()), ust_kat = Convert.ToInt16(dr["ust_kat"].ToString()), baslik = dr["baslik"].ToString(), icerik = dr["icerik"].ToString(), isklasor = dr["is_klasor"].ToString(), katman = Convert.ToInt16(dr["katman"].ToString()) });
            }
        }
        private void vtb_cagir()
        {
            if (baglan.State == ConnectionState.Closed)
                baglan.Open();
            kmt = new SQLiteCommand("Select * from icerikler ORDER BY baslik ASC", baglan);
            dr = kmt.ExecuteReader();
            dizi.Clear();
            while (dr.Read())
            {
                dizi.Add(new dizi_nesne { kat_id = Convert.ToInt16(dr["kat_id"].ToString()), ust_kat = Convert.ToInt16(dr["ust_kat"].ToString()), baslik = dr["baslik"].ToString(), icerik = dr["icerik"].ToString(), isklasor = dr["is_klasor"].ToString(), katman = Convert.ToInt16(dr["katman"].ToString()) });
            }
        }
        */
        #endregion

        private void listdoldur(int a)
        {
            if (baglan.State == ConnectionState.Closed)
                baglan.Open();
            kmt = new SQLiteCommand("select * from icerikler  inner Join syntaxs on icerikler.syntax=syntaxs.syn_id where ust_kat=@ustkat order by is_klasor desc,baslik asc", baglan);
            kmt.Parameters.AddWithValue("@ustkat", a);
            dr = kmt.ExecuteReader();
            while (dr.Read())
                olustur(Convert.ToInt32(dr["kat_id"].ToString()), dr["baslik"].ToString(), dr["is_klasor"].ToString(), Convert.ToInt32(dr["katman"].ToString()), "acilis", dr["icerik"].ToString(), Convert.ToChar(dr["sifre"].ToString()));

            //dizi.Where(dizi_nesne => dizi_nesne.ust_kat == a).ToList().ForEach(yeniAta => olustur(yeniAta.baslik.ToString(), yeniAta.ust_kat, yeniAta.isklasor, yeniAta.katman, "acilis", yeniAta.icerik));

        }

        void editorcagir(string tmp_baslik, string tmp_syntax)
        {
            bool found = false;
            foreach (TabPage tab in tab_txteditor.TabPages)
            {
                if (tmp_baslik.Equals(tab.Text))
                {
                    tab_txteditor.SelectedTab = tab;
                    found = true;
                }
            }
            if (!found)
            {
                textEditor = new TextEditor();
                textEditor.ShowLineNumbers = true;
                textEditor.HorizontalScrollBarVisibility = new System.Windows.Controls.ScrollBarVisibility();
                /* Metin editörü renklendirme 
                Color newColor = (Color)ColorConverter.ConvertFromString("#383838");
                Brush imageColor = new SolidColorBrush(newColor);
                textEditor.Foreground = Brushes.White;
                textEditor.Background = imageColor;*/
                textEditor.FontFamily = new System.Windows.Media.FontFamily("Consolas");
                textEditor.FontSize = 12.75f;
                textEditor.Options.HideCursorWhileTyping = false;


                //Host the WPF AvalonEdiot control in a Winform ElementHost control
                ElementHost host = new ElementHost();
                host.Dock = DockStyle.Fill;
                host.Child = textEditor;
                TabPage tabsayfa = new TabPage(tmp_baslik);
                tab_txteditor.Controls.Add(tabsayfa);
                tabsayfa.Size = new Size(tabsayfa.Size.Width, tabsayfa.Size.Height - 100);
                tabsayfa.Controls.Add(host);
                Label lbl = new Label();
                lbl.Location = new System.Drawing.Point(0, 0);
                lbl.Parent = tabsayfa;
                lbl.Text = tmp_syntax;
                tabsayfa.Controls.Add(lbl);
                tab_txteditor.SelectedTab = tabsayfa;
                int _ind = cmb_syntax.FindString(lbl.Text);
                cmb_syntax.SelectedIndex = _ind;
            }
        }

        void syntax(string tmp_combo)
        {
            string secilitab = tab_txteditor.SelectedTab.ToString();

            if (File.Exists("Syntaxs/" + tmp_combo + ".xshd") && tmp_combo != "Metin")
            {

                Stream xshd_stream = File.OpenRead("Syntaxs/" + tmp_combo + ".xshd");
                XmlTextReader xshd_reader = new XmlTextReader(xshd_stream);
                textEditor.SyntaxHighlighting = ICSharpCode.AvalonEdit.Highlighting.Xshd.HighlightingLoader.Load(xshd_reader, ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance);

                xshd_reader.Close();
                xshd_stream.Close();
            }
            else if (tmp_combo == "Metin") textEditor.SyntaxHighlighting = null; try { textEditor.SyntaxHighlighting = ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance.GetDefinition(tmp_combo); }
            catch { }
            foreach (Control lbl in tab_txteditor.SelectedTab.Controls)
            {
                if (lbl is Label)
                {
                    lbl.Text = tmp_combo;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*ElementHost _element = tab_txteditor.SelectedTab.Controls[0] as ElementHost;
            var texteditor = (TextEditor)_element.Child;
            MessageBox.Show(texteditor.Text);
            
            while (tab_txteditor.TabPages.Count != 0)
                tab_txteditor.TabPages.RemoveAt(0);*/


        }

        public class dizi_nesne
        {
            public int kat_id { get; set; }
            public int ust_kat { get; set; }
            public string baslik { get; set; }
            public string icerik { get; set; }
            public string isklasor { get; set; }
            public int katman { get; set; }
            public char sifre { get; set; }
        }


        void ata(dizi_nesne nesne, string slctdvalue)
        {
            nesne_ekle frm_ekle = new nesne_ekle(1, farekonum, "");
            frm_ekle.ShowDialog();
        }
        bool formaciliyor = false;

        private void OzelListbox1_DoubleClick(object sender, EventArgs e)
        {
            if (lbx_basliklar.SelectedItem != null)
            {
                if (formacik == true)
                    yukleniyor();
                OzelListbox.Nesne altsecim = (OzelListbox.Nesne)lbx_basliklar.SelectedItem;
                OzelListbox.Nesne secilen = (OzelListbox.Nesne)lbx_basliklar.SelectedItem;
                slctd = lbx_basliklar.SelectedIndex;
                try
                {
                    altsecim = (OzelListbox.Nesne)lbx_basliklar.Items[slctd + 1];
                }
                catch { }
                if (secilen.katman < altsecim.katman)
                    kapat(secilen);
                else
                {
                    int sifrekorumasi = 0;
                    if (secilen.sifreli == '1')
                    {
                        sifrekorumasi = sifrekontrol();
                    }

                    if (sifrekorumasi == 0)
                    {
                        if (secilen.is_klasor == 1)
                        {
                            if (baglan.State == ConnectionState.Closed)
                                baglan.Open();
                            kmt = new SQLiteCommand("select * from icerikler  inner join syntaxs on icerikler.syntax=syntaxs.syn_id where kat_id='" + secilen.id.ToString() + "' ", baglan);
                            dr = kmt.ExecuteReader();
                            while (dr.Read())
                                eksorgu(new dizi_nesne() { baslik = dr["baslik"].ToString(), icerik = "" /*yeniAta.icerik.ToString()*/, kat_id = Convert.ToInt32(dr["kat_id"].ToString()), ust_kat = Convert.ToInt32(dr["ust_kat"].ToString()), isklasor = dr["is_klasor"].ToString(), katman = Convert.ToInt32(dr["katman"].ToString()), sifre = Convert.ToChar(dr["sifre"].ToString()) });


                            //dizi.Where(arama => arama.baslik == secilen.isim.ToString()).ToList().ForEach(arama => eksorgu(new dizi_nesne() { baslik = arama.baslik, icerik = ""/*arama.icerik*/, kat_id = arama.kat_id, ust_kat = arama.ust_kat, isklasor = arama.isklasor, katman = arama.katman }));
                        }
                        else if (secilen.is_klasor == 0)
                        {
                            if (formacik != true)
                                formaciliyor = true;
                            if (baglan.State == ConnectionState.Closed)
                                baglan.Open();
                            kmt = new SQLiteCommand("select * from icerikler inner join syntaxs on icerikler.syntax=syntaxs.syn_id where baslik=@baslik", baglan);
                            kmt.Parameters.AddWithValue("@baslik", secilen.isim);
                            dr = kmt.ExecuteReader();
                            if (dr.Read())
                            {
                                editorcagir(dr["baslik"].ToString(), dr["syn_ad"].ToString());
                                textEditor.Text = dr["icerik"].ToString();
                            }

                            //dizi.Where(arama => arama.baslik == secilen.isim.ToString()).ToList().ForEach(arama => { editorcagir(arama.baslik); textEditor.Text = arama.icerik; });
                        }
                    }
                    if (formacik == true)
                        yukleniyor("");
                    if (formaciliyor == true)
                        formbuyu();
                }

            }
        }

        int sifrekontrol()
        {
            sifre sfrfrm = new sifre();
            if (!options.Default.pwhatirla)
            {
                sfrfrm.ShowDialog();
            }
            else if (string.IsNullOrWhiteSpace(Ortak.Psw))
            {
                sfrfrm.ShowDialog();
            }
            if (sfrfrm.DialogResult!=DialogResult.Abort && !string.IsNullOrWhiteSpace(Ortak.Psw))
                if (Properties.Settings.Default.Sifre == Ortak.Psw)
                    return 0;
                else { Ortak.durum = '0'; Ortak.sonuc = languageconvert("wrngpsw"); Ortak.Psw = ""; }
            return 1;
        }
        void formbuyu()
        {
            lbx_basliklar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom;
            pnl_genisle.Visible = true;
            if (formacik == false)
            {
                foreach (Control nesne in this.Controls)
                {
                    try
                    {
                        nesne.Enabled = false;
                    }
                    catch
                    {
                    }
                }
                tmr_formbuyu.Start();

            }
        }

        void kapat(OzelListbox.Nesne secilen)
        {
            try
            {
                lbx_basliklar.Items.RemoveAt(slctd);
                if (secilen.sifreli == '2')
                    listnesne = new OzelListbox.Nesne() { id = secilen.id, isim = secilen.isim, is_klasor = Convert.ToInt16(secilen.is_klasor), katman = secilen.katman, icerik = secilen.icerik, acik = false, sifreli = '1' };
                else listnesne = new OzelListbox.Nesne() { id = secilen.id, isim = secilen.isim, is_klasor = Convert.ToInt16(secilen.is_klasor), katman = secilen.katman, icerik = secilen.icerik, acik = false, sifreli = secilen.sifreli };

                lbx_basliklar.Items.Insert(slctd, listnesne);
                OzelListbox.Nesne altsecim = (OzelListbox.Nesne)lbx_basliklar.Items[slctd + 1];
                while (secilen.katman < altsecim.katman)
                {
                    if (altsecim.is_klasor == 2)
                        lbx_basliklar.EnableItem(slctd + 1);
                    lbx_basliklar.Items.RemoveAt(slctd + 1);
                    altsecim = (OzelListbox.Nesne)lbx_basliklar.Items[slctd + 1];
                }

                yukleniyor("");
            }
            catch { }
        }

        private void OzelListbox1_MouseMove(object sender, MouseEventArgs e)
        {
            int index = lbx_basliklar.IndexFromPoint(e.Location);
            // Just use the item's value for the tooltip.
            if (index != -1)
            {
                OzelListbox.Nesne secc = (OzelListbox.Nesne)lbx_basliklar.Items[index];
                if (secc.is_klasor == 0)
                    bildiri.Active = false;
                else
                {
                    bildiri.Active = true;
                    string tip = secc.icerik;
                    // Display the item's value as a tooltip.
                    if (bildiri.GetToolTip(lbx_basliklar) != tip)
                        bildiri.SetToolTip(lbx_basliklar, tip);
                }
            }
        }

        private void belgeEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nesne_ekle frm_ekle;
            if (lbx_basliklar.SelectedItem != null)
            {
                OzelListbox.Nesne nesne = (OzelListbox.Nesne)lbx_basliklar.SelectedItem;
                frm_ekle = new nesne_ekle(nesne.id, farekonum, "document");
            }
            else
            {
                frm_ekle = new nesne_ekle(-1, farekonum, "document");
            }
            frm_ekle.ShowDialog();

            if (Ortak.durum == '1')
            {
                lbx_basliklar.Items.Clear();
                listdoldur(0);
            }
        }

        public string languageconvert(string tmp_giris)
        {
            ci = new CultureInfo(options.Default.Language);
            Assembly a = Assembly.Load("VeriBank");
            ResourceManager rm = new ResourceManager("VeriBankasi.Lang.langres", a);
            return rm.GetString(tmp_giris, ci);
        }

        private void Anaform_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (baglan.State == ConnectionState.Open)
                baglan.Close();
            if (MessageBox.Show(languageconvert("exitques"), languageconvert("confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {

                e.Cancel = true;
            }
        }

        private void cmb_syntax_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tab_txteditor.TabCount > 0)
                syntax(cmb_syntax.SelectedItem.ToString());

        }


        private void context_ozellist_Opening(object sender, CancelEventArgs e)
        {
            farekonum = new System.Drawing.Point(Cursor.Position.X, Cursor.Position.Y);
            bool tmp_sonuc = false;
            OzelListbox.Nesne tmp_nesne = null;
            try
            {
                tmp_nesne = (OzelListbox.Nesne)lbx_basliklar.SelectedItem; tmp_sonuc = true;
                if (tmp_sonuc)
                    if (tmp_nesne.sifreli != '0')
                        context_ozellist.Items[3].Enabled = false;
                    else context_ozellist.Items[4].Enabled = false;
            }
            catch { for (int i = 2; i < context_ozellist.Items.Count; i++) context_ozellist.Items[i].Enabled = false; }

        }

        private void tmr_formbuyu_Tick(object sender, EventArgs e)
        {
            if (!formacik)
            {
                if (this.Width < 650)
                { this.Width += 30; this.Location = new System.Drawing.Point(this.Location.X - 15, this.Location.Y); }
                else if (this.Width < 700)
                { this.Width += 10; this.Location = new System.Drawing.Point(this.Location.X - 5, this.Location.Y); }
                else if (this.Width < 725)
                { this.Width += 5; this.Location = new System.Drawing.Point(this.Location.X - 2, this.Location.Y); }
                else if (this.Width < 745)
                { this.Width += 3; this.Location = new System.Drawing.Point(this.Location.X - 2, this.Location.Y); }
                else if (this.Width < 750)
                    this.Width += 1;
                else
                {
                    this.MaximizeBox = true; this.FormBorderStyle = FormBorderStyle.Sizable; formacik = true; tmr_formbuyu.Stop(); foreach (Control nesne in this.Controls)
                    {
                        try
                        {
                            nesne.Enabled = true;
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }

        private void btn_kaydet_Click(object sender, EventArgs e)
        {
            int durum = 0;

            try
            {
                //dizi_nesne _nesne = new dizi_nesne();
                //dizi.Where(arama => arama.baslik == tab_txteditor.SelectedTab.Text).ToList().ForEach(arama => _nesne = new dizi_nesne() { kat_id = arama.kat_id, ust_kat = arama.ust_kat, baslik = arama.baslik, icerik = arama.icerik, isklasor = arama.isklasor });
                if (baglan.State == ConnectionState.Closed)
                    baglan.Open();
                //kmt = new SQLiteCommand("select syn_id from syntax WHERE syn_ad=@synad",baglan);

                kmt = new SQLiteCommand("update icerikler set icerik=@icerik,  syntax=(select syn_id from syntaxs WHERE syn_ad=@synad) where baslik=@baslik ", baglan);
                kmt.Parameters.AddWithValue("@synad", cmb_syntax.SelectedItem.ToString());
                kmt.Parameters.AddWithValue("@baslik", tab_txteditor.SelectedTab.Text);
                kmt.Parameters.AddWithValue("@icerik", editordencek());
                durum = kmt.ExecuteNonQuery();
            }
            catch (Exception exp) { MessageBox.Show(exp.Message); }
            finally
            {
                if (durum == 1)
                {
                    Ortak.durum = '1';
                    Ortak.sonuc = languageconvert("savesuccess");
                }
                else { Ortak.durum = '0'; Ortak.sonuc = languageconvert("savefail"); }
                tmrstatus_kontrol();
                //diziguncelle();
            }
        }

        private string editordencek()
        {
            ElementHost _element = tab_txteditor.SelectedTab.Controls[0] as ElementHost;
            var _texteditor = (TextEditor)_element.Child;
            return _texteditor.Text;
        }

        private void tmr_statusbar_Tick(object sender, EventArgs e)
        {
            drmcbk_lbl.Text = "";
            Ortak.sonuc = null;
            drmcbk_lbl.ForeColor = System.Drawing.Color.Black;
            tmr_statusbar.Stop();
        }

        private void Anaform_FormClosed(object sender, FormClosedEventArgs e)
        {

            Task ts1 = Task.Run(() => File.Delete(@"Syntax\synt.con"));
            Task.WaitAll(ts1);
        }

        private void tab_txteditor_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Control lbl in tab_txteditor.SelectedTab.Controls)
            {
                if (lbl is Label)
                {
                    int _ind = cmb_syntax.FindString(lbl.Text);
                    cmb_syntax.SelectedIndex = _ind;
                }
            }

        }

        private void tab_txteditor_Click(object sender, EventArgs e)
        {
            var fareeekonum = new System.Drawing.Point(Cursor.Position.X, Cursor.Position.Y);


        }

        private void klasörEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nesne_ekle frm_ekle;
            if (lbx_basliklar.SelectedItem != null)
            {
                OzelListbox.Nesne nesne = (OzelListbox.Nesne)lbx_basliklar.SelectedItem;
                frm_ekle = new nesne_ekle(nesne.id, farekonum, "folder");
            }
            else
            {
                frm_ekle = new nesne_ekle(-1, farekonum, "folder");
            }
            frm_ekle.ShowDialog();
            if (Ortak.durum == '1')
            {
                lbx_basliklar.Items.Clear();
                listdoldur(0);
            }
        }

        private void Anaform_Activated(object sender, EventArgs e)
        {/*
            if (!string.IsNullOrWhiteSpace(Ortak.sonuc))
            {
                
                /*tmrstatus_kontrol();
            }
        */
        }
        public string sifrele(string metin)
        {

            return "";
        }

        void yeniad()
        {
            if (lbx_basliklar.SelectedIndex != -1)
            {
                var secili = (OzelListbox.Nesne)lbx_basliklar.SelectedItem;
                int seciili = lbx_basliklar.SelectedIndex;
                lbx_basliklar.Items.RemoveAt(seciili);
                secili.durum = 'v';
                lbx_basliklar.Items.Insert(seciili, secili);

            }
        }
        void basliksil()
        {
            char secilenvar = 'y';
            try
            {
                OzelListbox.Nesne tmp_nesne = (OzelListbox.Nesne)lbx_basliklar.SelectedItem;
                secilenvar = 'v';
                int sifrekonroller = 0;
                if (tmp_nesne.sifreli == '1')
                    sifrekonroller = sifrekontrol();
                if (sifrekonroller == 0)
                {
                    string tmp_message = "";
                    if (options.Default.Language == "tr-TR")
                        tmp_message = tmp_nesne.isim + ' ' + languageconvert("deletemessage") + '?';
                    else tmp_message = languageconvert("deletemessage") + ' ' + tmp_nesne.isim + '?';
                    if (MessageBox.Show(tmp_message, languageconvert("confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                    {
                        kmt = new SQLiteCommand("delete from icerikler where kat_id=@id", baglan);
                        kmt.Parameters.AddWithValue("@id", tmp_nesne.id);
                        if (baglan.State == ConnectionState.Closed)
                            baglan.Open();
                        int tmp = kmt.ExecuteNonQuery();
                        if (tmp == 0)
                        {
                            Ortak.durum = '0';
                            Ortak.sonuc = "fail";
                        }
                        else { Ortak.durum = '1'; Ortak.sonuc = languageconvert("deletesuccess"); int seciliolan = lbx_basliklar.SelectedIndex; lbx_basliklar.Refresh(); lbx_basliklar.Items.RemoveAt(seciliolan); }
                    }
                }
            }
            catch (Exception exp)
            {
                if (secilenvar == 'y') { Ortak.durum = '0'; Ortak.sonuc = languageconvert("objectnotselected"); }
                else Ortak.durum = '0'; Ortak.sonuc = languageconvert("unknownerror"); hatalogkaydi(exp.Message);
            }
        }
        public void tmrstatus_kontrol()
        {
            try { tmr_statusbar.Stop(); } catch { }
            if (Ortak.durum == '0')
                drmcbk_lbl.ForeColor = Color.Red;
            else drmcbk_lbl.ForeColor = Color.Green;
            drmcbk_lbl.Text = Ortak.sonuc;
            tmr_statusbar.Start();
        }


        private void yenidenAdlandırToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yeniad();
        }
        public void adguncelle(OzelListbox.Nesne nesne)
        {
            try
            {
                if (baglan.State == ConnectionState.Closed)
                    baglan.Open();
                //kmt = new SQLiteCommand("select syn_id from syntax WHERE syn_ad=@synad",baglan);
                kmt = new SQLiteCommand("update icerikler set baslik=@baslik where kat_id=@katid ", baglan);
                kmt.Parameters.AddWithValue("@baslik", nesne.isim);
                kmt.Parameters.AddWithValue("@katid", nesne.id);
                Ortak.durum = kmt.ExecuteNonQuery();
            }
            catch (Exception exp) { MessageBox.Show(exp.Message); }
            finally
            {
                if (Ortak.durum == 1)
                {
                    Ortak.sonuc = "Kayıt başarılı...";
                }
                else { Ortak.sonuc = "Kayıt başarısız..."; }
                //diziguncelle();
            }
        }

        private void lbx_basliklar_Resize(object sender, EventArgs e)
        {
            lbx_basliklar.Refresh();
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            basliksil();
        }

        private void btn_setting_Click(object sender, EventArgs e)
        {
            Ayarlar form = new Ayarlar();
            form.ShowDialog();
            language();
        }

        private void context_ozellist_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            for (int i = 0; i < context_ozellist.Items.Count; i++)
                context_ozellist.Items[i].Enabled = true;
        }

        private void passtool_Click(object sender, EventArgs e)
        {
            try
            {
                OzelListbox.Nesne tmp_nesne = (OzelListbox.Nesne)lbx_basliklar.SelectedItem;
                slctd = lbx_basliklar.SelectedIndex;
                sifre sfrfrm = new sifre();
                sfrfrm.ShowDialog();
                if (sfrfrm.DialogResult != DialogResult.Abort && !string.IsNullOrWhiteSpace(Ortak.Psw))
                    if (Properties.Settings.Default.Sifre == Ortak.Psw)
                    {
                        tmp_nesne.sifreli = '1';
                        kmt = new SQLiteCommand("update icerikler set sifre=@sifre where kat_id=@kat_id", baglan);
                        kmt.Parameters.AddWithValue("@sifre", tmp_nesne.sifreli.ToString());
                        kmt.Parameters.AddWithValue("@kat_id", tmp_nesne.id);
                        int tmp_sonuc = kmt.ExecuteNonQuery();
                        if (tmp_sonuc == 1)
                        {
                            lbx_basliklar.Items.RemoveAt(slctd);
                            lbx_basliklar.Items.Insert(slctd, tmp_nesne);
                            Ortak.durum = 1;
                            Ortak.sonuc = languageconvert("success");
                        }
                        else
                        {
                            Ortak.durum = 0; Ortak.sonuc = languageconvert("fail");
                        }
                    }
                    else { Ortak.durum = '0'; Ortak.sonuc = languageconvert("wrngpsw"); Ortak.Psw = ""; }
            }
            catch (Exception exp)
            {
                Ortak.durum = 0; Ortak.sonuc = languageconvert("unknownerror"); hatalogkaydi(exp.Message);
            }
        }

        private void removepass_Click(object sender, EventArgs e)
        {
            try
            {
                OzelListbox.Nesne tmp_nesne = (OzelListbox.Nesne)lbx_basliklar.SelectedItem;
                slctd = lbx_basliklar.SelectedIndex;
                sifre sfrfrm = new sifre();
                sfrfrm.ShowDialog();
                if (sfrfrm.DialogResult != DialogResult.Abort && !string.IsNullOrWhiteSpace(Ortak.Psw))
                    if (Properties.Settings.Default.Sifre == Ortak.Psw)
                    {
                        tmp_nesne.sifreli = '0';
                        kmt = new SQLiteCommand("update icerikler set sifre=@sifre where kat_id=@kat_id", baglan);
                        kmt.Parameters.AddWithValue("@sifre", tmp_nesne.sifreli.ToString());
                        kmt.Parameters.AddWithValue("@kat_id", tmp_nesne.id);
                        int tmp_sonuc = kmt.ExecuteNonQuery();
                        if (tmp_sonuc == 1)
                        {
                            lbx_basliklar.Items.RemoveAt(slctd);
                            lbx_basliklar.Items.Insert(slctd, tmp_nesne);
                            Ortak.durum = 1;
                            Ortak.sonuc = languageconvert("success");
                        }
                        else
                        {
                            Ortak.durum = 0; Ortak.sonuc = languageconvert("fail");
                        }
                    }
                    else { Ortak.durum = '0'; Ortak.sonuc = languageconvert("wrngpsw"); Ortak.Psw = ""; }
            }
            catch (Exception exp)
            {
                Ortak.durum = 0; Ortak.sonuc = languageconvert("unknownerror"); hatalogkaydi(exp.Message);
            }
        }
        public void hatalogkaydi(string hatamesaji)
        {
            StreamWriter yazici = new StreamWriter("log.txt");
            yazici.WriteLine(hatamesaji);
            MessageBox.Show(languageconvert("checkerrorlog") + @"/log.txt");
            yazici.Close();
        }
    }
    public static class Ortak
    {
        private static string Sonuc = "";
        public static string sonuc { get { return Sonuc; } set { Sonuc = value; Program.Anaform.tmrstatus_kontrol(); } }
        private static int Durum = 1;
        public static int durum { get { return Durum; } set { Durum = value; } }

        private static string psw = "";
        public static string Psw { get { return psw; } set { psw = value; } }
    }
}

