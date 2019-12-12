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
            Calendar calendar = new Calendar(id);
            calendar.Lire();
            return calendar;
        }

        public Calendar DetailsByPublicName(string publicName)
        {
            Calendar calendar = new Calendar(publicName, "PublicName");
            calendar.Lire();
            return calendar;
        }

        public Calendar DetailsByPrivateName(string privateName)
        {
            Calendar calendar = new Calendar(privateName, "PrivateName");
            calendar.Lire();
            return calendar;
        }

        internal List<Calendar> List()
        {
            OmniFW.Business.CollectionEntite<Calendar> calendars = new OmniFW.Business.CollectionEntite<Calendar>();
            calendars.Rechercher();
            return calendars.Liste;
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
            calendar.Enregistrer();
            return calendar;
        }
    }
}