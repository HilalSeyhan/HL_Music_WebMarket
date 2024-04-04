using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcHLproje.Models.Siniflar
{
    public class Kategori
    {
        [Key]
        public int KategoriId { get; set; }

        [Display(Name = "Kategori Adı")]
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string KategoriAd { get; set; }

        public ICollection<Urun> Uruns { get; set; }//her bir kategoride birden fazla ürün yer alabilir.(s veri tabanı karşılığı sebebi ile geldi)




    }
}