using AdventCalendar.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdventCalendar.Areas.Admin.Controllers
{
    public class CalendrierController : Controller
    {
        [HttpGet]
        public ActionResult Ajouter()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ajouter(string name)
        {
            CalendarDAL calendarDAL = new CalendarDAL();
            calendarDAL.Add(name);
            return View();
        }

        [HttpGet]
        public ActionResult Liste()
        {
            CalendarDAL calendarDAL = new CalendarDAL();
            calendarDAL.List();
            return View(calendarDAL.List());
        }
    }
}