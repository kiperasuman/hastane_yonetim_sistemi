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
    public partial class FrmDoktorBilgiDuzenle : Form
    {
        public FrmDoktorBilgiDuzenle()
        {
            InitializeComponent();
        }
        sqlBaglantisi bgl = new sqlBaglantisi();
        public string tcno; 

        private void FrmDoktorBilgiDuzenle_Load(object sender, EventArgs e)
        {
            mskTC.Text = tcno;

            SqlCommand command = new SqlCommand("Select * from Tbl_Doktorlar where DoktorTC = @p1", bgl.baglanti());
            command.Parameters.AddWithValue("@p1",mskTC.Text);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                txtAd.Text = reader[1].ToString();
                txtSoyad.Text = reader[2].ToString();
                mskTel.Text = reader[6].ToString();
                cmbBrans.Text = reader[3].ToString();
                txtSifre.Text = reader[5].ToString();

            }

            bgl.baglanti().Close();


            SqlCommand comm = new SqlCommand("Select BransAd from Tbl_Branslar", bgl.baglanti());
            SqlDataReader reader2 = comm.ExecuteReader();
            while (reader2.Read())
            {
                cmbBrans.Items.Add(reader2[0]);
            }
            bgl.baglanti().Close();
        }

        private void btnBilgiGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("update Tbl_Doktorlar set DoktorAd= @p1,DoktorSoyad = @p2,DoktorTelefon = @p3 , DoktorBrans = @p4 , DoktorSifre = @p5 where DoktorTC = @p6",bgl.baglanti());
            command.Parameters.AddWithValue("@p1",txtAd.Text);
            command.Parameters.AddWithValue("@p2", txtSoyad.Text);
            command.Parameters.AddWithValue("@p3", mskTel.Text);
            command.Parameters.AddWithValue("@p4", cmbBrans.Text);
            command.Parameters.AddWithValue("@p5",txtSifre.Text);
            command.Parameters.AddWithValue("@p6",mskTC.Text);
            command.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kayıt Güncelleme İşlemi Başarılı...!", "Bilgilendirme",MessageBoxButtons.OK , MessageBoxIcon.Information);
        }
    }
}
