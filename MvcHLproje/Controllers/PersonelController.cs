using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcHLproje.Models.Siniflar;

namespace MvcHLproje.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel
        Context c = new Context();

        public ActionResult Index()
        {
            var degerler = c.Personels.ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult PersonelEkle()
        {
            List<SelectListItem> deger1 = (from x in c.Departmans.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DepartmanAd,
                                               Value = x.DepartmanId.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            return View();
        }

        [HttpPost]
        public ActionResult PersonelEkle(Personel p)
        {
            if (Request.Files.Count > 0)
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/images/" + dosyaadi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                p.PersonelGorsel = "/images/" + dosyaadi + uzanti;//veri tabanına kaydetmek için.
            }
            c.Personels.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PersonelGetir(int Id)
        {
            List<SelectListItem> deger1 = (from x in c.Departmans.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DepartmanAd,
                                               Value = x.DepartmanId.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            var prs = c.Personels.Find(Id);
            return View("PersonelGetir", prs);
        }

        public ActionResult PersonelGuncelle(Personel pr)
        {
            if (Request.Files.Count > 0)
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/images/" + dosyaadi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                pr.PersonelGorsel = "/images/" + dosyaadi + uzanti;//veri tabanına kaydetmek için.
            }
            var prsnl = c.Personels.Find(pr.PersonelId);
            prsnl.PersonelAd = pr.PersonelAd;
            prsnl.PersonelSoyad = pr.PersonelSoyad;
            prsnl.PersonelGorsel = pr.PersonelGorsel;
            prsnl.Departmanid = pr.Departmanid;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PersonelListe()
        {
            var sorgu = c.Personels.ToList();
            return View(sorgu);
        }



    }
}