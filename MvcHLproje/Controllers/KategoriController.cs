using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcHLproje.Models.Siniflar;//veri listeleme yapabilmek için sınıflarımızı tanımladım.
using PagedList;//sayfalama için ekledim.
using PagedList.Mvc;

namespace MvcHLproje.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        Context c = new Context();//tabloları tutması için context tanımladım.

        public ActionResult Index(int sayfa = 1)//sayfalamayı index içinde yapacağım için burada tanımladım. kaç verirsek o sayıdan başlıyor sayfalamaya.
        {
            //var tüm veri türlerinde değer tutabiliyor.
            var degerler = c.Kategoris.ToList().ToPagedList(sayfa, 4);//alacağı ilk değer, bir sayfada kaç tane sayfalama görünsün
            return View(degerler);
        }

        [HttpGet]//form yüklendiği zaman burayı çalıştırıyor.(çalışınca boş bir sayfa gelir)
        public ActionResult KategoriEkle()
        {
            return View();
        }

        [HttpPost]//buton'a tıkladığımız zaman burası çalışır.
        public ActionResult KategoriEkle(Kategori k)
        {
            c.Kategoris.Add(k);//context'te bulunan kategorinin içine k'dan gelen değeri ekle (k view tarafında göndereceğimiz parametreleri tutacak. burada KategoriAdi'nı tutacak)
            c.SaveChanges();//değişiklikleri kaydetme komutu
            return RedirectToAction("Index");//işlem bittikten sonra yönlendir.
        }

        public ActionResult KategoriSil(int Id)
        {
            var ktgr = c.Kategoris.Find(Id);//Kategoris tablosunda benim göderdiğim Id'yi bul
            c.Kategoris.Remove(ktgr);//bulduğun Id'yi sil.
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriGetir(int Id)
        {
            var kategori = c.Kategoris.Find(Id);
            return View("KategoriGetir", kategori);//KategoriGetir sayfasına yönlendir.(kategori ile gelen değişkenle beraber)
        }

        public ActionResult KategoriGuncelle(Kategori k)
        {
            var ktg = c.Kategoris.Find(k.KategoriId);
            ktg.KategoriAd = k.KategoriAd;
            c.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}