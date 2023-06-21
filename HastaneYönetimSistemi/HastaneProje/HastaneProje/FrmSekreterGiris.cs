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
    public partial class FrmSekreterGiris : Form
    {
        public FrmSekreterGiris()
        {
            InitializeComponent();
        }
        sqlBaglantisi bgl = new sqlBaglantisi();

        private void btnGiris_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("select * from Tbl_Sekreterler where SekreterTC =@p1 and SekreterSifre = @p2", bgl.baglanti());
            command.Parameters.AddWithValue("@p1", mskTC.Text);
            command.Parameters.AddWithValue("@p2", txtSifre.Text);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                FrmSekreterDetay detay = new FrmSekreterDetay();
                detay.tc = mskTC.Text;
                detay.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Giriş ! Lütfen Tekrar Deneyiniz..!", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            bgl.baglanti().Close();

        }


    }


}
