using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcHLproje.Models.Siniflar;

namespace MvcHLproje.Controllers
{
    public class CariPanelController : Controller
    {
        // GET: CariPanel
        Context c = new Context();

        [Authorize]
        public ActionResult Index()
        {
            var mail = (string)Session["CariMail"];
            var degerler = c.Carilers.FirstOrDefault(x => x.CariMail == mail);
            ViewBag.m = mail;
            return View(degerler);
        }

        [Authorize]
        public ActionResult Siparislerim()
        {
            var mail = (string)Session["CariMail"];
            var Id = c.Carilers.Where(x => x.CariMail == mail.ToString()).Select(y => y.CariId).FirstOrDefault();
            var degerler = c.SatisHarekets.Where(x => x.Cariid == Id).ToList();
            return View(degerler);
        }

        [Authorize]
        public ActionResult KargoTakip(string p)
        {
            var k = from x in c.KargoDetays select x;
                k = k.Where(y => y.TakipKodu.Contains(p));
            return View(k.ToList());
        }

        [Authorize]
        public ActionResult CariKargoTakip(string id)
        {
            var degerler = c.KargoTakips.Where(x => x.TakipKodu == id).ToList();
            return View(degerler);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

    }
}