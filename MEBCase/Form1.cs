using MEBCase.Context;
using MEBCase.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MEBCase
{
    public partial class Form1 : Form
    {
        MyContext context = new MyContext();
        public int secimId = -1;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnKayit_Click(object sender, EventArgs e)
        {
            YeniKayit();
            Listele();
            Temizle();
        }
        private void YeniKayit()
        {

            if (txtUrunAdi.Text == "" || txtFiyat.Text == "" || txtUreticiFirma.Text == "" ||
                cbKategoriler.Text == "")
            {
                MessageBox.Show("Ürün adı, fiyatı,üretici firma veya kategorisi boş bırakılmaz... ");
                return;
            }
            try
            {
                Urun urun = new Urun();
                urun.Fiyat = decimal.Parse(txtFiyat.Text);
                urun.UrunAdi = txtUrunAdi.Text;
                urun.UreticiFirma = txtUreticiFirma.Text;
                urun.KategoriId = (int)cbKategoriler.SelectedValue;
           
               
                context.Urunler.Add(urun);
                context.SaveChanges();


                MessageBox.Show("Kayit basarili");

                //Temizle();
                //Listele();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        private void Listele()
        {
            Liste.Rows.Clear();
            int i = 0, sira = 1;
            var lst = context.Urunler.ToList();

            foreach (var k in lst)
            {
                Liste.Rows.Add();
                Liste.Rows[i].Cells[0].Value = k.Id;
                Liste.Rows[i].Cells[1].Value = k.UrunAdi;
                Liste.Rows[i].Cells[2].Value = k.Fiyat;
                Liste.Rows[i].Cells[3].Value = k.UreticiFirma;
                Liste.Rows[i].Cells[4].Value = k.Kategori.KategoriAdi;
                i++;
                sira++;
            }

            Liste.AllowUserToAddRows = false;
            Liste.ReadOnly = true;
            Liste.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

        private void Guncelle()
        {
            if (secimId < 0)
            {
                return;
            }
            try
            {
                var urun = context.Urunler.First(x => x.Id == secimId);
                urun.Fiyat = Convert.ToDecimal(txtFiyat.Text);
                urun.UrunAdi = txtUrunAdi.Text;
                urun.UreticiFirma = txtUreticiFirma.Text;
                urun.KategoriId = (int) cbKategoriler.SelectedValue;
               

                context.SaveChanges();

                MessageBox.Show("Guncelleme basarili");
                Temizle();
                Listele();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void Temizle()
        {
            foreach (Control k in pnlOrta.Controls)
            {
                if (k is TextBox || k is ComboBox)
                {
                    k.Text = "";
                }
            }
            secimId = -1;


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ComboDoldur();
            Listele();
        }
        private void ComboDoldur()
        {


            var kategoriler = context.Kategoriler.ToList();

            cbKategoriler.DataSource = kategoriler;
            cbKategoriler.ValueMember = "Id";
            cbKategoriler.DisplayMember = "KategoriAdi";
            cbKategoriler.SelectedIndex = -1;

        }

        private void btnFormTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void Liste_DoubleClick(object sender, EventArgs e)
        {
            secimId = (int?)Liste.CurrentRow.Cells[0].Value ?? -1;
            Ac(secimId);
        }
        public void Ac(int id)
        {
            secimId = id;//dis formdan veri gelirse secimId hatasi almamak icin bu islemi yaptim
            var urun = context.Urunler.Find(id);
            try
            {
                txtFiyat.Text = urun.Fiyat.ToString();
                txtUreticiFirma.Text = urun.UreticiFirma;
                txtUrunAdi.Text = urun.UrunAdi;

                cbKategoriler.Text = urun.Kategori.KategoriAdi == null ? "" : urun.Kategori.KategoriAdi;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            Guncelle();
            Temizle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (secimId > 0)
            {
                var urun = context.Urunler.Find(secimId);
                if (urun==null)
                {
                    MessageBox.Show("Urun Sec");
                    return;
                }
                context.Urunler.Remove(urun);
                context.SaveChanges();
                MessageBox.Show("Silme basarili");
                Temizle();
                Listele();
            }
        }
    }
}
