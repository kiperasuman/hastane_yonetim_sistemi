using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace HastaneProje
{
    public partial class FrmHastaGiris : Form
    {
        public FrmHastaGiris()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmUyeOl uye = new FrmUyeOl();
            uye.Show();
        }
        sqlBaglantisi bgl= new sqlBaglantisi();
       
        private void btnGiris_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("select * from Tbl_Hastalar where HastaTC = @p1 and HastaSifre = @p2", bgl.baglanti());
            command.Parameters.AddWithValue("@p1",mskTC.Text);
            command.Parameters.AddWithValue("@p2",txtSifre.Text);
            SqlDataReader reader = command.ExecuteReader(); 
            if(reader.Read())
            {
                MessageBox.Show("Giriş Başarılı ! Hoşgeldiniz ! ");
                FrmHastaDetay hastaDetay = new FrmHastaDetay();
                hastaDetay.tc = mskTC.Text;
                hastaDetay.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Giriş ! Lütfen Tekrar Deneyiniz ! ");
            }
            bgl.baglanti().Close();
        }
    }
}
