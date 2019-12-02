using AdventCalendar.DAL;
using AdventCalendar.Models;
using AdventCalendar.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdventCalendar.Controllers
{
    public class CalendrierController : Controller
    {
        [HttpGet]
        public ActionResult Index(string name)
        {
            CalendarDAL calendarDAL = new CalendarDAL();
            Calendar calendar = calendarDAL.Details(name);
            ViewBag.CalendarName = calendar.Name;

            string boxPicturePath = Path.Combine(ConfigurationManager.AppSettings["BoxPicturePath"], calendar.BoxName);
            Dictionary<int, string> dictionaryGenericsPicturesNames = new Dictionary<int, string>();
            for (int i = 1; i <= 24; i++)
            {
                dictionaryGenericsPicturesNames.Add(i, Path.Combine(boxPicturePath, $"{i}.png"));
            }
            Dictionary<int, string> dictionaryPicturesNames = calendarDAL.Dictionary(name, DateTime.Today);
            
            CalendarViewModel calendarViewModel = new CalendarViewModel(dictionaryPicturesNames, dictionaryGenericsPicturesNames);
            return View(calendarViewModel);
        }

        public ActionResult Add(string name)
        {
            CalendarDAL calendrierDAL = new CalendarDAL();
            calendrierDAL.Add(name);

            return View();
        }


    }
}