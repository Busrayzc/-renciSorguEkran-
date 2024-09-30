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

namespace EntityOrnek
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("data source=localhost;initial catalog=DbSinavOgrenci;integrated security=True;trustservercertificate=True;");
        DbSinavOgrenciEntities db = new DbSinavOgrenciEntities();
        private void btnDersListesi_Click(object sender, EventArgs e)
        {
            
            SqlCommand komut = new SqlCommand("select * from TblDersler",baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnOgrenciListele_Click(object sender, EventArgs e)
        {
           
            dataGridView1.DataSource = db.TblOgrenci.ToList();
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
        }

        private void btnNotListesi_Click(object sender, EventArgs e)
        {
            var query = from item in db.TblNotlar
                        select new { item.NotId, item.Ogr, item.Ders, item.sinav1, item.sinav2, item.sinav3, item.ortalama, item.durum };
            dataGridView1.DataSource = query.ToList();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            TblOgrenci t = new TblOgrenci();
            t.Ad = txtAd.Text;
            t.Soyad = txtSoyad.Text;
            db.TblOgrenci.Add(t);
            db.SaveChanges();
            MessageBox.Show("Öğrenci listeye kaydedildi.");
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtOgrenciId.Text);
            var x = db.TblOgrenci.Find(id);
            db.TblOgrenci.Remove(x);
            db.SaveChanges();
            MessageBox.Show("Öğrenci sistemden silindi.");
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtOgrenciId.Text);
            var x = db.TblOgrenci.Find(id);
            x.Ad = txtAd.Text;
            x.Soyad = txtSoyad.Text;
            x.Fotograf = txtFoto.Text;
            db.SaveChanges();
            MessageBox.Show("Öğrenci bilgileri başarıyla güncellendi.");
        }

        private void btnBul_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.TblOgrenci.Where(x => x.Ad == txtAd.Text).ToList();
            dataGridView1.DataSource = db.TblOgrenci.Where(x => x.Soyad == txtSoyad.Text).ToList();
            


        }

        private void txtAd_TextChanged(object sender, EventArgs e)
        {
            string aranan = txtAd.Text;
            var degerler = from item in db.TblOgrenci
                           where item.Ad.Contains(aranan)
            select item;
            dataGridView1.DataSource = degerler.ToList();

        }

        private void btnNotGuncelle_Click(object sender, EventArgs e)
        {
            var sorgu = from d1 in db.TblNotlar
                        join d2 in db.TblOgrenci
                        on d1.Ogr equals d2.OgrenciId
                        join d3 in db.TblDersler 
                        on d1.Ders equals d3.DersId
                        select new
                        {
                            öğrenci=d2.Ad+" "+d2.Soyad,
                            DERS=d3.DersAd, 
                            SINAV1=d1.sinav1,
                            SINAV2=d1.sinav2,
                            SINAV3=d1.sinav3,
                            Ortalama=d1.ortalama,
                        };
            dataGridView1.DataSource = sorgu.ToList();
        }
    }
}
