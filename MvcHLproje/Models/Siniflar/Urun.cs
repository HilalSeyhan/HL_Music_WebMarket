using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcHLproje.Models.Siniflar
{
    public class Urun
    {
        [Key]
        public int UrunId { get; set; }

        //kısıtlamalar:
        [Display(Name = "Ürün Adı")]
        [Column(TypeName ="Varchar")]
        [StringLength(30)]
        public string UrunAd { get; set; }

        [Display(Name = "Sanatçı")]
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string Sanatci { get; set; }

        public short Stok { get; set; }

        [Display(Name = "Alış Fiyatı")]
        public decimal AlisFiyat { get; set; }

        [Display(Name = "Satış Fiyatı")]
        public decimal SatisFiyat { get; set; }

        public bool Durum { get; set; }

        [Display(Name = "Görsel")]
        [Column(TypeName = "Varchar")]
        [StringLength(250)]
        public string UrunGorsel { get; set; }

        public int Kategoriid { get; set; }//yeni ürün eklerken kategori id otomatik artan hatası veriyordu bunun önüne geçmek için Kategoriid adında yeni bir değişken oluşturdum buradan kategoriyi çekeceğim

        public virtual Kategori Kategori { get; set; }//her ürün sadece bir kategoride yer alabilir.(virtual sayesinde kategori sınıfındaki değerlere ulaşabileceğim.)

        public ICollection<SatisHareket> SatisHarekets { get; set; }

    }
}