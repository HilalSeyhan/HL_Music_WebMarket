using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcHLproje.Models.Siniflar
{
    public class Cariler
    {
        [Key]
        public int CariId { get; set; }

        [Display(Name = "Cari Adı")]
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        [Required(ErrorMessage ="Bu Alanı Boş Bırakamazsınız..")]
        public string CariAd { get; set; }

        [Display(Name = "Cari Soyadı")]
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        [Required(ErrorMessage = "Bu Alanı Boş Bırakamazsınız..")]
        public string CariSoyad { get; set; }

        [Display(Name = "Cari Şehir")]
        [Column(TypeName = "Varchar")]
        [StringLength(13)]
        public string CariSehir { get; set; }

        [Display(Name = "Cari Mail")]
        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string CariMail { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string CariParola { get; set; }

        public bool Durum { get; set; }

        public ICollection<SatisHareket> SatisHarekets { get; set; }//bir cari'nin birden fazla satış hareketi olabilir.

    }
}