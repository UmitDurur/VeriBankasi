using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;

namespace VeriBankasi
{
    public partial class nesne_ekle : Form
    {
        public nesne_ekle(int secili, Point fare, string islem)
        {
            InitializeComponent();
            slctdvalue = secili;
            farekonum = fare;
            this.islem = islem;
            switch (islem)
            {
                case "document":
                    pnl_blgekle.Visible = true;
                    pnl_header.Parent = pnl_blgekle;
                    pnl_footer.Parent = pnl_blgekle;
                    break;
                case "folder":
                    pnl_dsyekle.Visible = true;
                    pnl_header.Parent =pnl_dsyekle;
                    pnl_footer.Parent = pnl_dsyekle;
                    this.Size=new Size(this.Size.Width,this.Size.Height+20);
                    
                    break;
                default: MessageBox.Show("Hata"); break;
            }
        }

        public class dizi_nesne
        {
            public int kat_id { get; set; }
            public int ust_kat { get; set; }
            public string baslik { get; set; }
            public string is_klasor { get; set; }
            public int katman { get; set; }
        }

        void dizidoldur()
        {
            if (baglan.State == ConnectionState.Closed)
                baglan.Open();
            kmt = new SQLiteCommand("Select * from icerikler where is_klasor=1 ORDER BY kat_id ASC", baglan);
            dr = kmt.ExecuteReader();
            dizi.Clear();
            while (dr.Read())
            {
                dizi.Add(new dizi_nesne { kat_id = Convert.ToInt16(dr["kat_id"].ToString()), ust_kat = Convert.ToInt16(dr["ust_kat"].ToString()), baslik = dr["baslik"].ToString(), is_klasor = dr["is_klasor"].ToString(), katman = Convert.ToInt16(dr["katman"].ToString()) });
            }
        }

        SQLiteConnection baglan = new SQLiteConnection("Data Source=data.dll; Version=3;");
        SQLiteCommand kmt;
        SQLiteDataReader dr;

        List<dizi_nesne> dizi = new List<dizi_nesne>();

        Point farekonum;
        string islem;
        int slctdvalue;
        public string baslik;
        public int kat_id;
        int eskiind = -1;
        bool combosecengelle = false;
        Anaform ana = new Anaform();

        void language()
        {
            lbl_baslik.Text =ana.languageconvert("title");
            lbl_icerik.Text = ana.languageconvert("comment");
            btn_blgekle.Text = ana.languageconvert("btnadd");
            btn_iptal.Text = ana.languageconvert("btncancel");
        }

        private void nesne_ekle_Load(object sender, EventArgs e)
        {
            dizidoldur();
            language();
            this.Location = farekonum;
            cmb_ktgri.DisplayMember = "baslik";
            cmb_ktgri.Items.Add(new cmbitem { id = -1, baslik = ana.languageconvert("topdirectory") });
            cmb_ktgri.Items.Add(new cmbitem { id = -1, baslik = "----------" });
            cmb_ktgri.Items.Add(new cmbitem { id = 0, baslik = ana.languageconvert("parentdirectory") ,katman=0});
            cmb_ktgri.Items.Add(new cmbitem{ id = -1,baslik = "----------"});

            dizi.Where(arama => arama.ust_kat == 0).ToList().ForEach(yeni => { altklasorler(yeni.kat_id, yeni.baslik, yeni.is_klasor, yeni.katman); cmb_ktgri.Items.Add(new cmbitem { id = -1, baslik = "----------" }); });
            if (slctdvalue != -1)
            {
                int ind = -1;
                for(int i=0; i<cmb_ktgri.Items.Count;i++)
                {
                    cmbitem tmp_item = (cmbitem)cmb_ktgri.Items[i];
                    if (tmp_item.id == slctdvalue)
                        ind = i;
                }
                //int ind = cmb_ktgri.FindString(slctdvalue);
                cmb_ktgri.SelectedIndex = ind;
                eskiind = ind;
            }
            else cmb_ktgri.SelectedIndex = 0;
            dizi.Clear();
            combosecengelle = true;
        }

        void altklasorler(int tmp_katid, string tmp_baslik, string tmp_isklasor, int tmp_katman)
        {
            string a = ""; for (int i = 1; i < tmp_katman; i++) a = a + "  ";
            cmb_ktgri.Items.Add(new cmbitem { id = tmp_katid, baslik = a + tmp_baslik, katman = tmp_katman });
            if (tmp_isklasor == "0")
            {
                a = a + "  ";
                dizi.Where(arama => arama.ust_kat == tmp_katid).ToList().ForEach(yeni => { cmb_ktgri.Items.Add(new cmbitem { id = yeni.kat_id, baslik = a + yeni.baslik, katman = yeni.katman }); });
            }
            else dizi.Where(arama => arama.ust_kat == tmp_katid).ToList().ForEach(yeni => { altklasorler(yeni.kat_id, yeni.baslik, yeni.is_klasor, yeni.katman); });
        }

        public class cmbitem
        {
            public int id { get; set; }
            public string baslik { get; set; }
            public int katman { get; set; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Ortak.durum = '0';
            Ortak.sonuc = ana.languageconvert("cancel");
            this.Close();
        }

        private void cmb_ktgri_SelectedIndexChanged(object sender, EventArgs e)
        {
            try {
                if(combosecengelle){
                    cmbitem gecici = (cmbitem)cmb_ktgri.SelectedItem;
                    if (gecici.id == -1)
                    {
                        cmb_ktgri.SelectedIndex = eskiind;
                    }
                    else eskiind = cmb_ktgri.SelectedIndex;
                }
            }
            catch { }
        }

        private void nesne_ekle_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Close();
        }

        private void btn_klsrekle_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt_baslik.Text))
            {
                if (islem == "Belge ekle")
                    belge_ekle();
                else dosya_ekle();
            }
            else MessageBox.Show(ana.languageconvert("title")+' '+ana.languageconvert("notnull"));
        }


        void dosya_ekle()
        {
            cmbitem secilicmb = (cmbitem)cmb_ktgri.SelectedItem;
            if (secilicmb.id >= 0)
                //dizi.Where(arama=>arama.baslik==cmb_ktgri.SelectedText.Trim());
                kmt = new SQLiteCommand("insert into icerikler(ust_kat,baslik,icerik,is_klasor,katman) VALUES(@ust_kat,@baslik,@icerik,@klasor,@katman)", baglan);
            kmt.Parameters.AddWithValue("@ust_kat", secilicmb.id);
            kmt.Parameters.AddWithValue("@baslik", txt_baslik.Text);
            kmt.Parameters.AddWithValue("@icerik",txt_icerik.Text);
            kmt.Parameters.AddWithValue("@klasor",1);
            kmt.Parameters.AddWithValue("@katman", secilicmb.katman + 1);
            try
            {
                kmt.ExecuteNonQuery();
                Ortak.durum = '1';
                Ortak.sonuc = ana.languageconvert("success");
                this.Close();
            }
            catch (Exception exc)
            {
                Ortak.durum = '0';
                Ortak.sonuc = ana.languageconvert("fail") ;
                ana.hatalogkaydi(exc.Message);
            }
        }


        void belge_ekle()
        {
            cmbitem secilicmb = (cmbitem)cmb_ktgri.SelectedItem;
            if (secilicmb.id >= 0)
                //dizi.Where(arama=>arama.baslik==cmb_ktgri.SelectedText.Trim());
                kmt = new SQLiteCommand("insert into icerikler(ust_kat,baslik,katman) VALUES(@ust_kat,@baslik,@katman)", baglan);
            kmt.Parameters.AddWithValue("@ust_kat", secilicmb.id);
            kmt.Parameters.AddWithValue("@baslik", txt_baslik.Text);
            kmt.Parameters.AddWithValue("@katman", secilicmb.katman + 1);
            try
            {
                kmt.ExecuteNonQuery();
                Ortak.durum = '1';
                Ortak.sonuc = ana.languageconvert("success");
                this.Close();
            }
            catch (Exception exc)
            {
                Ortak.durum = '0';
                Ortak.sonuc = ana.languageconvert("fail") ;
                ana.hatalogkaydi(exc.Message);
            }
        }

    }
}
