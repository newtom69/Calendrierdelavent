using AdventCalendar.DAL;
using AdventCalendar.Models;
using AdventCalendar.ViewModels;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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

        [HttpGet]
        public ActionResult Details(string name)
        {
            // TODO DRY même système que Calendrier Index de area "" 
            CalendarDAL calendarDAL = new CalendarDAL();
            Calendar calendar = calendarDAL.Details(name);

            BoxDAL boxDAL = new BoxDAL();
            Box box = boxDAL.Details(calendar.BoxId);

            string boxPictureFullName = Path.Combine(ConfigurationManager.AppSettings["BoxPicturePath"], box.Path);
            Dictionary<int, string> dictionaryGenericsPicturesNames = new Dictionary<int, string>();
            for (int i = 1; i <= 24; i++)
            {
                dictionaryGenericsPicturesNames.Add(i, Path.Combine(boxPictureFullName, $"{i}.png"));
            }
            Dictionary<int, string> dictionaryPicturesNames = calendarDAL.Dictionary(name);

            CalendarViewModel calendarViewModel = new CalendarViewModel(dictionaryPicturesNames, dictionaryGenericsPicturesNames);
            ViewBag.CalendarName = calendar.Name;
            return View(calendarViewModel);
        }


    }
}