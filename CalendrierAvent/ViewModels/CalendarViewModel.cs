using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdventCalendar.ViewModels
{
    public class CalendarViewModel
    {
        //public string[] ImagesPath = new string[24];

        public Dictionary<int, string> DictionaryPicturesNames { get; }
        public Dictionary<int, string> DictionaryGenericsPicturesNames { get;}

        public CalendarViewModel(Dictionary<int, string> dictionaryPicturesNames, Dictionary<int, string> dictionaryGenericsPicturesNames)
        {
            DictionaryPicturesNames = dictionaryPicturesNames;
            DictionaryGenericsPicturesNames = dictionaryGenericsPicturesNames;
        }
    }
}