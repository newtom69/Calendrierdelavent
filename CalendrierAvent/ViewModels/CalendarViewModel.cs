using System.Collections.Generic;

namespace AdventCalendar.ViewModels
{
    public class CalendarViewModel
    {
        //public string DisplayName { get; }
        //public string PublicName { get; }
        //public string PrivateName { get; }
        public Calendar Calendar { get; }

        public Dictionary<int, string> PicturesNames { get; }
        public Dictionary<int, string> GenericsPicturesNames { get; }

        public CalendarViewModel(Calendar calendar , Dictionary<int, string> picturesNames, Dictionary<int, string> genericsPicturesNames)
        {
            PicturesNames = picturesNames;
            GenericsPicturesNames = genericsPicturesNames;
            //DisplayName = calendar.DisplayName;
            //PublicName = calendar.PublicName;
            //PrivateName = calendar.PrivateName;
            Calendar = calendar;
        }
    }
}