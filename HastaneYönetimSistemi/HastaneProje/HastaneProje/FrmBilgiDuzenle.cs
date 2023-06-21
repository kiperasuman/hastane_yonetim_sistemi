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
namespace HastaneProje
{
    public partial class FrmBilgiDuzenle : Form
    {
        public FrmBilgiDuzenle()
        {
            InitializeComponent();
        }

        public string TCno;
        sqlBaglantisi bgl = new sqlBaglantisi();
        public string cinsiyet;
        private void FrmBilgiDuzenle_Load(object sender, EventArgs e)
        {
            mskTC.Text = TCno;
            SqlCommand komut = new SqlCommand("select * from Tbl_Hastalar where HastaTC = @p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1" , mskTC.Text);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                txtAd.Text = reader[1].ToString();
                txtSoyad.Text = reader[2].ToString();
                mskTel.Text = reader[4].ToString();
                txtSifre.Text = reader[5].ToString();
                cinsiyet = reader[6].ToString();
                if (cinsiyet =="Kadın")
                {
                    checkBox1.Checked = true;

                }
                else
                {
                    checkBox2.Checked = true;
                }
            }
            bgl.baglanti().Close();
        }

        private void btnBilgiGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update Tbl_Hastalar set HastaAd = @p1,HastaSoyad = @p2 , HastaTelefon = @p3 , HastaSifre = @p4 , HastaCinsiyet = @p5 where HastaTC = @p6",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", mskTel.Text);
            komut.Parameters.AddWithValue("@p4", txtSifre.Text);
            komut.Parameters.AddWithValue("@p5", cinsiyet);
            komut.Parameters.AddWithValue("@p6",mskTC.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Bilgileriniz Güncellendi ! ","Bilgi" ,MessageBoxButtons.OK , MessageBoxIcon.Information);

        
        }
    }
}
