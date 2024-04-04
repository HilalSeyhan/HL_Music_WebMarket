using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcHLproje.Models.Siniflar;

namespace MvcHLproje.Controllers
{
    public class GaleriController : Controller
    {
        // GET: Galeri
        Context c = new Context();

        public ActionResult Index()
        {
            var degerler = c.Uruns.ToList();
            return View(degerler);
        }
    }
}