using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcHLproje.Models.Siniflar;

namespace MvcHLproje.Controllers
{
    public class istatistikController : Controller
    {
        // GET: istatistik
        Context c = new Context();

        public ActionResult Index()
        {
            //toplam cari
            var deger1 = c.Carilers.Count().ToString();
            ViewBag.d1 = deger1;

            //ürün sayısı
            var deger2 = c.Uruns.Count().ToString();
            ViewBag.d2 = deger2;

            //personel sayısı
            var deger3 = c.Personels.Count().ToString();
            ViewBag.d3 = deger3;

            //kategori sayısı
            var deger4 = c.Kategoris.Count().ToString();
            ViewBag.d4 = deger4;

            //toplam stok
            var deger5 = c.Uruns.Sum(x=>x.Stok).ToString();//sum komutu ile toplamayı yaptırıyoruz.
            ViewBag.d5 = deger5;

            //sanatçı sayısı
            var deger6 = (from x in c.Uruns select x.Sanatci).Distinct().Count().ToString();
            //ürünler içerisinden markayı seç. seçmiş olduğun markaları tekrarsız getir. say. string olarak yazdır.
            ViewBag.d6 = deger6;

            //min fiyatlı ürün
            var deger9 = (from x in c.Uruns orderby x.SatisFiyat ascending select x.UrunAd).FirstOrDefault();
            //ürünler içerisinde satış fiyatı'nı sırala. a'dan z'ye (küçükten büyüğe) sırala. buradan ürünün adını getir. en üstteki değeri getir.
            ViewBag.d9 = deger9;

            //max fiyatlı ürün
            var deger8 = (from x in c.Uruns orderby x.SatisFiyat descending select x.UrunAd).FirstOrDefault();
            //ürünler içerisinde satış fiyatı'nı sırala. en üste en büyüğü al. buradan ürünün adını getir. en üstteki değeri getir.
            ViewBag.d8 = deger8;

            //kritik seviye
            var deger7 = c.Uruns.Count(x => x.Stok <= 100).ToString();
            ViewBag.d7 = deger7;

            //max sanatçı
            var deger12 = c.Uruns.GroupBy(x => x.Sanatci).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault();
            //sanatçıya göre gruplandır. count'a göre b-k sırala. sonra sanatçıyı getir. en üsttekini seç
            ViewBag.d12 = deger12;

            //en çok satan
            var deger13 = c.Uruns.Where(u => u.UrunId == (c.SatisHarekets.GroupBy(x => x.Urunid).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault())).Select(k => k.UrunAd).FirstOrDefault();
            ViewBag.d13 = deger13;

            //kasadaki tutar
            var deger14 = c.SatisHarekets.Sum(x => x.ToplamTutar).ToString();
            ViewBag.d14 = deger14;

            //bugünkü satışlar
            DateTime bugun = DateTime.Today;//bu şekilde bugünün tarihini alıp ona göre sorguya girecek.
            var deger15 = c.SatisHarekets.Count(x => x.Tarih == bugun).ToString();
            ViewBag.d15 = deger15;

            //bugünkü kasa !!!!!bugün satış yapılmadıysa null döner hata alırız.
            var deger16 = c.SatisHarekets.Where(x => x.Tarih == bugun).Sum(y => (decimal?)y.ToplamTutar).ToString();
            ViewBag.d16 = deger16;

            return View();
        }

        public ActionResult KolayTablolar()
        {
            var sorgu = from x in c.Carilers
                        group x by x.CariSehir into g
                        select new SinifGrup
                        {
                            Sehir = g.Key,
                            Sayi = g.Count()
                        };//cariler tablosundan şehirleri grupla Sehir içine içeriği aktar. Sayi içine de sayısını.

            return View(sorgu.ToList());
        }

        public PartialViewResult Partial1()
        {
            var sorgu2 = from x in c.Personels
                         group x by x.Departman.DepartmanAd into g
                         select new SinifGrup2
                         {
                             Departman = g.Key,
                             Sayi = g.Count()
                         };//personeller tablosundan departmanid'ye göre grupla departman içine içeriği aktar. Sayi içine de sayısını.

            return PartialView(sorgu2.ToList());
        }

        public PartialViewResult Partial2()
        {
            var sorgu3 = c.Carilers.ToList();
            return PartialView(sorgu3);
        }

        public PartialViewResult Partial3()
        {
            var sorgu4 = c.Uruns.ToList();
            return PartialView(sorgu4);
        }

        public PartialViewResult Partial4()
        {
            var sorgu5 = from x in c.Uruns
                         group x by x.Sanatci into g
                         select new SinifGrup3
                         {
                             sanatci = g.Key,
                             sayi = g.Count()
                         };
            return PartialView(sorgu5.ToList());
        }

    }
}