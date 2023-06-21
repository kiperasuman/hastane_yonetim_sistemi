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
    public partial class FrmSekreterDetay : Form
    {
        sqlBaglantisi bgl = new sqlBaglantisi();
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        public string tc;
        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            lblTc.Text = tc;
          
            // Ad - Soyad 
            SqlCommand komut = new SqlCommand("select SekreterAdSoyad from Tbl_Sekreterler where SekreterTC = @p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lblTc.Text);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                lblAdSoyad.Text = reader[0].ToString(); 
            }
            bgl.baglanti().Close();


            // Branşları Çekme İşlemi 

            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("Select BransAd from Tbl_Branslar",bgl.baglanti());
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;

            // Doktorlar - DataGridView
            DataTable dt2 = new DataTable();
            SqlDataAdapter adapter2 = new SqlDataAdapter("select * from Tbl_Doktorlar",bgl.baglanti());
            adapter2.Fill(dt2);
            dataGridView2.DataSource = dt2;


            //Branşı Comboboxa Aktarma 
            SqlCommand comm = new SqlCommand("Select BransAd from Tbl_Branslar",bgl.baglanti());
            SqlDataReader reader2 = comm.ExecuteReader();
            while (reader2.Read())
            {
                cmbBrans.Items.Add(reader2[0]);
            }
            bgl.baglanti().Close();

            
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Randevular (RandevuTarih, RandevuSaat , RandevuBrans, RandevuDoktor) values(@p1,@p2,@p3,@p4) ",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", mskTarih.Text);
            komut.Parameters.AddWithValue("@p2", mskSaat.Text);
            komut.Parameters.AddWithValue("@p3", cmbBrans.Text);
            komut.Parameters.AddWithValue("@p4", cmbDoktor.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kayıt İşlemi Başarılı..!", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoktor.Items.Clear();
            // Doktorları Combobaxa aktarma 
            SqlCommand command2 = new SqlCommand("select (DoktorAd + ' ' + DoktorSoyad) From Tbl_Doktorlar where DoktorBrans = @p1", bgl.baglanti());
            command2.Parameters.AddWithValue("@p1",cmbBrans.Text);
            SqlDataReader readerr = command2.ExecuteReader();
            while (readerr.Read())
            {
                cmbDoktor.Items.Add(readerr[0]);
            }
            bgl.baglanti().Close();
        }

        private void btnDuyuruKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand("insert into Tbl_Duyurular(DuyuruDetay) values (@p1)", bgl.baglanti());
            sqlCommand.Parameters.AddWithValue("@p1", rchDuyuru.Text);
            sqlCommand.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Duyuru Oluşturuldu..!", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDoktorPaneli_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli panel = new FrmDoktorPaneli();
            panel.Show();
            

        }

        private void btnBransPaneli_Click(object sender, EventArgs e)
        {
            FrmBransPaneli panel = new FrmBransPaneli();
            panel.Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi liste = new FrmRandevuListesi();
            liste.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmDuyurular duyuru = new FrmDuyurular();
            duyuru.Show();

        }
    }
}
