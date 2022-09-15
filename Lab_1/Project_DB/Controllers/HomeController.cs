using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OsipovCourseWork.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if ((UserType)Session["Access"] == UserType.Admin)
            {
                return View();
            }
            if ((UserType)Session["Access"] == UserType.User)
            {
                return RedirectToAction("IndexU");
            }
            if ((UserType)Session["Access"] == UserType.Guest)
            {
                return RedirectToAction("IndexG");
            }
            return View();
        }

        public ActionResult IndexU()
        {
            if ((UserType)Session["Access"] == UserType.User)
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        public ActionResult IndexG()
        {
            if ((UserType)Session["Access"] == UserType.Guest)
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Описание";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Контакты";

            return View();
        }

    }
}