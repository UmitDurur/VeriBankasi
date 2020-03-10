using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VeriBankasi
{
    public partial class sifre : Form
    {
        public sifre()
        {
            InitializeComponent();
        }

        Anaform ana = new Anaform();

        private void sifre_Load(object sender, EventArgs e)
        {
            btn_cancel.Text = ana.languageconvert("btncancel");
            btn_tamam.Text = ana.languageconvert("btnok");
            this.Text = ana.languageconvert("password");
        }

        private void btn_tamam_Click(object sender, EventArgs e)
        {
            Ortak.Psw = txt_psw.Text;
            this.Dispose();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
            this.Dispose();
        }
    }
}
