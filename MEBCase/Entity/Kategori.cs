using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEBCase.Entity
{
    [Table("Kategoriler")]
    public class Kategori
    {
        public int Id { get; set; }
        public string KategoriAdi { get; set; }

        public List<Urun> Urunler { get; set; }
    }
}
