using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VeriBankasi
{
    public partial class Ayarlar : Form
    {
        public Ayarlar()
        {
            InitializeComponent();
        }
        Anaform ana = new Anaform();
        void language()
        {
            lbl_lang.Text = ana.languageconvert("lang");
            btn_iptal.Text = ana.languageconvert("btncancel");
            btn_kydt.Text = ana.languageconvert("btnsave");
            chb_pwhatirla.Text = ana.languageconvert("pwhatirla");
            this.Text = ana.languageconvert("setting");
        }


        private void Ayarlar_Load(object sender, EventArgs e)
        {
            language();
            cmblang_kontrol();
            chbhatirla();
        }

        private void btn_iptal_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        void chbhatirla()
        {
            if (options.Default.pwhatirla)
                chb_pwhatirla.Checked = true;
            else chb_pwhatirla.Checked = false;
        }
        void cmblang_kontrol()
        {
            switch (options.Default.Language)
            {
                case "tr-TR":
                    cmb_languages.SelectedIndex = cmb_languages.FindString("Türkçe");
                    break;
                case "en-US":
                    cmb_languages.SelectedIndex = cmb_languages.FindString("English");
                    break;
                case "fr-FR":
                    cmb_languages.SelectedIndex = cmb_languages.FindString("French"); break;
                default:
                    MessageBox.Show("hata"); break;
            }
        }

        private void btn_kydt_Click(object sender, EventArgs e)
        {
            switch (cmb_languages.SelectedItem.ToString())
            {
                case "Türkçe":
                    options.Default.Language = "tr-TR";
                    break;
                case "English":
                    options.Default.Language = "en-US";
                    break;
                case "French":
                    MessageBox.Show(ana.languageconvert("lngnotsupport"));
                    break;
                default:
                    MessageBox.Show("hata"); break;
            }
            if (chb_pwhatirla.Checked)
                options.Default.pwhatirla = true;
            else options.Default.pwhatirla = false;
            options.Default.Save();
            this.Dispose();
        }

        private void cmb_languages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_languages.SelectedItem.ToString() == "French")
            {
                MessageBox.Show(ana.languageconvert("lngnotsupport"));
                cmblang_kontrol();
            }
        }
    }
}
