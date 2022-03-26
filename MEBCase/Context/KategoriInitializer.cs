using MEBCase.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEBCase.Context
{


    public class KategoriInitializer : DropCreateDatabaseIfModelChanges<MyContext>
    {
        protected override void Seed(MyContext context)
        {
            var kategoriler = new List<Kategori>
        {
            new Kategori { KategoriAdi="Temizlik" },
            new Kategori { KategoriAdi="Bakım"},

        };
            kategoriler.ForEach(s => context.Kategoriler.Add(s));
            context.SaveChanges();
        }
    }

}
