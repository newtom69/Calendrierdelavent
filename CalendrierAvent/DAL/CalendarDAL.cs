using AdventCalendar.Tools;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace AdventCalendar.DAL
{
    public class CalendarDAL
    {
        public Calendar Details(int id)
        {
            using (AdventCalendarEntities db = new AdventCalendarEntities())
            {
                Calendar calendar = (from c in db.Calendar
                                     where c.Id == id
                                     select c).FirstOrDefault();
                return calendar;
            }
        }

        public Calendar DetailsByPublicName(string publicName)
        {
            using (AdventCalendarEntities db = new AdventCalendarEntities())
            {
                Calendar calendar = (from c in db.Calendar
                                     where c.PublicName == publicName
                                     select c).FirstOrDefault();
                return calendar;
            }
        }

        public Calendar DetailsByPrivateName(string privateName)
        {
            using (AdventCalendarEntities db = new AdventCalendarEntities())
            {
                Calendar calendar = (from c in db.Calendar
                                     where c.PrivateName == privateName
                                     select c).FirstOrDefault();
                return calendar;
            }
        }

        internal List<Calendar> List()
        {
            using (AdventCalendarEntities db = new AdventCalendarEntities())
            {
                List<Calendar> calendars = (from c in db.Calendar
                                            select c).ToList();
                return calendars;
            }
        }

        public Dictionary<int, string> PicturesList(int id, DateTime date)
        {
            if (date.Month == 12)
                return new PictureDAL().Dictionary(Details(id).Id, date.Day);
            else
                return new Dictionary<int, string>();
        }

        public Dictionary<int, string> Dictionary(int id, DateTime? date = null)
        {
            DateTime dateOk = date ?? DateTime.MaxValue;
            if (dateOk == DateTime.MaxValue)
            {
                return new PictureDAL().Dictionary(id);
            }
            else
            {
                if (dateOk.Month == 12)
                    return new PictureDAL().Dictionary(id, dateOk.Day);
                else
                    return new Dictionary<int, string>();
            }
        }

        public Calendar Add(string name)
        {
            name = name.Replace("-", "");
            string randomPublicSuffix = "-" + Tool.RandomAsciiPrintable(6);
            string randomPrivateSuffix = "-" + Tool.RandomAsciiPrintable(10);
            Calendar calendar = new Calendar()
            {
                DisplayName = name,
                PublicName = name + randomPublicSuffix,
                PrivateName = name + randomPrivateSuffix,
                BoxId = 1 //TODO
            };
            using (AdventCalendarEntities db = new AdventCalendarEntities())
            {
                db.Calendar.Add(calendar);
                db.SaveChanges();
            }

            return calendar;
        }
    }
}