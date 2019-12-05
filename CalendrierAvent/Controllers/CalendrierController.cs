using AdventCalendar.DAL;
using AdventCalendar.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
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
            Calendar calendar = calendarDAL.DetailsByPublicName(name);
            if (calendar == null)
            {
                // if calendar witch PublicName don't exist, test with PrivateName and redirect to admin 
                if (calendarDAL.DetailsByPrivateName(name) != null)
                    return Redirect($"{Request.Url.Scheme}://{Request.Url.Authority}/Modifier/{name}");
                else
                    return RedirectToAction("Ajouter");
            }

            BoxDAL boxDAL = new BoxDAL();
            Box box = boxDAL.Details(calendar.BoxId);

            string boxPictureFullName = Path.Combine(ConfigurationManager.AppSettings["BoxPicturePath"], box.Path);
            Dictionary<int, string> dictionaryGenericsPicturesNames = new Dictionary<int, string>();
            for (int i = 1; i <= 24; i++)
            {
                dictionaryGenericsPicturesNames.Add(i, Path.Combine(boxPictureFullName, $"{i}.png"));
            }
            Dictionary<int, string> dictionaryPicturesNames = calendarDAL.Dictionary(calendar.Id, DateTime.Today);

            CalendarViewModel calendarViewModel = new CalendarViewModel(calendar, dictionaryPicturesNames, dictionaryGenericsPicturesNames);
            return View(calendarViewModel);
        }

        [HttpGet]
        public ActionResult Ajouter()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ajouter(string name)
        {
            CalendarDAL calendarDAL = new CalendarDAL();
            string privateName = calendarDAL.Add(name);
            return Redirect($"{Request.Url.Scheme}://{Request.Url.Authority}/Modifier/{privateName}");
        }

        [HttpGet]
        public ActionResult Liste()
        {
            CalendarDAL calendarDAL = new CalendarDAL();
            calendarDAL.List();
            return View(calendarDAL.List());
        }

        [HttpGet]
        public ActionResult Modifier(string name)
        {
            // TODO DRY même système que Calendrier Index de area "" 
            CalendarDAL calendarDAL = new CalendarDAL();
            Calendar calendar = calendarDAL.DetailsByPrivateName(name);

            BoxDAL boxDAL = new BoxDAL();
            Box box = boxDAL.Details(calendar.BoxId);

            string boxPictureFullName = Path.Combine(ConfigurationManager.AppSettings["BoxPicturePath"], box.Path);
            Dictionary<int, string> dictionaryGenericsPicturesNames = new Dictionary<int, string>();
            for (int i = 1; i <= 24; i++)
            {
                dictionaryGenericsPicturesNames.Add(i, Path.Combine(boxPictureFullName, $"{i}.png"));
            }
            Dictionary<int, string> dictionaryPicturesNames = calendarDAL.Dictionary(calendar.Id);

            CalendarViewModel calendarViewModel = new CalendarViewModel(calendar, dictionaryPicturesNames, dictionaryGenericsPicturesNames);
            return View(calendarViewModel);
        }

        [HttpPost]
        public ActionResult Modifier(string privateName, List<HttpPostedFileBase> files)
        {
            Calendar calendar = new CalendarDAL().DetailsByPrivateName(privateName);
            int calendarId = calendar.Id;
            string publicName = calendar.PublicName;
            PictureDAL pictureDAL = new PictureDAL();
            for (int i = 0; i < files.Count; i++) //TODO change for no loop
            {
                if (files[i] != null)
                {
                    int dayNumber = i + 1;
                    string directoryName = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["PicturePath"]), publicName);
                    string extension = Path.GetExtension(files[i].FileName);
                    if (extension.Length > 5)
                        extension = extension.Substring(0, 5);

                    string fileName = Guid.NewGuid().ToString("n") + extension;
                    string fullFileName = Path.Combine(directoryName, fileName);
                    Directory.CreateDirectory(directoryName);
                    using (Image image = Image.FromStream(files[i].InputStream))
                        image.Save(fullFileName);
                    pictureDAL.Add(calendarId, dayNumber, fileName);
                }
            }
            return Redirect($"{ Request.Url.Scheme}://{Request.Url.Authority}/{privateName}");
        }
    }
}