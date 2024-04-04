using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcHLproje.Models.Siniflar
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(10)]
        public string KullaniciAd { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string Parola { get; set; }

        [Column(TypeName = "Char")]//a, b, c gibi yetki alanı uygulanacağı için char ve 1 karakter veriyorum.
        [StringLength(1)]
        public string Yetki { get; set; }
    }
}