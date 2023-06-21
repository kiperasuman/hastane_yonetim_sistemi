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
    public partial class FrmDoktorDetay : Form
    {
        public FrmDoktorDetay()
        {
            InitializeComponent();
        }
        sqlBaglantisi bgl = new sqlBaglantisi();
        
        public string tc;

        private void FrmDoktorDetay_Load(object sender, EventArgs e)
        {
            lblTc.Text = tc;


            // Ad Soyad Çekme
            SqlCommand command = new SqlCommand("select DoktorAd , DoktorSoyad from Tbl_Doktorlar where DoktorTC =@p1 ",bgl.baglanti());
            command.Parameters.AddWithValue("@p1", tc);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                lblAdSoyad.Text = reader[0].ToString() + ' ' + reader[1].ToString();

            }

            bgl.baglanti().Close();


            //randevu listesi 
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from Tbl_Randevular where RandevuDoktor = '"+ lblAdSoyad.Text + "'", bgl.baglanti());

            adapter.Fill(dt);
            dataGridView1.DataSource = dt; 



        }

        private void btnBilgiDuzenle_Click(object sender, EventArgs e)
        {
            FrmDoktorBilgiDuzenle duzenle = new FrmDoktorBilgiDuzenle();
            duzenle.tcno = lblTc.Text;
            duzenle.Show();

        }

        private void btnDuyuru_Click(object sender, EventArgs e)
        {
            FrmDuyurular duyuru = new FrmDuyurular();
            duyuru.Show();

        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            rchSikayet.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
        }
    }
}
