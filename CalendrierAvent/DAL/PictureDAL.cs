using HttpCalendrierAvent.Models;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace HttpCalendrierAvent.DAL
{
    public class PictureDAL
    {
        public Picture Details(int calendarId, int dayNumber)
        {
            Calendar cal = new Calendar(calendarId);
            cal.Lire();
            Picture picture = cal.CollPic.Liste.Where(p => p.DayNumber == dayNumber).FirstOrDefault();
            return picture;
        }

        public Dictionary<int, string> Dictionary(int calendarId, int dayNumber = 31)
        {
            string calendarPath = new CalendarDAL().Details(calendarId).PublicName;
            string openPicturePath = Path.Combine(ConfigurationManager.AppSettings["PicturePath"], calendarPath);
            Calendar cal = new Calendar(calendarId);
            cal.Lire();
            return cal.CollPic.Liste.Where(p => p.DayNumber <= dayNumber).ToDictionary(x => x.DayNumber, x => Path.Combine(openPicturePath, x.Name));
        }

        public void Add(int calendarId, int dayNumber, string name)
        {
            Picture picture = Details(calendarId, dayNumber);
            if (picture == null)
            {
                picture = new Picture()
                {
                    CalendarId = calendarId,
                    DayNumber = dayNumber,
                    Name = name,
                };
            }
            else
            {
                picture.Name = name;
            }
            picture.Enregistrer();
        }
    }
}