using AdventCalendar.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace AdventCalendar.DAL
{
    public class PictureDAL
    {

        public Picture Details(int calendrierId, int dayNumber)
        {
            using (AdventCalendarEntities db = new AdventCalendarEntities())
            {
                Picture picture = (from p in db.Picture
                                   join c in db.Calendar on p.CalendarId equals c.Id
                                   where p.CalendarId == calendrierId && p.DayNumber == dayNumber
                                   select p).FirstOrDefault();

                return picture;
            }
        }

        public Dictionary<int, string> Dictionary(int calendarId, int dayNumber = 31)
        {
            string calendarPath = new CalendarDAL().GetEncryptedName(calendarId);
            string openPicturePath = Path.Combine(ConfigurationManager.AppSettings["PicturePath"], calendarPath);
         
            using (AdventCalendarEntities db = new AdventCalendarEntities())
            {
                Dictionary<int, string> pictures = (from p in db.Picture
                                                    join c in db.Calendar on p.CalendarId equals c.Id
                                                    where p.CalendarId == calendarId && p.DayNumber <= dayNumber
                                                    select p).ToDictionary(x => x.DayNumber, x => Path.Combine(openPicturePath, x.Name));
                return pictures;
            }
        }

        public void Add(int calendarId, int dayNumber)
        {
            Picture picture = new Picture()
            {
                CalendarId = calendarId,
                DayNumber = dayNumber,
                Name = Guid.NewGuid().ToString("n"),
            };
            using (AdventCalendarEntities db = new AdventCalendarEntities())
            {
                db.Picture.Add(picture);
                db.SaveChanges();
            }
        }

    }
}