using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace AdventCalendar.DAL
{
    public class PictureDAL
    {

        public Picture Details(int calendarId, int dayNumber)
        {
            using (AdventCalendarEntities db = new AdventCalendarEntities())
            {
                Picture picture = (from p in db.Picture
                                   join c in db.Calendar on p.CalendarId equals c.Id
                                   where p.CalendarId == calendarId && p.DayNumber == dayNumber
                                   select p).FirstOrDefault();

                return picture;
            }
        }

        public Dictionary<int, string> Dictionary(int calendarId, int dayNumber = 31)
        {
            string calendarPath = new CalendarDAL().Details(calendarId).PublicName;
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

        public void Add(int calendarId, int dayNumber, string name)
        {
            using (AdventCalendarEntities db = new AdventCalendarEntities())
            {
                Picture picture = (from p in db.Picture
                                   join c in db.Calendar on p.CalendarId equals c.Id
                                   where p.CalendarId == calendarId && p.DayNumber == dayNumber
                                   select p).FirstOrDefault();

                if (picture == null)
                {
                    picture = new Picture()
                    {
                        CalendarId = calendarId,
                        DayNumber = dayNumber,
                        Name = name,
                    };
                    db.Picture.Add(picture);
                }
                else
                {
                    picture.Name = name;
                }
                db.SaveChanges();
            }
        }
    }
}