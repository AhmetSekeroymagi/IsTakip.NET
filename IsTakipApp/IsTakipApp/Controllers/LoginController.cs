using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IsTakipApp.Models;

namespace IsTakipApp.Controllers
{
    public class LoginController : Controller
    {
        isTakipDbEntities entity =new isTakipDbEntities();
        // GET: Login
        public ActionResult Index()
        {
            ViewBag.mesaj = "";
            return View();
        }

        [HttpPost]
        public ActionResult Index(string kullaniciAd , string sifre)
        {
            var personel = (from p in entity.Personeller where p.personelKullaniciAd == kullaniciAd && p.personelParola == sifre select p).FirstOrDefault();

            if (personel != null)
            {
                Session["PesonelAdSoyad"] = personel.personelAdSoyad;
                Session["PersonelId"] = personel.personelId;
                Session["PersonelBirimId"] = personel.personelBirimId;
                Session["PersonelYetkiTurId"] = personel.personelYetkiTurId;

                switch (personel.personelYetkiTurId)
                {
                    case 1:
                        return RedirectToAction("Index", "Yonetici");
                    case 2:
                        return RedirectToAction("Index", "Calisan");
                    default:
                        return View();
                }
            }
            else
            {
                ViewBag.mesaj = "Kullanıcı Adı yada Parola Yanlış";

                return View();
            }
        }
    }
}