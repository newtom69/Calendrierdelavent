using HttpCalendrierAvent.DAL;
using HttpCalendrierAvent.Models;
using HttpCalendrierAvent.Tools;
using HttpCalendrierAvent.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace HttpCalendrierAvent.Controllers
{
    public class CalendrierController : ParentController
    {
        [HttpGet]
        public ActionResult Index(string name)
        {
            CalendarDAL calendarDAL = new CalendarDAL();
            Calendar calendar = calendarDAL.DetailsByPublicName(name);
            if (calendar.Id == -1)
            {
                // if calendar witch PublicName don't exist, test with PrivateName and redirect to admin 
                if (calendarDAL.DetailsByPrivateName(name).Id != -1)
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
        public ActionResult Ajouter(string name, string email)
        {
            CalendarDAL calendarDAL = new CalendarDAL();
            Calendar calendar = calendarDAL.Add(name);
            string directoryName = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["PicturePath"]), calendar.PublicName);
            Directory.CreateDirectory(directoryName);
            string subject = "Calendrier créé";
            string publicUrl = $"{Request.Url.Scheme}://{Request.Url.Authority}/{calendar.PublicName}";
            string privateUrl = $"{Request.Url.Scheme}://{Request.Url.Authority}/{calendar.PrivateName}";
            string message = "Bonjour\n\n" +
                "Bravo, vous avez créé un calendrier.\n" +
                "Voilà les liens indispensables :\n" +
                $"public : {publicUrl}\n" +
                $"privé (uniquement pour vous) : {privateUrl}\n" +
                "N'oubliez pas d'y ajouter les photos / images\n\n" +
                $"A bientôt sur {Request.Url.Authority}";
            Tool.EnvoieMail(email, subject, message);
            return Redirect($"{Request.Url.Scheme}://{Request.Url.Authority}/Modifier/{calendar.PrivateName}");
        }

        [HttpGet]
        public ActionResult Liste()
        {
            return View((Calendar)null);
        }

        [HttpPost]
        public ActionResult Liste(string password)
        {
            string cryptedGoodPassword = ConfigurationManager.AppSettings["Adminpassword"];
            string cryptedPassword = password.GetHash();
            if (cryptedPassword == cryptedGoodPassword)
            {
                CalendarDAL calendarDAL = new CalendarDAL();
                calendarDAL.List();
                return View(calendarDAL.List());
            }
            else
            {
                ViewBag.Message = "Mot de passe incorrect";
                return View((Calendar)null);
            }
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
        public ActionResult Modifier(string privateName, HttpPostedFileBase file, int dayNumber)
        {
            Calendar calendar = new CalendarDAL().DetailsByPrivateName(privateName);
            int calendarId = calendar.Id;
            string publicName = calendar.PublicName;
            PictureDAL pictureDAL = new PictureDAL();

            if (file != null)
            {
                string directoryName = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["PicturePath"]), publicName);
                string extension = Path.GetExtension(file.FileName);
                if (extension.Length > 5)
                    extension = extension.Substring(0, 5);
                string fileName = Guid.NewGuid().ToString("n") + extension;
                string fullFileName = Path.Combine(directoryName, fileName);
                using (FileStream fileStream = new FileStream(fullFileName, FileMode.Create))
                {
                    file.InputStream.Seek(0, SeekOrigin.Begin);
                    file.InputStream.CopyTo(fileStream);
                }
                #region futurFeature
                //if (extension == ".webp")
                //{
                //    using (FileStream fileStream = new FileStream(fullFileName, FileMode.Create))
                //    {
                //        files[i].InputStream.Seek(0, SeekOrigin.Begin);
                //        files[i].InputStream.CopyTo(fileStream);
                //    }
                //}
                //else
                //{
                //    using (Image image = Image.FromStream(files[i].InputStream))
                //        image.Save(fullFileName);
                //}
                #endregion
                pictureDAL.Add(calendarId, dayNumber, fileName);
            }
            return Redirect($"{ Request.Url.Scheme}://{Request.Url.Authority}/{privateName}");
        }
    }
}