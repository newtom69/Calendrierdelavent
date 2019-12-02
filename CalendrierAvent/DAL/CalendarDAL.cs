using AdventCalendar.Models;
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

        public Calendar Details(string name)
        {
            using (AdventCalendarEntities db = new AdventCalendarEntities())
            {
                Calendar calendar = (from c in db.Calendar
                                     where c.EncryptedName == name
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

        public string GetName(string name)
        {
            using (AdventCalendarEntities db = new AdventCalendarEntities())
            {
                string shortName = (from c in db.Calendar
                                    where c.EncryptedName == name
                                    select c.Name).FirstOrDefault();
                return shortName;
            }
        }

        public string GetEncryptedName(int id)
        {
            using (AdventCalendarEntities db = new AdventCalendarEntities())
            {
                string encryptedName = (from c in db.Calendar
                                        where c.Id == id
                                        select c.EncryptedName).FirstOrDefault();
                return encryptedName;
            }
        }

        public string Path(string name)
        {
            char separator = ConfigurationManager.AppSettings["SeparatorChar"][0];
            Calendar calendar = Details(name);
            if (calendar != null)
            {
                int index = calendar.EncryptedName.IndexOf(separator);
                return calendar.EncryptedName.Substring(0, index + 1);
            }
            else
                return "";
        }

        public Dictionary<int, string> PicturesList(int id, DateTime date)
        {
            if (date.Month == 12)
                return new PictureDAL().Dictionary(Details(id).Id, date.Day);
            else
                return new Dictionary<int, string>();
        }

        public Dictionary<int, string> Dictionary(string name, DateTime date)
        {
            if (date.Month == 12)
                return new PictureDAL().Dictionary(Details(name).Id, date.Day);
            else
                return new Dictionary<int, string>();
        }

        public void Add(string name)
        {
            name = name.Replace("-", "");
            string randomSuffix = "-" + Tool.RandomAsciiPrintable(6);
            Calendar calendar = new Calendar()
            {
                Name = name,
                EncryptedName = name + randomSuffix,
            };
            using (AdventCalendarEntities db = new AdventCalendarEntities())
            {
                db.Calendar.Add(calendar);
                db.SaveChanges();
            }
        }
    }
}