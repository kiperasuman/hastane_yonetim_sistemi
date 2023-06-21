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
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }
        sqlBaglantisi bgl = new sqlBaglantisi();
        public string tc;
        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            // AD SOYAD ÇEKME İŞLEMİ 
            lblTc.Text = tc;    
            SqlCommand command = new SqlCommand("select HastaAd,HastaSoyad from Tbl_Hastalar where HastaTC = @p1 ", bgl.baglanti());
            command.Parameters.AddWithValue("@p1", lblTc.Text);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                lblAdSoyad.Text = reader[0] +" "+ reader[1];
            }
            bgl.baglanti().Close();

            // RANDEVU GEÇMİŞİ 

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Randevular where HastaTC= " + tc, bgl.baglanti());
            da.Fill(dt);
            dataGecmis.DataSource = dt;


            // Branş Çekme
            SqlCommand command2 = new SqlCommand("select BransAd from Tbl_Branslar",bgl.baglanti());
            SqlDataReader dataReader = command2.ExecuteReader();
            while (dataReader.Read())
            {
                cmbBrans.Items.Add(dataReader[0]);
            }

            bgl.baglanti().Close();
        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoktor.Items.Clear();

            SqlCommand komut3 = new SqlCommand("select DoktorAd, DoktorSoyad from Tbl_Doktorlar where DoktorBrans = @p1",bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1", cmbBrans.Text);
            SqlDataReader dataReader = komut3.ExecuteReader();
            while (dataReader.Read())
            {
                cmbDoktor.Items.Add(dataReader[0]+ " "+dataReader[1]);
            }

            bgl.baglanti().Close();

        }

        private void cmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Randevular where RandevuBrans = '" + cmbBrans.Text + "'" + " and RandevuDoktor = '"+cmbDoktor.Text +"'and RandevuDurum = 0" ,bgl.baglanti());
            da.Fill(dt);
            dataAktif.DataSource = dt;
        
        
        
        
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmBilgiDuzenle duzenle = new FrmBilgiDuzenle();
            duzenle.TCno = lblTc.Text;
            duzenle.Show();
           
        }

        private void btnRandevuAl_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update Tbl_Randevular set RandevuDurum = 1, HastaTC = @p1,HastaSikayet=@p2 where RandevuId = @p3", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",lblTc.Text);
            komut.Parameters.AddWithValue("@p2", rchSikayet.Text);
            komut.Parameters.AddWithValue("@p3", txtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Alındı","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);

        }

        private void dataAktif_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataAktif.SelectedCells[0].RowIndex;
            txtId.Text = dataAktif.Rows[secilen].Cells[0].Value.ToString();
        }
    }
}   
