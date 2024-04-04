using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcHLproje.Models.Siniflar;

namespace MvcHLproje.Controllers
{
    public class DepartmanController : Controller
    {
        // GET: Departman
        Context c = new Context();

        public ActionResult Index()
        {
            var degerler = c.Departmans.Where(x => x.Durum == true).ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult DepartmanEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DepartmanEkle(Departman d)
        {
            d.Durum = true;
            c.Departmans.Add(d);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DepartmanSil(int Id)
        {
            var dep = c.Departmans.Find(Id);
            dep.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DepartmanGetir(int Id)
        {
            var dptgtr = c.Departmans.Find(Id);
            return View("DepartmanGetir", dptgtr);
        }

        public ActionResult DepartmanGuncelle(Departman p)
        {
            var depart = c.Departmans.Find(p.DepartmanId);
            depart.DepartmanAd = p.DepartmanAd;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DepartmanDetay(int Id)
        {
            var degerler = c.Personels.Where(x => x.Departmanid == Id).ToList();//departman detay'da o departmanda olan personelleri listeletiyoruz.
            var dpt = c.Departmans.Where(x => x.DepartmanId == Id).Select(y => y.DepartmanAd).FirstOrDefault();//Id'ye göre sorgu yapıyoruz daha sonra o Id değerinin karşılığı olan departman adı değerini alıyoruz. ToList kullanmadık çünkü tek bir eleman gösterecek listeleme mantığı yok bir değer için de FirstOrDefault kullandık
            ViewBag.d = dpt;
            return View(degerler);
        }

        public ActionResult DepartmanPersonelSatis(int Id)
        {
            var degerler = c.SatisHarekets.Where(x => x.Personelid == Id).ToList();

            var per = c.Personels.Where(x => x.PersonelId == Id).Select(y => y.PersonelAd +" "+y.PersonelSoyad).FirstOrDefault();
            ViewBag.dpersonel = per;//satış sayfasında üst kısımda personelin adını göstermek için.

            return View(degerler);
        }
    }
}